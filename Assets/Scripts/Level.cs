using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private MeshRenderer LevelOverlay;
    [SerializeField] private Collider LevelOverlayCollider;
    [SerializeField] private float RevealSpeed, RevealOverlaySpeed;
    [SerializeField] private float RevealHeightOffset;
    
    private bool HideOverlay;
    private bool IsActive;

    void Update()
    {
        SetOverlayAlpha(Mathf.MoveTowards(LevelOverlay.material.color.a, HideOverlay ? 0 : 1, Time.deltaTime * (1f / RevealOverlaySpeed)));

        if(Input.GetKeyDown(KeyCode.Space))
        {
            ShowLevel();
        }
    }

    private void SetOverlayAlpha(float Alpha)
    {
        Material Mat = LevelOverlay.material;
        Mat.color = new Color(Mat.color.r, Mat.color.g, Mat.color.b, Alpha);
        LevelOverlay.material = Mat;
    }

    public void ShowLevel()
    {
        if(!IsActive)
        {
            StartCoroutine(ShowLevelIEnum());    
        }
    }

    private IEnumerator ShowLevelIEnum()
    {
        IsActive = true;
        HideOverlay = true;

        float OriginalLevelHeight = transform.position.y;
        transform.position -= Vector3.up * RevealHeightOffset;
        LevelOverlayCollider.enabled = true;

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
