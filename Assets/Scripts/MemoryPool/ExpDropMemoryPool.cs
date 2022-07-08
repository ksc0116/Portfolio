using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpDropMemoryPool : MonoBehaviour
{
    [SerializeField] GameObject expDropPrefab;
    MemoryPool m_pool;

    private void Awake()
    {
        m_pool =new MemoryPool(expDropPrefab);
    }

    public void SpawnExpDrop(Transform target,float exp,Transform expOriginParent,GameObject removeObj)
    {
        GameObject clone = m_pool.ActivePoolItem();
        clone.GetComponent<EXP_Drop>().Init(target,exp,m_pool, expOriginParent, removeObj);
        clone.transform.SetParent(transform);
    }
}
