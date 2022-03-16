using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float TurnSpeed;

    [Header("Appearance")]
    [SerializeField] protected Transform ModelTransform;
    [SerializeField] protected Animator ModelAnimator;
    
    private bool IsActive;
    protected Rigidbody Rigidbody;
    private Vector3 Movement;
    private Quaternion TargetModelRotation;
    protected bool CanMove;

    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        TargetModelRotation = Quaternion.identity;
        CanMove = true;
    }

    protected virtual void Update()
    {
        if(!Rigidbody) return;

        Vector2 MoveInput = Vector2.zero;

        if(IsActive && CanMove)
        {
            MoveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if(MoveInput.magnitude > 1) MoveInput.Normalize();
        }

        if(MoveInput != Vector2.zero)
        {
            Movement = CameraController.Instance.transform.rotation * new Vector3(MoveInput.x, 0, MoveInput.y);
            TargetModelRotation = Quaternion.LookRotation(Movement);

            ModelTransform.rotation = Quaternion.Lerp(ModelTransform.rotation, TargetModelRotation, Time.deltaTime * TurnSpeed);
            // ModelTransform.rotation = Quaternion.RotateTowards(ModelTransform.rotation, TargetModelRotation, Time.deltaTime * TurnSpeed);
            float Angle = Quaternion.Angle(ModelTransform.rotation, TargetModelRotation);
        }

        Vector3 Velocity = MoveInput != Vector2.zero ? ModelTransform.forward * MoveSpeed : Vector3.zero;
        Velocity.y = Rigidbody.velocity.y;
        Rigidbody.velocity = Velocity;

        ModelAnimator.speed = MoveInput != Vector2.zero ? 2f : 1f;
    }

    public void SetAsActiveController(bool Active)
    {
        IsActive = Active;
    }

    public virtual void PerformAction()
    {
    }

    public virtual void Interact()
    {
    }
}