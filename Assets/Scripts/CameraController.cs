using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraController : Singleton<CameraController>
{
    [SerializeField] private Transform Target;
    [SerializeField] private float SmoothTime, MaxSpeed;
    [SerializeField] private Volume PostProcessVolume;

    public Camera MainCamera { get; private set; }

    private Coroutine CameraShakeCoroutine;
    private bool SmoothToTarget;

    protected override void Awake()
    {
        base.Awake();
        MainCamera = Camera.main;
    }

    void LateUpdate()
    {
        if(Target)
        {
            float DistToTarget = Vector3.Distance(transform.position, Target.position);

            if(SmoothToTarget)
            {
                Vector3 vel = Vector3.zero;
                transform.position = Vector3.SmoothDamp(transform.position, Target.position, ref vel, SmoothTime, MaxSpeed);
            }
            else
            {
                transform.position = Target.position;
            }
            
            if(DistToTarget < 0.2f)
            {
                SmoothToTarget = false;
            }
        }
    }

    public void SetTarget(Transform Target)
    {
        this.Target = Target;
        SmoothToTarget = true;
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

    public void SetPostProcessingEffectEnabled<T>(bool enabled) where T : VolumeComponent 
    {
        PostProcessVolume.profile.TryGet<T>(out T Component);
        if(Component != null) 
        {
            Component.active = enabled;
        }
    }

    public void SetAntialiasingEnabled(bool Enabled) 
    {
        QualitySettings.antiAliasing = Enabled ? 2 : 0;
    }
}