using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button ButtonResume;

    void OnEnable()
    {
        ButtonResume.Select();
    }

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
