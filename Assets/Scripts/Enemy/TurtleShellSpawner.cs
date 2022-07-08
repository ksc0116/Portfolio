using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleShellSpawner : MonoBehaviour
{
    public Transform m_target;
    public GameObject turtleShellPrefab;
    public GameObject expDropPrefab;
    public Transform[] spawnPoint;
    public Transform expOriginParent;
    public DamageTextMemoryPool damageTextMemoryPool;
    private void Awake()
    {
        for(int i = 0; i < spawnPoint.Length; i++)
        {
            GameObject clone = Instantiate(turtleShellPrefab);
            clone.GetComponent<TurtleShell>().Init(m_target, expDropPrefab,expOriginParent);
            clone.transform.position = spawnPoint[i].position;
        }
    }
}
