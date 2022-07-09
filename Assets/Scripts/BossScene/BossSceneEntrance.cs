using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossSceneEntrance : MonoBehaviour
{
    [SerializeField] GameObject fadeImage;
    [SerializeField] GameObject highLight;
    [SerializeField] Transform destinationPosition;
    [SerializeField] GameObject touchEffect;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            fadeImage.SetActive(true);
            highLight.SetActive(true);
            other.GetComponent<NavMeshAgent>().ResetPath();
            other.GetComponent<NavMeshAgent>().speed = 2f;
            other.GetComponent<NavMeshAgent>().SetDestination(destinationPosition.position);
            if (touchEffect.activeSelf == false) touchEffect.SetActive(true);
            touchEffect.transform.position=new Vector3(destinationPosition.position.x,destinationPosition.position.y+1f,destinationPosition.position.z);
        }
    }
}
