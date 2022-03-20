using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : Singleton<MenuManager>
{
    public PauseMenu PauseMenu;
    public MainMenu MainMenu;
    public HelpMenu HelpMenu;
    public CharacterPreview CharacterPreview;

    protected override void Awake()
    {
        // Set menus to initial state
        PauseMenu.gameObject.SetActive(false);
        MainMenu.gameObject.SetActive(true);
        HelpMenu.gameObject.SetActive(false);
        CharacterPreview.gameObject.SetActive(false);
    }
}
