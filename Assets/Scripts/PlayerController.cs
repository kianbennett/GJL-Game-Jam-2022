using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] private CharacterController[] Characters;

    [HideInInspector] public int ActiveCharacter;
    [HideInInspector] public bool HasStarted;

    [SerializeField] private Vector3 CameraLocalPositionInit;
    [SerializeField] private Vector3 CameraLocalRotationInit;

    private PlayerControls Controls;
    private InputAction InputActionNextCharacter;
    private InputAction InputActionPrevCharacter;
    private InputAction InputActionPerformAction;
    private InputAction InputActionPause;

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

        Controls = new PlayerControls();
    }

    void OnEnable()
    {
        InputActionNextCharacter = Controls.Player.NextCharacter;
        InputActionPrevCharacter = Controls.Player.PrevCharacter;
        InputActionPerformAction = Controls.Player.Action;
        InputActionPause = Controls.Player.Pause;

        InputActionNextCharacter.Enable();
        InputActionPrevCharacter.Enable();
        InputActionPerformAction.Enable();
        InputActionPause.Enable();

        InputActionNextCharacter.performed += delegate { NextCharacter(); };
        InputActionPrevCharacter.performed += delegate { PrevCharacter(); };
        InputActionPerformAction.performed += delegate 
        { 
            if(HasStarted && !Paused) Characters[ActiveCharacter].PerformAction(); 
        };
        InputActionPause.performed += delegate { if(HasStarted) SetPaused(!Paused); };
    }

    void OnDisable()
    {
        InputActionNextCharacter.Disable();
        InputActionPrevCharacter.Disable();
        InputActionPerformAction.Disable();
        InputActionPause.Disable();
    }

    private void SwitchToCharacter(int Character, bool PlaySFX = true)
    {
        if(CameraController.Instance.IsInCutscene() || Paused) return;

        Characters[ActiveCharacter].SetAsActiveController(false);
        ActiveCharacter = Character;
        Characters[ActiveCharacter].SetAsActiveController(true);
        CameraController.Instance.SetTarget(Characters[ActiveCharacter].transform);

        if(PlaySFX) AudioManager.Instance.SfxCharacterChange.PlayAsSFX(Random.Range(0.5f, 0.6f));
    }

    private void NextCharacter()
    {
        int NextCharIndex = ActiveCharacter + 1;
        if(NextCharIndex > 2) NextCharIndex = 0;
        SwitchToCharacter(NextCharIndex);
    }

    private void PrevCharacter()
    {
        int PrevCharIndex = ActiveCharacter - 1;
        if(PrevCharIndex < 0) PrevCharIndex = 2;
        SwitchToCharacter(PrevCharIndex);
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
