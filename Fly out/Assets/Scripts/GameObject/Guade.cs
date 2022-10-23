using System.Collections;
using System.Collections.Generic;
using ToxicFamilyGames.YandexSDK;
using UnityEngine;

public class Guade : MonoBehaviour
{
    [SerializeField] private GameObject mobileGuadeCar;
    [SerializeField] private GameObject mobileGuadeCharacter;
    [SerializeField] private GameObject pcGuadeCharacter;
    [SerializeField] private GameObject pcGuadeCar;
    private GameObject _guadeCar;
    private GameObject _guadeCharacter;

    private void Start()
    {
        if (AuthorizationYandex.IsMobile()) 
        {
            _guadeCar = mobileGuadeCar;
            _guadeCharacter = mobileGuadeCharacter;
        }
        else
        {
            _guadeCar = pcGuadeCar;
            _guadeCharacter = pcGuadeCharacter;
        }
    }

    public void StartGuadeCar()
    {
        if (PlayerPrefs.GetInt("guadeCar", 0) == 1) return;
        _guadeCar.SetActive(true);
    }

    public void StartGuadeCharacter()
    {
        if (PlayerPrefs.GetInt("guadeCharacter", 0) == 1) return;
        _guadeCharacter.SetActive(true);
        Time.timeScale = 0;
    }

    public void StopGuadeCar()
    {
        _guadeCar.SetActive(false);   
        PlayerPrefs.SetInt("guadeCar", 1);
    }

    public void StopGuadeCharacter()
    {
        _guadeCharacter.SetActive(false);
        PlayerPrefs.SetInt("guadeCharacter", 1);
        Time.timeScale = 1;
    }

#if UNITY_EDITOR
    [ContextMenu("ResetGuade")]
    public void ResetGuade()
    {
        PlayerPrefs.SetInt("guadeCharacter", 0);
        PlayerPrefs.SetInt("guadeCar", 0);
    }
#endif
}
