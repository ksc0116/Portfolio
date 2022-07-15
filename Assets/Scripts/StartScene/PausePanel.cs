using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    [SerializeField] Image fadeImage;
    [SerializeField] GameObject inGameUI;
    [SerializeField] GameObject menuCamera;
    [SerializeField] GameObject optionExitButtonInMenu;
    [SerializeField] GameObject optionExitButtonInGame;
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject backGroundPanel;
    [SerializeField] GameObject optionPanel;
    public void SaveButton()
    {
        Manager.instance.sound_Manager.PlaySound(Manager.instance.sound_Manager.buttonUIClip);
        Manager.instance.json_Manager.JsonDataSave();
        backGroundPanel.SetActive(false);
        pausePanel.SetActive(false);
    }
    public void OptionButton()
    {
        Manager.instance.sound_Manager.PlaySound(Manager.instance.sound_Manager.buttonUIClip);
        optionPanel.SetActive(true);
        pausePanel.SetActive(false);
    }
    public void OptionExit()
    {
        optionPanel.SetActive(false);
        pausePanel.SetActive(true);
    }
    public void BackButton()
    {
        Manager.instance.sound_Manager.PlaySound(Manager.instance.sound_Manager.buttonUIClip);
        backGroundPanel.SetActive(false);
        pausePanel.SetActive(false);
    }
    public void ExitButton()
    {
        Manager.instance.sound_Manager.PlaySound(Manager.instance.sound_Manager.buttonUIClip);
        mainMenuPanel.SetActive(true);
        Manager.instance.sound_Manager.ChangeBGM(Manager.instance.sound_Manager.mainMenuBGM);
        fadeImage.gameObject.SetActive(true);
        StartCoroutine(FadeIn());
        inGameUI.SetActive(false);
        menuCamera.SetActive(true);
        
        optionExitButtonInMenu.SetActive(true);
        optionExitButtonInGame.SetActive(false);
        pausePanel.SetActive(false);
    }

    IEnumerator FadeIn()
    {
        Color color = fadeImage.color;
        color.a = 1f;
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / 1f;
          
            color.a = Mathf.Lerp(color.a, 0f, 0.01f);
            fadeImage.color = color;
            yield return null;
        }
        
        fadeImage.gameObject.SetActive(false);
    }
}
