using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringCharacterController : CharacterController
{
    [SerializeField] private float JumpSpeed;
    [SerializeField] private float GravityAcceleration;

    private bool IsGrounded;

    protected override void Awake()
    {
        base.Awake();
        IsGrounded = true;
    }

    protected override void Update()
    {
        base.Update();

        Vector3 Velocity = Rigidbody.velocity;
        if(!IsGrounded)
        {
            Velocity.y += GravityAcceleration * Time.deltaTime;
        }
        Rigidbody.velocity = Velocity;
    }

    public override void PerformAction()
    {
        base.PerformAction();

        if(IsGrounded)
        {
            IsGrounded = false;
            ModelAnimator.SetTrigger("JumpInto");
        }
    }

    public void Jump()
    {
        Rigidbody.velocity = new Vector3(Rigidbody.velocity.x, JumpSpeed, Rigidbody.velocity.z);
    }

    void OnCollisionEnter(Collision other) 
    {
        if(IsCollisionWithGround(other)) 
        {
            if(!IsGrounded)
            {
                ModelAnimator.SetTrigger("JumpExit");
                IsGrounded = true;
            }
        }
    }

    // Gets the angle of the collision normal (the vector perpendicular to the surface), if it's below 45 then it counts as a flat surface (i.e. ground)
    private bool IsCollisionWithGround(Collision Collision) 
    {
        for(int i = 0; i < Collision.contactCount; i++) 
        {
            ContactPoint Contact = Collision.contacts[0];
            float Angle = Vector3.Angle(Vector3.up, Contact.normal);
            if(Angle < 45) 
            {
                return true;
            }
        }
        return false;
    }
}
