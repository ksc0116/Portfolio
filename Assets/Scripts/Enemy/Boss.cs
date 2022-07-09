using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BossTrigger")
        {
            Manager.instance.camera_Manager.isTrigger = true;
        }
    }
}
