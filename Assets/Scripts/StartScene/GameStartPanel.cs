using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartPanel : MonoBehaviour
{
    [SerializeField] GameObject optionExitInMenu;
    [SerializeField] GameObject optionExitInGame;
    [SerializeField] GameObject inGameUI;
    [SerializeField] GameObject gameStartPanel;
    [SerializeField] GameObject backGroundImage;
    [SerializeField] Image  fadeImage;
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject panelCamera;
    public void ContinueButton()
    {
        Manager.instance.sound_Manager.PlaySound(Manager.instance.sound_Manager.buttonUIClip);
        inGameUI.SetActive(true);
        optionExitInGame.SetActive(true);
        fadeImage.gameObject.SetActive(true);
        StartCoroutine(FadeIn());
        Manager.instance.json_Manager.JsonDataLoad();
       
    }

    public void NewButton()
    {
        Manager.instance.sound_Manager.PlaySound(Manager.instance.sound_Manager.buttonUIClip);
        inGameUI.SetActive(true);
        optionExitInGame.SetActive(true);
        fadeImage.gameObject.SetActive(true);
        StartCoroutine(FadeIn());
        Manager.instance.json_Manager.JsonDataReset();
        Manager.instance.json_Manager.JsonDataLoad();
    }

    public void BackButton()
    {
        Manager.instance.sound_Manager.PlaySound(Manager.instance.sound_Manager.buttonUIClip);
        optionExitInMenu.SetActive(true);
        mainMenuPanel.SetActive(true);
        gameStartPanel.SetActive(false);
    }

    IEnumerator FadeIn()
    { 
        panelCamera.SetActive(false);
        gameStartPanel.SetActive(false);
        backGroundImage.SetActive(false);

        float currentTime = 0.0f;
        float percent = 0.0f;
        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / 1.0f;
            
            Color color = fadeImage.color;
            color.a = Mathf.Lerp(color.a, 0f, 0.01f);
            fadeImage.color = color;
            yield return null;
        }
        fadeImage.gameObject.SetActive(false);
    }
}
