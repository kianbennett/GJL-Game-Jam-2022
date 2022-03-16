using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimHelper : MonoBehaviour
{
    [SerializeField] private CharacterController Owner;

    public void ShootSteam()
    {
        (Owner as SteamCharacterController).SteamParticles.Play();
    }

    public void Jump()
    {
        (Owner as SpringCharacterController).Jump();
    }
}
