using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) // if the player collides with the checkpoint
        {
            other.gameObject.GetComponent<PlayerController>().SetCheckpoint(transform.position); // set the checkpoint for the player
        }
    }
}