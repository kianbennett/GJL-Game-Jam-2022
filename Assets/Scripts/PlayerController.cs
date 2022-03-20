using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] private CharacterController[] Characters;

    [HideInInspector] public int ActiveCharacter;
    [HideInInspector] public bool HasStarted;

    [SerializeField] private Vector3 CameraLocalPositionInit;
    [SerializeField] private Vector3 CameraLocalRotationInit;

    private bool Paused;
    private Level CurrentLevel;
    private Level PreviousLevel;

    protected override void Awake()
    {
        SwitchToCharacter(1, false);

        Application.targetFrameRate = 60;
        AudioManager.Instance.MusicMainMenu.PlayAsMusic();

        CameraController.Instance.SetOverrideLocalCameraPos(CameraLocalPositionInit);
        CameraController.Instance.SetOverrideLocalCameraRot(CameraLocalRotationInit);
        CameraController.Instance.SetPostProcessingEffectEnabled<UnityEngine.Rendering.Universal.DepthOfField>(true);
    }

    void Update()
    {
        if(HasStarted)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1)) SwitchToCharacter(0);
            if(Input.GetKeyDown(KeyCode.Alpha2)) SwitchToCharacter(1);
            if(Input.GetKeyDown(KeyCode.Alpha3)) SwitchToCharacter(2);

            if(Input.GetKeyDown(KeyCode.Space))
            {
                Characters[ActiveCharacter].PerformAction();
            }

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                SetPaused(!Paused);
            }
        }
    }

    private void SwitchToCharacter(int Character, bool PlaySFX = true)
    {
        if(CameraController.Instance.IsInCutscene()) return;

        Characters[ActiveCharacter].SetAsActiveController(false);
        ActiveCharacter = Character;
        Characters[ActiveCharacter].SetAsActiveController(true);
        CameraController.Instance.SetTarget(Characters[ActiveCharacter].transform);

        if(PlaySFX) AudioManager.Instance.SfxCharacterChange.PlayAsSFX(Random.Range(0.5f, 0.6f));
    }

    public bool IsPaused()
    {
        return Paused;
    }

    public void SetPaused(bool Paused)
    {
        this.Paused = Paused;
        MenuManager.Instance.PauseMenu.gameObject.SetActive(Paused);
        MenuManager.Instance.CharacterPreview.gameObject.SetActive(!Paused);
        Time.timeScale = Paused ? 0 : 1;
        if(Paused)
        {
            AudioManager.Instance.FadeOutMusic();    
        }
        else
        {
            AudioManager.Instance.FadeInMusic();
        }
    }

    public void StartGame()
    {
        StartCoroutine(StartGameIEnum());
    }

    private IEnumerator StartGameIEnum()
    {
        CameraController.Instance.SetOverrideLocalCameraPos(null);
        CameraController.Instance.SetOverrideLocalCameraRot(null);
        CameraController.Instance.SetPostProcessingEffectEnabled<UnityEngine.Rendering.Universal.DepthOfField>(false);
        AudioManager.Instance.FadeOutMusic();
        CameraController.Instance.MaxSpeed *= 0.25f;
        CameraController.Instance.SmoothTime *= 0.5f;
        yield return new WaitForSeconds(0.5f);

        AudioManager.Instance.MusicLevel.PlayAsMusic();
        
        AudioManager.Instance.FadeInMusic();

        yield return new WaitForSeconds(1.2f);

        CameraController.Instance.MaxSpeed *= 4f;
        CameraController.Instance.SmoothTime *= 2f;
        MenuManager.Instance.HelpMenu.Open(false);

        while(MenuManager.Instance.HelpMenu.gameObject.activeInHierarchy)
        {
            yield return null;
        }

        HasStarted = true;
    }

    public void SetCurrentLevel(Level Level)
    {
        if(CurrentLevel != null)
        {
            PreviousLevel = CurrentLevel;
        }
        CurrentLevel = Level;
    }

    public Level GetPreviousLevel()
    {
        return PreviousLevel;
    }
}
