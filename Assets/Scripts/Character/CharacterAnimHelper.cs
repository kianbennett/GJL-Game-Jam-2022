using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimHelper : MonoBehaviour
{
    [SerializeField] private CharacterController Owner;

    public void ShootSteam()
    {
        (Owner as SteamCharacterController).SteamParticles.Play();
        AudioManager.Instance.SfxSteam.PlayAsSFX(Random.Range(0.9f, 1.1f));
    }

    public void Jump()
    {
        (Owner as SpringCharacterController).Jump();
    }
}
