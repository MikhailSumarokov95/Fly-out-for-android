using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guade : MonoBehaviour
{
    [SerializeField] private GameObject guadeCar;
    [SerializeField] private GameObject guadeCharacter;

    public void StartGuadeCar()
    {
        if (PlayerPrefs.GetInt("guadeCar", 0) == 1) return;
        guadeCar.SetActive(true);
    }

    public void StartGuadeCharacter()
    {
        if (PlayerPrefs.GetInt("guadeCharacter", 0) == 1) return;
        guadeCharacter.SetActive(true);
        Time.timeScale = 0;
    }

    public void StopGuadeCar()
    {
        guadeCar.SetActive(false);   
        PlayerPrefs.SetInt("guadeCar", 1);
    }

    public void StopGuadeCharacter()
    {
        guadeCharacter.SetActive(false);
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
