using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float TurnSpeed;

    [Header("Appearance")]
    [SerializeField] private Transform ModelTransform;
    
    private Rigidbody Rigidbody;
    private Vector3 Movement;

    void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(!Rigidbody) return;

        Vector2 MoveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if(MoveInput.magnitude > 1) MoveInput.Normalize();

        Movement = CameraController.Instance.transform.rotation * new Vector3(MoveInput.x, 0, MoveInput.y);

        Vector3 Velocity = Vector3.zero;

        if(Movement != Vector3.zero)
        {
            Quaternion TargetModelRotation = Quaternion.LookRotation(Movement);
            ModelTransform.rotation = Quaternion.Lerp(ModelTransform.rotation, TargetModelRotation, Time.deltaTime * TurnSpeed);
            // ModelTransform.rotation = Quaternion.RotateTowards(ModelTransform.rotation, TargetModelRotation, Time.deltaTime * TurnSpeed);
            float Angle = Quaternion.Angle(ModelTransform.rotation, Quaternion.LookRotation(Movement));

            Velocity = ModelTransform.forward * MoveSpeed;
        }

        Rigidbody.velocity = Velocity;
    }
}