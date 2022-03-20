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
    }

    public void Quit()
    {
        Application.Quit();
    }
}
