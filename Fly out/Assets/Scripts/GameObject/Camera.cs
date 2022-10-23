using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private float damping = 1f;
    [SerializeField] private Vector3 offset = new Vector3(0, 3f, -4.5f);
    private Transform _target;

    private void FixedUpdate()
    {
        if (_target == null) return;
        var currentPosition = transform.position;
        var desiredPosition = _target.position + (_target.rotation * offset);
        transform.position = Vector3.Lerp(currentPosition, desiredPosition, Time.deltaTime * damping);
        transform.LookAt(_target.transform);
    }

    public void SetTarget(GameObject target) => _target = target.transform.Find("TargetCamera");
}
