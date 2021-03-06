using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSceneMidGoal : MonoBehaviour
{
    [SerializeField] GameObject boss;

    [SerializeField] Transform inCamera;

    [SerializeField] Vector3 targetPos;

    bool isFirst = false;

    private void Update()
    {
        if (isFirst == false)
        {
            if (Manager.instance.camera_Manager.isBossDrop == true)
            {
                isFirst = true;
                StartCoroutine(Manager.instance.camera_Manager.BossInCamera(inCamera.gameObject, boss.transform, targetPos));
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponent<BoxCollider>().isTrigger = false;
            GetComponent<BoxCollider>().size = Vector3.one;
            GetComponent<BoxCollider>().center = new Vector3(0, -0.5f, 0);
        }
    }
}
