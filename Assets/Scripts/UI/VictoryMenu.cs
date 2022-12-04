using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VictoryMenu : MonoBehaviour
{
    [SerializeField] private Button PlayAgainButton;

    void Awake()
    {
        PlayAgainButton.Select();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }   

    public void Quit()
    {
        Application.Quit();
    }
}
