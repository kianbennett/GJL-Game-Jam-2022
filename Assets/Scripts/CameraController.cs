using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class CameraController : Singleton<CameraController>
{
    public float SmoothTime, MaxSpeed;
    [SerializeField] private float ZoomSpeed;
    [SerializeField] private float MinCameraDist, MaxCameraDist;
    [SerializeField] private Volume PostProcessVolume;
    [SerializeField] private Vector3 DefaultCameraRotation;
    [SerializeField] private Vector3 DefaultCameraPosition;

    public Camera MainCamera { get; private set; }

    private Transform Target;
    private Vector3 TargetPosition;
    private bool UsePositionAsTarget;

    private Coroutine CameraShakeCoroutine;
    private bool SmoothToTarget;
    private Vector3 TargetToCameraDir;
    private float CameraDist;
    private bool InCutscene;
    private Vector3? OverrideLocalCameraPos;
    private Vector3? OverrideLocalCameraRot;

    private Coroutine CutsceneCoroutine;

    protected override void Awake()
    {
        base.Awake();
        MainCamera = Camera.main;
        TargetToCameraDir = DefaultCameraPosition.normalized;
        CameraDist = DefaultCameraPosition.magnitude;
    }

    void LateUpdate()
    {
        if(Target)
        {
            float DistToTarget = Vector3.Distance(transform.position, GetTargetPosition());

            if(SmoothToTarget)
            {
                Vector3 vel = Vector3.zero;
                transform.position = Vector3.SmoothDamp(transform.position, GetTargetPosition(), ref vel, SmoothTime, MaxSpeed);
            }
            else
            {
                transform.position = GetTargetPosition();
            }
            
            if(DistToTarget < 0.2f)
            {
                SmoothToTarget = false;
            }

            // if(Input.mouseScrollDelta.y != 0)
            // {
            //     Zoom(Input.mouseScrollDelta.y);
            // }

            if(OverrideLocalCameraPos.HasValue)
            {
                MainCamera.transform.localPosition = OverrideLocalCameraPos.Value;
            } 
            else
            {
                MainCamera.transform.localPosition = 
                    Vector3.Lerp(MainCamera.transform.localPosition, TargetToCameraDir * CameraDist, Time.deltaTime * 5);
            }

            if(OverrideLocalCameraRot.HasValue)
            {
                MainCamera.transform.localRotation = Quaternion.Euler(OverrideLocalCameraRot.Value);
            }
            else
            {
                MainCamera.transform.localRotation = 
                    Quaternion.RotateTowards(MainCamera.transform.localRotation, Quaternion.Euler(DefaultCameraRotation), Time.deltaTime * 60f);
            }
        }
    }

    public void SetTarget(Transform Target)
    {
        this.Target = Target;
        SmoothToTarget = true;
        UsePositionAsTarget = false;
    }

    public void SetTarget(Vector3 Position)
    {
        this.TargetPosition = Position;
        SmoothToTarget = true;
        UsePositionAsTarget = true;
    }

    private Vector3 GetTargetPosition()
    {
        return UsePositionAsTarget ? TargetPosition : Target.position;
    }

    public void ShakeCamera(float Distance, float Interval, float Duration)
    {
        if(CameraShakeCoroutine != null) StopCoroutine(CameraShakeCoroutine);
        StartCoroutine(CameraShakeIEnum(Distance, Interval, Duration));
    }

    private IEnumerator CameraShakeIEnum(float Distance, float Interval, float Duration)
    {
        float TimeRemaining = Duration;
        while(TimeRemaining > 0)
        {
            TimeRemaining -= Interval;
            float OffsetX = Random.Range(0, Distance * (TimeRemaining / Duration));
            float OffsetY = Random.Range(0, Distance * (TimeRemaining / Duration));
            MainCamera.transform.localPosition = new Vector2(OffsetX, OffsetY);
            yield return new WaitForSeconds(Interval);
        }
        MainCamera.transform.localPosition = Vector3.zero;
    }

    public void Zoom(float delta)
    {
        CameraDist = Mathf.Clamp(CameraDist - delta * Time.deltaTime * ZoomSpeed, MinCameraDist, MaxCameraDist);
    }

    public void SetPostProcessingEffectEnabled<T>(bool enabled) where T : VolumeComponent 
    {
        PostProcessVolume.profile.TryGet<T>(out T Component);
        if(Component != null) 
        {
            Component.active = enabled;
        }
    }

    public void StartCutscene(float InitialDelay, float Duration, UnityAction Callback, params Vector3[] Positions)
    {
        if(Positions.Length == 0) 
        {
            Debug.LogWarning("Trying to show cutscene with no positions");
            return;
        }
        if(CutsceneCoroutine != null) StopCoroutine(CutsceneCoroutine);
        CutsceneCoroutine = StartCoroutine(StartCutsceneIEnum(InitialDelay, Duration, Callback, Positions));
    }

    private IEnumerator StartCutsceneIEnum(float InitialDelay, float Duration, UnityAction Callback, Vector3[] Positions)
    {
        InCutscene = true;

        yield return new WaitForSeconds(InitialDelay);

        Transform PrevTarget = Target;

        Vector3 AveragePosition = Vector3.zero;
        foreach(Vector3 Position in Positions)
        {
            AveragePosition += Position;
        }
        AveragePosition /= Positions.Length;
        SetTarget(AveragePosition);

        while(SmoothToTarget) yield return null;

        Callback.Invoke();

        yield return new WaitForSeconds(Duration);

        SetTarget(PrevTarget);
        InCutscene = false;
    }

    public void SetOverrideLocalCameraPos(Vector3? OverrideLocalCameraPos)
    {
        this.OverrideLocalCameraPos = OverrideLocalCameraPos;
    }

    public void SetOverrideLocalCameraRot(Vector3? OverrideLocalCameraRot)
    {
        this.OverrideLocalCameraRot = OverrideLocalCameraRot;
    }

    public bool IsInCutscene()
    {
        return InCutscene;
    }
}