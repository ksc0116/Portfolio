using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TurtleShellSpawner : MonoBehaviour
{
    public Transform m_target;
    public GameObject turtleShellPrefab;
    public GameObject expDropPrefab;
    public Transform[] spawnPoint;
    public Transform expOriginParent;
    public void Spawn()
    {
        for(int i = 0; i < spawnPoint.Length; i++)
        {
            GameObject clone = Instantiate(turtleShellPrefab);
            clone.GetComponent<TurtleShell>().Init(m_target, expDropPrefab,expOriginParent);
            clone.GetComponent<NavMeshAgent>().enabled = false;
            clone.transform.position = spawnPoint[i].position;
            clone.GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}
