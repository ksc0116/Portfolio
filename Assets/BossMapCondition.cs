using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMapCondition : MonoBehaviour
{
    [SerializeField] GameObject boss;

    [SerializeField] Transform inCamera;

    [SerializeField] Vector3 targetPos;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            StartCoroutine(Manager.instance.camera_Manager.BossInCamera(inCamera.gameObject, boss.transform, targetPos));
        }
    }
}
