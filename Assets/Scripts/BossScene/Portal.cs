using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class Portal : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI interactText;
    [SerializeField] Transform targetPos;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Manager.instance.playerStat_Manager.isPortal = true;
            interactText.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.G))
            {
                Manager.instance.sound_Manager.ChangeBGM(Manager.instance.sound_Manager.mainMenuBGM);
                other.GetComponent<NavMeshAgent>().enabled = false;
                Manager.instance.playerStat_Manager.isPortal = false;
                interactText.gameObject.SetActive(false);
                other.transform.position = targetPos.position;
                other.GetComponent<NavMeshAgent>().enabled = true;
                Manager.instance.camera_Manager.isBossScene = false;
            }
        }
    }
}
