using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController[] Characters;

    [HideInInspector] public int ActiveCharacter;

    void Awake()
    {
        SwitchToCharacter(1);

        Application.targetFrameRate = 60;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) SwitchToCharacter(0);
        if(Input.GetKeyDown(KeyCode.Alpha2)) SwitchToCharacter(1);
        if(Input.GetKeyDown(KeyCode.Alpha3)) SwitchToCharacter(2);

        if(Input.GetKeyDown(KeyCode.F))
        {
            Characters[ActiveCharacter].PerformAction();
        }
    }

    private void SwitchToCharacter(int Character)
    {
        if(CameraController.Instance.IsInCutscene()) return;

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
