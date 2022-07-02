using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringCharacterController : CharacterController
{
    [SerializeField] private float JumpSpeed;
    [SerializeField] private float GravityAcceleration;
    [SerializeField] private Transform Wheel;
    [SerializeField] private Transform[] Gears;

    private bool IsGrounded;
    private bool IsJumping;
    private Coroutine ResetJumpCoroutine;

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

        // For the wheel use the horizontal velocity only
        Velocity.y = 0;
        Wheel.Rotate(Vector3.up, Time.deltaTime * -75 * Velocity.magnitude, Space.Self);

        for(int i = 0; i < Gears.Length; i++)
        {
            Gears[i].Rotate(Vector3.right, Time.deltaTime * 160 * (i == 0 ? 1 : -1), Space.Self);
        }
    }

    public override void PerformAction()
    {
        base.PerformAction();

        if(!IsJumping)
        {
            IsJumping = true;
            ModelAnimator.SetTrigger("JumpInto");
            if(ResetJumpCoroutine != null) StopCoroutine(ResetJumpCoroutine);
            StartCoroutine(ResetJumpIEnum());
            Debug.Log("Jump");
        }
    }

    public void Jump()
    {
        if(!IsGrounded) return;

        IsGrounded = false;
        Rigidbody.velocity = new Vector3(Rigidbody.velocity.x, JumpSpeed, Rigidbody.velocity.z);
        AudioManager.Instance.SfxJump.PlayAsSFX(Random.Range(1.6f, 1.8f));
        if(ResetJumpCoroutine != null) StopCoroutine(ResetJumpCoroutine);
    }

    void OnCollisionEnter(Collision other) 
    {
        if(IsCollisionWithGround(other)) 
        {
            if(!IsGrounded)
            {
                ModelAnimator.SetTrigger("JumpExit");
                IsGrounded = true;
                IsJumping = false;
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

    // Dirty hack to fix the bug of not being grounded sometimes, if they haven't landed on the ground 3s after jumping
    // just reset the grounded state lol
    private IEnumerator ResetJumpIEnum()
    {
        yield return new WaitForSeconds(3.0f);
        IsJumping = false;
    }
}
