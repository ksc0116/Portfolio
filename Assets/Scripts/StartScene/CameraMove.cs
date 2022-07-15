using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform[] patrolPoint;
    int patrolIndex = 0;
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, patrolPoint[patrolIndex].position, Time.deltaTime*0.1f);
        if(Vector3.Distance(transform.position, patrolPoint[patrolIndex].position) < 0.01f)
        {
            patrolIndex = patrolIndex>patrolPoint.Length? 0 : patrolIndex+=1;
        }
    }
}
