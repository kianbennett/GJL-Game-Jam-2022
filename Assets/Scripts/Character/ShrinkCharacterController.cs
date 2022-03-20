using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkCharacterController : CharacterController
{
    [SerializeField] private Collider ColliderBig;
    [SerializeField] private Collider ColliderSmall;
    [SerializeField] private Transform[] Wheels;

    private bool IsSmall;

    protected override void Update()
    {
        base.Update();

        foreach(Transform Wheel in Wheels)
        {
            Wheel.Rotate(Vector3.up, Time.deltaTime * 80 * Rigidbody.velocity.magnitude, Space.Self);
        }
    }

    public override void PerformAction()
    {
        base.PerformAction();

        IsSmall = !IsSmall;
        ModelAnimator.SetTrigger(IsSmall ? "ShrinkInto" : "ShrinkExit");
        ColliderBig.enabled = !IsSmall;
        ColliderSmall.enabled = IsSmall;
        if(IsSmall) AudioManager.Instance.SfxShrink.PlayAsSFX(Random.Range(0.9f, 1.1f));
            else AudioManager.Instance.SfxGrow.PlayAsSFX(Random.Range(0.9f, 1.1f));
    }
}
