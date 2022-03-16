using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkCharacterController : CharacterController
{
    [SerializeField] private Collider ColliderBig;
    [SerializeField] private Collider ColliderSmall;

    private bool IsSmall;

    public override void PerformAction()
    {
        base.PerformAction();

        IsSmall = !IsSmall;
        ModelAnimator.SetTrigger(IsSmall ? "ShrinkInto" : "ShrinkExit");
        ColliderBig.enabled = !IsSmall;
        ColliderSmall.enabled = IsSmall;
    }
}
