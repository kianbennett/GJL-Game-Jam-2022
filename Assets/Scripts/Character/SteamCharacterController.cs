using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamCharacterController : CharacterController
{
    public ParticleSystem SteamParticles;
    [SerializeField] private GameObject SteamCollider;
    [SerializeField] private Transform Wheel;
    [SerializeField] private Transform[] Pistons;
    [SerializeField] private AnimationCurve PistonScaleCurve;
    
    private bool IsSteaming;
    private float[] PistonOffsets;

    protected override void Awake()
    {
        base.Awake();
        PistonOffsets = new float[Pistons.Length];
        for(int i = 0; i < PistonOffsets.Length; i++)
        {
            PistonOffsets[i] = Random.Range(0f, 1f);
        }
    }

    protected override void Update()
    {
        base.Update();

        Wheel.Rotate(Vector3.forward, Time.deltaTime * 80 * Rigidbody.velocity.magnitude, Space.Self);
        for(int i = 0; i < Pistons.Length; i++)
        {
            Transform Piston = Pistons[i];
            float PistonSpeed = IsSteaming ? 2 : 1;
            // float PistonScale = 0.6f * Mathf.Sin(Time.time * PistonSpeed + PistonOffsets[i]);
            float PistonScale = PistonScaleCurve.Evaluate((Time.time * PistonSpeed + PistonOffsets[i]) % 1f);
            Piston.localScale = new Vector3(Piston.localScale.x, Piston.localScale.y, PistonScale);
        }
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
