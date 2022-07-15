using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScene : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Manager.instance.sound_Manager.bgmAudioSource.clip == Manager.instance.sound_Manager.monsterSceneBGM) return;
            Manager.instance.sound_Manager.ChangeBGM(Manager.instance.sound_Manager.monsterSceneBGM);
        }
    }
}
