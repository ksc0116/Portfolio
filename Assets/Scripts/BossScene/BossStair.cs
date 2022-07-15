using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStair : MonoBehaviour
{
    [SerializeField] GameObject explosionPrefab;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BridgeDestroyCollider")
        {
            Instantiate(explosionPrefab,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
