using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlayer : MonoBehaviour
{
    [SerializeField] private Rigidbody pointAddForce;
    [SerializeField] private float factorForceFlyFormCar = 500f;
    [SerializeField] private float factorForceTaxiing = 10000;
    [SerializeField] private GameObject glassSound;
    [SerializeField] private GameObject cryDriversSound;
    [SerializeField] private GameObject ApplauseSound;
    private LeaderBoard _leaderBoard;
    private int _scoreTarget = -1;
    private float _timerDelayAfterCounting;
    private bool _isCrashedWithCollision;
    private Rigidbody _characterPlayerRB;
    private bool _isPushed;
    private bool _isBannedPushing = true;
    private InputController _inputController;
    private bool _isStartedDelayPushCoroutine;
    readonly float _delayAfterCounting = 1f;

    public float PowerStartForce { get; set; }

    public float AngleStartForce { get; set; }

    public float AngleTurnCarY { get; set; }

    private void Start()
    {
        _leaderBoard = FindObjectOfType<LeaderBoard>();
        FlyOutCar();
        _characterPlayerRB = GetComponent<Rigidbody>();
        _inputController = FindObjectOfType<InputController>();
    }

    private void Update()
    {
        if (_scoreTarget > -1) _timerDelayAfterCounting += Time.deltaTime;
        if (_timerDelayAfterCounting > _delayAfterCounting)
        {
            _leaderBoard.StartLeaderBoard(_scoreTarget, false);
            GetComponent<CharacterPlayer>().enabled = false;
        }
        if (_inputController.Horizontal == 0 && _inputController.Vertical == 0) _isBannedPushing = false;
        if (!_isPushed && !_isStartedDelayPushCoroutine && !_isBannedPushing &&
            (_inputController.Horizontal != 0 || _inputController.Vertical != 0)) StartCoroutine("DelayPush");
    }

    private void FlyOutCar()
    {
        var vectorForce = new Vector3(Mathf.Cos(AngleStartForce * Mathf.PI / 2) * Mathf.Sin(AngleTurnCarY * Mathf.Deg2Rad),
            Mathf.Sin(AngleStartForce * Mathf.PI / 2),
            Mathf.Cos(AngleStartForce * Mathf.PI / 2) * Mathf.Cos(AngleTurnCarY * Mathf.Deg2Rad)) * PowerStartForce * factorForceFlyFormCar;
        pointAddForce.AddForce(vectorForce, ForceMode.Impulse);

        var glassSoundPlaying = Instantiate(glassSound);
        Destroy(glassSoundPlaying, glassSoundPlaying.GetComponent<AudioSource>().clip.length);
        var cryDriversSoundPlaying = Instantiate(cryDriversSound);
        Destroy(cryDriversSoundPlaying, cryDriversSoundPlaying.GetComponent<AudioSource>().clip.length);
    }

    private void Push(Vector2 direction)
    {
        _characterPlayerRB.AddForce((Vector3)direction * factorForceTaxiing);
        _isPushed = true;
    }

    private void OnCharacterCrashed()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        _isCrashedWithCollision = true;
        var applauseSoundPlaying = Instantiate(ApplauseSound);
        Destroy(applauseSoundPlaying, applauseSoundPlaying.GetComponent<AudioSource>().clip.length);
    }

    private IEnumerator DelayPush()
    {
        _isStartedDelayPushCoroutine = true;
        yield return new WaitForSeconds(0.2f);
        Push(new Vector2(_inputController.Horizontal, _inputController.Vertical).normalized);
        _isStartedDelayPushCoroutine = false;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (_isCrashedWithCollision) return;
        if (collision.gameObject.tag == "Target")
        {
            OnCharacterCrashed();
        }
        if (collision.gameObject.tag == "Ground")
        {
            _leaderBoard.StartLeaderBoard(0, false);
            OnCharacterCrashed();
            GetComponent<CharacterPlayer>().enabled = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Target")
        {
            if (_scoreTarget < int.Parse(other.gameObject.name)) 
                _scoreTarget = int.Parse(other.gameObject.name);
        }
    }
}
