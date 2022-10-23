using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartBodyCharacter : MonoBehaviour
{
    [SerializeField] private CharacterPlayer player;

    private void OnCollisionEnter(Collision collision) => player.OnCollisionEnter(collision);

    private void OnTriggerEnter(Collider other) => player.OnTriggerEnter(other);
}
