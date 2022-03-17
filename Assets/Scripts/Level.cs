using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private MeshRenderer LevelOverlay;
    [SerializeField] private Collider LevelOverlayCollider;
    [SerializeField] private float RevealSpeed = 1.0f, RevealOverlaySpeed = 0.6f;
    [SerializeField] private float RevealHeightOffset = 1.7f;
    
    private bool HideOverlay;
    private bool IsActive;

    void Update()
    {
        SetOverlayAlpha(Mathf.MoveTowards(LevelOverlay.material.color.a, HideOverlay ? 0 : 1, Time.deltaTime * (1f / RevealOverlaySpeed)));
    }

    private void SetOverlayAlpha(float Alpha)
    {
        Material Mat = LevelOverlay.material;
        Mat.color = new Color(Mat.color.r, Mat.color.g, Mat.color.b, Alpha);
        LevelOverlay.material = Mat;
    }

    public void ShowLevel(float Delay = 0f)
    {
        if(!IsActive)
        {
            StartCoroutine(ShowLevelIEnum(Delay));    
        }
    }

    private IEnumerator ShowLevelIEnum(float Delay = 0f)
    {
        IsActive = true;
        HideOverlay = true;

        float OriginalLevelHeight = transform.position.y;
        transform.position -= Vector3.up * RevealHeightOffset;
        LevelOverlayCollider.enabled = true;

        yield return new WaitForSeconds(Delay);

        while(true)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, OriginalLevelHeight, transform.position.z), Time.deltaTime * RevealSpeed);
            if(Mathf.Approximately(transform.position.y, OriginalLevelHeight))
            {
                break;
            }
            yield return null;
        }

        LevelOverlayCollider.enabled = false;
    }
}
