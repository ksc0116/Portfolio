using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Manager : MonoBehaviour
{
    public AudioSource bgmAudioSource;
    public AudioSource sfxAudioSource;

    [Header("[Clip]")]
    public AudioClip stepClip;
    public AudioClip bossDownClip;
    public AudioClip attackClip;
    public AudioClip levelUpClip;
    public AudioClip dashClip;
    public AudioClip enemyDamageClip;
    public AudioClip rockDownClip;
    public AudioClip bossSkillClip;
    public AudioClip dangerClip;
    public AudioClip clearClip;
    public AudioClip buttonUIClip;
    public AudioClip stunClip;
    public AudioClip rockBreakClip;

    [Header("[BGM]")]
    public AudioClip bossBGM;
    public AudioClip mainMenuBGM;
    public AudioClip monsterSceneBGM;
    public AudioClip gameClearBGM;

    private void Awake()
    {
        bgmAudioSource.volume = PlayerPrefs.GetFloat("BGM");
        sfxAudioSource.volume = PlayerPrefs.GetFloat("SFX");
    }

    public void ChangeBGM(AudioClip p_clip)
    {
        bgmAudioSource.clip = p_clip;
        bgmAudioSource.Play();
    }
    public void PlaySound(AudioClip clip)
    {
        sfxAudioSource.PlayOneShot(clip);
    }

    public void SetBgmVolume(float volume)
    {
        bgmAudioSource.volume=volume;
        SetBGMLevel(volume);
    }
    void SetBGMLevel(float sliderValue)
    {
        PlayerPrefs.SetFloat("BGM",sliderValue);
    }

    public void SetSfxVolume(float volume)
    {
        sfxAudioSource.volume = volume;
        SetSFXLevel(volume);
    }
    void SetSFXLevel(float sliderValue)
    {
        PlayerPrefs.SetFloat("SFX", sliderValue);
    }
}
