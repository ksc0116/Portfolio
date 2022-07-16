using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalesPerson : MonoBehaviour
{
    public GameObject invenObj;
    public GameObject marketObj;
    public GameObject talkTextObj;
    public GameObject inCamera;
    public GameObject nameTextObj;

    Camera cam;
    private void Awake()
    {
        cam=Camera.main;
    }
    private void Update()
    {
        if (cam.enabled == true)
        {
            Quaternion q_text = Quaternion.LookRotation(nameTextObj.transform.position - cam.transform.position);
            Vector3 angle = Quaternion.RotateTowards(nameTextObj.transform.rotation, q_text, 1000).eulerAngles;
            nameTextObj.transform.rotation = Quaternion.Euler(angle.x, angle.y, 0);
        }
        else if (inCamera.activeSelf == true)
        {
            Quaternion q_text = Quaternion.LookRotation(nameTextObj.transform.position - inCamera.transform.position);
            Vector3 angle = Quaternion.RotateTowards(nameTextObj.transform.rotation, q_text, 1000).eulerAngles;
            nameTextObj.transform.rotation = Quaternion.Euler(angle.x, angle.y, 0);
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Manager.instance.quest_Manager.isTalk == false)
            {
                talkTextObj.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                Manager.instance.quest_Manager.isTalk = true;
                Manager.instance.playerStat_Manager.isMoveAble = false;
                marketObj.SetActive(true);
                invenObj.SetActive(true);
                Manager.instance.camera_Manager.ChangeCamera(inCamera);
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
