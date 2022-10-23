using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private GameObject[] backGround;
    private GameObject _backGroundPlaying;
    private int _numberBackgroundPlaying = 0;

    private void Start()
    {
        _backGroundPlaying = Instantiate(backGround[_numberBackgroundPlaying]);
    }

    public void SetSound(bool isOn) => AudioListener.volume = isOn ? 1 : 0;

    [ContextMenu("NextBackGround")]
    public void NextBackGround()
    {
        Destroy(_backGroundPlaying);
        _numberBackgroundPlaying++;
        _numberBackgroundPlaying = (int)Mathf.Repeat(_numberBackgroundPlaying, backGround.Length);
        _backGroundPlaying = Instantiate(backGround[_numberBackgroundPlaying]);
    }
}
