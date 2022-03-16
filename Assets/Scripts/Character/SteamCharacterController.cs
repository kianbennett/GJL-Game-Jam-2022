using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamCharacterController : CharacterController
{
    public ParticleSystem SteamParticles;
    public GameObject SteamCollider;

    private bool IsSteaming;

    protected override void Update()
    {
        base.Update();
    }

    public override void PerformAction()
    {
        base.PerformAction();

        SteamCollider.SetActive(!SteamCollider.activeSelf);
        IsSteaming = !IsSteaming;
        ModelAnimator.SetTrigger(IsSteaming ? "SteamInto" : "SteamExit");
        if(!IsSteaming) SteamParticles.Stop();
        CanMove = !IsSteaming;
    }
}
