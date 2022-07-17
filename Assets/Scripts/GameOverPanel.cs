using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] Image fadeImage;
    [SerializeField] GameObject optionExitInMenu;
    [SerializeField] GameObject optionExitInPause;
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject mainMenuBg;
    [SerializeField] GameObject mainMenuCamera;
    [SerializeField] GameObject inGameUI;
    [SerializeField] Animator playerAnim;
    public void RetryButton()
    {
        Manager.instance.sound_Manager.PlaySound(Manager.instance.sound_Manager.buttonUIClip);
        Manager.instance.playerStat_Manager.isDie = false;
        playerAnim.SetTrigger("onRetry");
        StartCoroutine(FadeIn());
        Manager.instance.json_Manager.JsonDataLoad();
        
    }

    public void ExitButton()
    {
        Manager.instance.sound_Manager.PlaySound(Manager.instance.sound_Manager.buttonUIClip);
        optionExitInPause.SetActive(false);
        inGameUI.SetActive(false);
        StartCoroutine(FadeInMenu());
    }
    IEnumerator FadeIn()
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        fadeImage.gameObject.SetActive(true);

        Color color = fadeImage.color;
        color.a = 1f;
        fadeImage.color = color;
        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / 1f;
            color = fadeImage.color;
            color.a = Mathf.Lerp(color.a, 0f, 0.01f);
            fadeImage.color = color;
            yield return null;
        }
        gameObject.SetActive(false);
        fadeImage.gameObject.SetActive(false);
    }
    IEnumerator FadeInMenu()
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        fadeImage.gameObject.SetActive(true);

        Color color = fadeImage.color;
        color.a = 1f;
        fadeImage.color = color;
        while (color.a >0.1f)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / 1f;
            color = fadeImage.color;
            color.a = Mathf.Lerp(color.a, 0f, 0.01f);
            fadeImage.color = color;
            yield return null;
        }
        mainMenuBg.SetActive(true);
        mainMenuCamera.SetActive(true);
        optionExitInMenu.SetActive(true);
        mainMenuPanel.SetActive(true);
        fadeImage.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
