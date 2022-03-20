using UnityEngine;
using UnityEngine.UI;

public class CharacterPreviewCard : MonoBehaviour 
{
    [SerializeField] private Image OutlineImage;
    [SerializeField] private CanvasGroup CanvasGroup;
    [SerializeField] private Animator CharacterAnimator;

    private bool IsActive;

    void Update()
    {
        transform.localScale = Vector3.one * Mathf.Lerp(transform.localScale.x, IsActive ? 1f : 0.85f, Time.deltaTime * 10);
    }

    public void SetActive(bool Active)
    {
        IsActive = Active;
        OutlineImage.enabled = IsActive;
        CanvasGroup.enabled = !IsActive;
        CharacterAnimator.enabled = IsActive;
    }
}