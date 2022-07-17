using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPanel : MonoBehaviour
{
    [SerializeField] GameObject gameStartPanel;
    [SerializeField] GameObject optionPanel;
    [SerializeField] GameObject optionExitButtonInMenu;
    [SerializeField] GameObject optionExitButtonInGame;
    public void StartButton()
    {
        Manager.instance.sound_Manager.PlaySound(Manager.instance.sound_Manager.buttonUIClip);
        gameObject.SetActive(false);
        gameStartPanel.SetActive(true);
        optionExitButtonInMenu.SetActive(false);
    }

    public void OptionButtin()
    {
        Manager.instance.sound_Manager.PlaySound(Manager.instance.sound_Manager.buttonUIClip);
        gameObject.SetActive(false);
        optionPanel.SetActive(true);    
    }

    public void ExitButton()
    {
        gameObject.SetActive(false);
        Application.Quit(); 
    }
}
