using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathArea : MonoBehaviour
{
    [Header("Spawn Point Game Object")]
    public GameObject spawnPoint;

    //info
    //apply this script to a game object with a collider, make sure you set it's collider to trigger = true
    //this script takes in a game object which acts as a spawn point to send the player too if they collide with the game object this script is attached too
    public void OnTriggerEnter(Collider other) // looks for trigger
    {

        if (other.gameObject.tag == "Player") // checks if it has collided with anything Tagged "Player"
        {
            other.transform.position = spawnPoint.transform.position; // sends collided player to spawn point location
        }

    }
}
