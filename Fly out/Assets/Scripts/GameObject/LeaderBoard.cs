using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using ToxicFamilyGames.MenuEditor;
using System.Text;
using UnityEngine.Events;

public class LeaderBoard : MonoBehaviour
{
    public UnityEvent onStartedLeaderBoard;
    [SerializeField] private TMP_Text[] namesGamersTable;
    [SerializeField] private TMP_Text[] scoreGamersTable;
    [SerializeField] private int maxScoreRound;
    [SerializeField] private int minScoreRound;
    private string[] _namesGamersOfLanguage;
    private string _namePlayerOfLanguage;
    private Gamers[] _gamers;
    private LanguageController _languageController;
    private Money _money;

    private void Start()
    {
        _languageController = FindObjectOfType<LanguageController>();
        _money = FindObjectOfType<Money>();
        _namePlayerOfLanguage = GetNamePlayer(_languageController.SelectedLanguage);
        _namesGamersOfLanguage = GetGamersNamesOfLanguage(_languageController.SelectedLanguage);
        _gamers = new Gamers[namesGamersTable.Length];
    }

    public void CreateNewGamers()
    {
        _gamers[0] = new Gamers { Name = _namePlayerOfLanguage, Score = 0 }; 
        for (int i = 1; i < namesGamersTable.Length; i++)
        {
            _gamers[i] = new Gamers();
            _gamers[i].Name = _namesGamersOfLanguage[Random.Range(0, _namesGamersOfLanguage.Length)];
            _gamers[i].Score = 0;
        }
    }

    public void StartLeaderBoard(int scorePlayer, bool addScorePlayerOnly)
    {
        if (!addScorePlayerOnly) onStartedLeaderBoard?.Invoke();
        var playerPositionInLeaderBoard = -1;
        var minScore = minScoreRound / 10;
        var maxScore = maxScoreRound / 10;
        for (int i = 0; i < _gamers.Length; i++)
        {
            if (_gamers[i].Name == _namePlayerOfLanguage) _gamers[i].Score += scorePlayer;
            else if (addScorePlayerOnly) continue;
            else _gamers[i].Score += Random.Range(minScore, maxScore) * 10;
        }
        _gamers = SortGamers(_gamers);
        for (int i = 0; i < _gamers.Length; i++)
        {
            if (_gamers[i].Name == _namePlayerOfLanguage) playerPositionInLeaderBoard = i + 1;
            namesGamersTable[i].text = _gamers[i].Name;
            scoreGamersTable[i].text = _gamers[i].Score.ToString();
        }
        _money.PlayerPositionInLeaderBoard = playerPositionInLeaderBoard;
    }

    public void RewardPlayerScore(int score) => StartLeaderBoard(score, true);

    private Gamers[] SortGamers(Gamers[] gamers)
    {
        for (int i = 0; i < _gamers.Length - 1; i++)
        {
            int max = i;
            for (int j = i + 1; j < gamers.Length; j++)
            {
                if (gamers[j].Score > gamers[max].Score)
                {
                    max = j;
                }
            }
            Gamers temp = gamers[max];
            gamers[max] = gamers[i];
            gamers[i] = temp;
        }
        return gamers;
    }

    private string GetNamePlayer(string language)
    {
        switch (language)
        {
            case "ru":
                return "Вы";
            case "en":
                return "You";
            case "tr":
                return "Sen";
            default:
                return "You";
        }
    }

    private string[] GetGamersNamesOfLanguage(string language)
    {
        string namesOfLanguage;
        var namesOfPeoplesJson = Resources.Load<TextAsset>("NameGamers");
        var NamesOfPeoples = JsonUtility.FromJson<NamesOfPeoples>(namesOfPeoplesJson.ToString());
        switch (language)
        {
            case "ru":
                namesOfLanguage = NamesOfPeoples.ru;
                break;
            case "en":
                namesOfLanguage = NamesOfPeoples.en;
                break;
            case "tr":
                namesOfLanguage = NamesOfPeoples.tr;
                break;
            default:
                namesOfLanguage = NamesOfPeoples.en;
                break;
        }
        return ParceName(namesOfLanguage);
    }

    private string[] ParceName(string listNameInString)
    {
        var Names = new List<string>();
        StringBuilder nameSB = new StringBuilder();
        for (int i = 0; i < listNameInString.Length; i++)
        {
            if (char.IsLetter(listNameInString[i]))
            {
                nameSB.Append(listNameInString[i]);
            }
            else
            {
                if (nameSB.Length != 0)
                {
                    Names.Add(nameSB.ToString());
                    nameSB.Clear();
                }
            }
        }
        return Names.ToArray();
    }

    private class Gamers
    {
        public int Score;
        public string Name;
    }

    [System.Serializable]
    private class NamesOfPeoples
    {
        public string ru;
        public string en;
        public string tr;
    }
}