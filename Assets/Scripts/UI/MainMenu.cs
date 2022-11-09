using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TextReady;
    private bool HasBegun;

    void Awake()
    {
        InputSystem.onAnyButtonPress.CallOnce(ctrl =>
        {
            if(!HasBegun)
            {
                HasBegun = true;
                gameObject.SetActive(false);
                PlayerController.Instance.StartGame();
            }
        });
    }

    void Update()
    {
        TextReady.color = new Color(1, 1, 1, 0.3f + 0.2f * Mathf.Sin(Time.time * 4f));
    }
}
