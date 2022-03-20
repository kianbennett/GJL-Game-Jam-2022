using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] private CharacterController[] Characters;

    [HideInInspector] public int ActiveCharacter;

    private bool Paused;

    protected override void Awake()
    {
        SwitchToCharacter(1, false);

        Application.targetFrameRate = 60;
        AudioManager.Instance.MusicLevel.PlayAsMusic();
    }

    void Update()
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
            AudioManager.Instance.PauseMusic();    
        }
        else
        {
            AudioManager.Instance.ResumeMusic();
        }
    }
}
