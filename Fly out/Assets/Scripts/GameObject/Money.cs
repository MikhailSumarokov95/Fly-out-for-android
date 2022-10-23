using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] private int[] rewardForPositionInGame;
    [SerializeField] private int[] rewardForPositionInRound;
    [SerializeField] private TMP_Text[] moneyTexts;
    [SerializeField] private TMP_Text rewardMoneyText;

    public int PlayerPositionInLeaderBoard { get; set; }

    private void Start()
    {
        foreach (var moneyText in moneyTexts) 
            moneyText.text = PlayerPrefs.GetInt("money", 0).ToString();
    }

    private void Update()
    {
        if (rewardMoneyText.gameObject.activeInHierarchy) rewardMoneyText.color -= new Color(0, 0, 0, 0.01f);
        rewardMoneyText.gameObject.SetActive(rewardMoneyText.color.a > 0.1f);
    }

    public void RewardingForGame()
    {
        if (PlayerPositionInLeaderBoard >= rewardForPositionInGame.Length
            || PlayerPositionInLeaderBoard < 1) return;
        ChangeAmountMoney(rewardForPositionInGame[PlayerPositionInLeaderBoard - 1]);
    }

    public void RewardingForRound()
    {
        if (PlayerPositionInLeaderBoard >= rewardForPositionInRound.Length
            || PlayerPositionInLeaderBoard < 1) return;
        ChangeAmountMoney(rewardForPositionInRound[PlayerPositionInLeaderBoard - 1]);
    }

    public void Rewarding(int reward) => ChangeAmountMoney(reward);

    public void SpendMoney(int reward) => ChangeAmountMoney(- reward);

    private void ChangeAmountMoney(int value)
    {
        var money = PlayerPrefs.GetInt("money", 0) + value;
        PlayerPrefs.SetInt("money", money);
        foreach(var moneyText in moneyTexts) moneyText.text = money.ToString();
        rewardMoneyText.gameObject.SetActive(true);
        rewardMoneyText.text = value.ToString();
        rewardMoneyText.color += new Color(0, 0, 0, 1);
    }
}
