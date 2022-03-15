using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraController : Singleton<CameraController>
{
    [SerializeField] private Transform Target;
    [SerializeField] private Volume PostProcessVolume;

    public Camera MainCamera { get; private set; }

    private Coroutine CameraShakeCoroutine;

    protected override void Awake()
    {
        base.Awake();
        MainCamera = Camera.main;
    }

    void LateUpdate()
    {
        if(Target)
        {
            Vector3 TargetPos = Target.position;
            TargetPos.y = 0;
            transform.position = TargetPos;
        }
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