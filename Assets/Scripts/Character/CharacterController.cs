using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float TurnSpeed;

    [Header("Appearance")]
    [SerializeField] protected Transform ModelTransform;
    [SerializeField] protected Animator ModelAnimator;

    [Header("Audio")]
    [SerializeField] protected AudioSource AudioEngineIdle;
    [SerializeField] protected AudioSource AudioEngineRunning;
    
    private bool IsActive;
    protected Rigidbody Rigidbody;
    private Vector3 Movement;
    private Quaternion TargetModelRotation;
    protected bool CanMove;

    private PlayerControls Controls;
    private InputAction InputActionMove;

    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        TargetModelRotation = Quaternion.identity;
        CanMove = true;
    }

    void OnEnable()
    {
        Controls = new PlayerControls();
        InputActionMove = Controls.Player.Move;
        InputActionMove.Enable();
    }

    protected virtual void Update()
    {
        if(!Rigidbody) return;

        Vector2 MoveInput = Vector2.zero;

        if(IsActive && PlayerController.Instance.HasStarted && CanMove && !CameraController.Instance.IsInCutscene())
        {
            MoveInput = InputActionMove.ReadValue<Vector2>();
            // MoveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
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

        Vector3 Velocity = MoveInput != Vector2.zero ? Movement * MoveSpeed : Vector3.zero;
        Velocity.y = Rigidbody.velocity.y;
        Rigidbody.velocity = Velocity;

        if(ModelAnimator.GetCurrentAnimatorStateInfo(0).IsName("AnimIdle"))
        {
            ModelAnimator.speed = MoveInput != Vector2.zero ? 2f : 1f;
        }
        else
        {
            ModelAnimator.speed = 1;
        }

        AudioEngineRunning.mute = !IsActive || MoveInput == Vector2.zero;
        
        // if(IsActive && MoveInput != Vector2.zero && !AudioEngineRunning.isPlaying)
        // {
        //     AudioEngineRunning.mute =
        // }
        // else if(AudioEngineRunning.isPlaying)
        // {
        //     AudioEngineRunning.Stop();
        // }
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