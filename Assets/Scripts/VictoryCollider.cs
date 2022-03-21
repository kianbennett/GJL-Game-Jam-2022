using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.GetComponent<CharacterController>())
        {
            PlayerController.Instance.SetPaused(true);
            MenuManager.Instance.PauseMenu.gameObject.SetActive(false);
            MenuManager.Instance.VictoryMenu.gameObject.SetActive(true);
        }
    }
}
