using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSetting : MonoBehaviour
{
    [SerializeField] Slider  bgmSlider;
    [SerializeField] Slider sfxSlider;

    private void Awake()
    {
        Setvolume();
    }

    void Setvolume()
    {
        if (PlayerPrefs.HasKey("BGM") == true)
        {
            bgmSlider.value = PlayerPrefs.GetFloat("BGM");
        }
        else
        {
            bgmSlider.value = 1f;
        }

        if (PlayerPrefs.HasKey("SFX") == true)
        {
            sfxSlider.value = PlayerPrefs.GetFloat("SFX");
        }
        else
        {
            sfxSlider.value = 1f;
        }
    }
}
