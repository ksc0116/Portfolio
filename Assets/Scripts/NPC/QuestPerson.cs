using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class QuestPerson : MonoBehaviour
{
    public TextMeshPro acceptText;
    public bool isAccept=false;
    public GameObject talkTextObj;
    public GameObject inCamera;
    public GameObject questPanel;

    Camera cam;
    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Manager.instance.quest_Manager.questIndex == 2)
        {
            acceptText.text = "";
            return;
        }

        ChangeText();
    }

    void ChangeText()
    {
        if (Manager.instance.quest_Manager.isClear == true)
        {
            acceptText.text = "!";
            acceptText.color = Color.yellow;
        }
        else if(Manager.instance.quest_Manager.isClear == false)
        {
            acceptText.text = "?";

            if (Manager.instance.quest_Manager.isAccept == true)
            {
                acceptText.color = Color.gray;
            }
            else if(Manager.instance.quest_Manager.isAccept == false)
            {
                acceptText.color = Color.yellow;
            }
        }

        if (cam.enabled == true)
        {
            Quaternion q_hp = Quaternion.LookRotation(acceptText.transform.position - cam.transform.position);
            Vector3 hp_angle = Quaternion.RotateTowards(acceptText.transform.rotation, q_hp, 1000).eulerAngles;
            acceptText.transform.rotation = Quaternion.Euler(hp_angle.x, hp_angle.y, 0);
        }

        if (inCamera.activeSelf == true)
        {
            Quaternion q_hp = Quaternion.LookRotation(acceptText.transform.position - inCamera.transform.position);
            Vector3 hp_angle = Quaternion.RotateTowards(acceptText.transform.rotation, q_hp, 1000).eulerAngles;
            acceptText.transform.rotation = Quaternion.Euler(hp_angle.x, hp_angle.y, 0);
        }


    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if (Manager.instance.quest_Manager.isTalk == false)
            {
                talkTextObj.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                Manager.instance.quest_Manager.isTalk = true;
                Manager.instance.playerStat_Manager.isMoveAble = false;
                Manager.instance.camera_Manager.ChangeCamera(inCamera);
                questPanel.SetActive(true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            talkTextObj.SetActive(false);
        }
    }
}
