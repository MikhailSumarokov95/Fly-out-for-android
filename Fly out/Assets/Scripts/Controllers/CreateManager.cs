using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToxicFamilyGames.MenuEditor;
using UnityEngine.Events;

public class CreateManager : MonoBehaviour, IShoper
{
    public UnityEvent<GameObject> onCreateCar;
    public UnityEvent<GameObject> onCreateCharacter;
    [SerializeField] private Transform carSpawnPoint;
    [SerializeField] private GameObject characterSkin;
    [SerializeField] private Vector3 offsetSpawnCharacter = new Vector3(0, 0, 4f);
    [SerializeField] private GameObject[] levelsPrefabs;
    private GameObject _currentLevel;
    private GameObject _carSkin;
    private GameObject _car;
    private List<GameObject> _character;

    private void Start()
    {
        _character = new List<GameObject>();
    }

    public void CreateLevel()
    {
        if (_currentLevel != null) Destroy(_currentLevel);
        var selectedLevel = levelsPrefabs[Random.Range(0, levelsPrefabs.Length - 1)];
        _currentLevel = Instantiate(selectedLevel, selectedLevel.transform.position, selectedLevel.transform.rotation);
    }

    public void CreateCar()
    {
        if (_car != null) Destroy(_car);
        _car = Instantiate(_carSkin, carSpawnPoint.position, _carSkin.transform.rotation);
        onCreateCar?.Invoke(_car);
    } 

    public void CreateCharacter(float powerForce, float angleForce)
    {
        _character.Add(Instantiate(characterSkin, _car.transform.position + offsetSpawnCharacter, Quaternion.Euler(60, _car.transform.eulerAngles.y, 0)));
        var chatacterCh = _character[_character.Count - 1].GetComponent<CharacterPlayer>();
        chatacterCh.PowerStartForce = powerForce * _car.GetComponent<Rigidbody>().velocity.magnitude;
        chatacterCh.AngleStartForce = angleForce;
        chatacterCh.AngleTurnCarY = _car.transform.eulerAngles.y;
        _car.GetComponent<CarPlayer>().BanControl();
        onCreateCharacter?.Invoke(_character[_character.Count - 1]);
    }

    public void DestroyCharacters()
    {
        for (int i = 0; i < _character.Count; i++)
        {
            Destroy(_character[i]);
        }
        _character.Clear();
    }

    public void OnSelect(GameObject carSkin) => _carSkin = carSkin;
}
