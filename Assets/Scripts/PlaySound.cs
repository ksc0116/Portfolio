using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public void PlaySFX(AudioClip clip)
    {
        Manager.instance.sound_Manager.PlaySound(clip);
    }
}
