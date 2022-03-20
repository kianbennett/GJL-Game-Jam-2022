using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMenu : MonoBehaviour
{
    private bool OpenedFromPauseMenu;

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
