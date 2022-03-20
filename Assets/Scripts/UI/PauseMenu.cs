using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void Resume()
    {
        PlayerController.Instance.SetPaused(false);
    }

    public void Help()
    {
        gameObject.SetActive(false);
        MenuManager.Instance.HelpMenu.Open(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
