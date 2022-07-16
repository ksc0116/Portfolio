using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform targetPosition;
    [SerializeField] Transform touchEffect;

    Transform playerTransform;
    float moveSpeed = 2f;

    bool isTrigger = false;
    public bool isArrive = false;
    private void Update()
    {
        if (isArrive==true) return;

        if(Vector3.Distance(transform.position, targetPosition.position) < 0.01f)
        {
            isArrive = true;    
        }
        if (isTrigger == true)
        {
            transform.position=Vector3.MoveTowards(transform.position, targetPosition.position, moveSpeed*Time.deltaTime);
            if(isArrive == false)
            {
                playerTransform.localPosition = Vector3.zero;
                touchEffect.localPosition = new Vector3(0, 0.05f, 0);
                playerTransform.GetComponent<PlayerController>().isMove = false;
                playerTransform.GetComponentInChildren<Animator>().SetBool("isMove", playerTransform.GetComponent<PlayerController>().isMove);
            }
            else
            {
                playerTransform.parent = null;
                playerTransform.localPosition=transform.position;
                playerTransform.GetComponent<NavMeshAgent>().SetDestination(transform.position);
                touchEffect.parent = null;
                touchEffect.localPosition = transform.position;
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isTrigger = true;
            playerTransform=other.transform;
            Debug.Log(other.name);
            playerTransform.SetParent(transform);
            touchEffect.SetParent(transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            Manager.instance.disable_Manager.DestroyParticle(transform);
            Manager.instance.disable_Manager.DestroyObject(gameObject);
        }
    }
}
