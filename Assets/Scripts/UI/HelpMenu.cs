using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class HelpMenu : MonoBehaviour
{
    [SerializeField] private Button ButtonOk;
    private bool OpenedFromPauseMenu;

    // private PlayerControls Controls;

    void OnEnable()
    {
        ButtonOk.Select();
    }

    public void Open(bool FromPauseMenu)
    {
        gameObject.SetActive(true);
        OpenedFromPauseMenu = FromPauseMenu;
    }

    public void Close()
    {
        gameObject.SetActive(false);
        if(OpenedFromPauseMenu)
        {
            MenuManager.Instance.PauseMenu.gameObject.SetActive(true);
        }
        else
        {
            PlayerController.Instance.SetPaused(false);
            MenuManager.Instance.CharacterPreview.gameObject.SetActive(true);
        }
    }
}
