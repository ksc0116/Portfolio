using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStairCondition : MonoBehaviour
{
    [SerializeField] GameObject[] stairs;
    [SerializeField] Transform inCamera;

    bool isFirst=false;

    private void OnTriggerEnter(Collider other)
    {
        if (isFirst == false)
        {
            if (other.tag == "Player")
            {
                isFirst = true;
                StartCoroutine(OnTrigger());
            }
        }

    }
    IEnumerator OnTrigger()
    {
        StartCoroutine( Manager.instance.camera_Manager.MainCameraMove(inCamera) );
        yield return new WaitForSeconds(0.1f);
        for (int i=stairs.Length-1;i>-1;i--)
        {
            stairs[i].GetComponent<Rigidbody>().useGravity = true;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
