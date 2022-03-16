using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController[] Characters;

    private int ActiveCharacter;

    void Awake()
    {
        SwitchToCharacter(1);

        Application.targetFrameRate = 60;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            PreviousCharacter();
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            NextCharacter();
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            Characters[ActiveCharacter].PerformAction();
        }
    }

    private void SwitchToCharacter(int Character)
    {
        Characters[ActiveCharacter].SetAsActiveController(false);
        ActiveCharacter = Character;
        Characters[ActiveCharacter].SetAsActiveController(true);
        CameraController.Instance.SetTarget(Characters[ActiveCharacter].transform);
    }

    private void NextCharacter()
    {
        SwitchToCharacter(ActiveCharacter >= Characters.Length - 1 ? 0 : ActiveCharacter + 1);
    }

    private void PreviousCharacter()
    {
        SwitchToCharacter(ActiveCharacter <= 0 ? Characters.Length - 1 : ActiveCharacter - 1);
    }
}
