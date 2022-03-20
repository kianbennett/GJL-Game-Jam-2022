using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPreview : MonoBehaviour
{
    public CharacterPreviewCard steamImage;
    public CharacterPreviewCard jumpImage;
    public CharacterPreviewCard shrinkImage;
    public PlayerController playerController;
    private void Start()
    {
        playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        jumpImage.SetActive(playerController.ActiveCharacter == 0);
        steamImage.SetActive(playerController.ActiveCharacter == 1);
        shrinkImage.SetActive(playerController.ActiveCharacter == 2);
        // if(playerController.ActiveCharacter == 0)
        // {
            // jumpImage.SetActive(true);
            // shrinkImage.SetActive(false);
            // steamImage.SetActive(false);
        // }
        // if (playerController.ActiveCharacter == 1)
        // {
            // jumpImage.SetActive(false);
            // shrinkImage.SetActive(false);
            // steamImage.SetActive(true);
        // }
        // if (playerController.ActiveCharacter == 2)
        // {
            // jumpImage.SetActive(false);
            // shrinkImage.SetActive(true);
            // steamImage.SetActive(false);
        // }
    }
}
