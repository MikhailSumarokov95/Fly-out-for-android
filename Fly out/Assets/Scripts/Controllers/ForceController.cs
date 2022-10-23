using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ForceController : MonoBehaviour
{
    [SerializeField] private Slider sliderMagnitudePowerForce;
    [SerializeField] private GameObject arrowMagnitudeAngleForce;
    [SerializeField] private float _factorChanges = 1;
    public UnityEvent<float, float> onChoiceForceFinished;
    private InputController _inputControler;
    private bool _isBanedCoroutineSelectForce;
    private bool _isPausedSelectForce;

    private void Start()
    {
        _inputControler = FindObjectOfType<InputController>();
    }

    private void Update()
    {
        if (_inputControler.TouchDownForSelectForce && !_isBanedCoroutineSelectForce)
            StartCoroutine("SelectForce");
    }

    public void PauseSelectPower(bool value) => _isPausedSelectForce = value;

    public void ResetPower()
    {
        sliderMagnitudePowerForce.value = 0;
        arrowMagnitudeAngleForce.transform.rotation = Quaternion.identity;
        _isBanedCoroutineSelectForce = false;
        _isPausedSelectForce = false;
        StopCoroutine("SelectForce");
    }

    private IEnumerator SelectForce()
    {
        var magnitudePowerForce = 0f;
        var magnitudeAngleForce = 0f;
        _isBanedCoroutineSelectForce = true;
        while (!_inputControler.TouchUpForSelectForce)
        {
            if (_isPausedSelectForce) yield return null;
            magnitudePowerForce += Time.deltaTime * _factorChanges;
            sliderMagnitudePowerForce.value = Mathf.PingPong(magnitudePowerForce, 1);
            yield return null;
        }

        yield return new WaitUntil(() => _inputControler.TouchDownForSelectForce);

        while (!_inputControler.TouchUpForSelectForce)
        {
            if (_isPausedSelectForce) yield return null;
            magnitudeAngleForce += Time.deltaTime * _factorChanges;
            arrowMagnitudeAngleForce.transform.eulerAngles = new Vector3(0, 0, Mathf.PingPong(magnitudeAngleForce, 1) * 90);
            yield return null;
        }
        onChoiceForceFinished?.Invoke(sliderMagnitudePowerForce.value, arrowMagnitudeAngleForce.transform.eulerAngles.z / 90);
    }
}
