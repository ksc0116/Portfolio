using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemMemoryPool : MonoBehaviour
{
    [SerializeField] GameObject hpPotion;
    [SerializeField] GameObject mpPotion;

    MemoryPool hp_pool;
    MemoryPool mp_pool;

    private void Awake()
    {
        hp_pool = new MemoryPool(hpPotion);
        mp_pool = new MemoryPool(mpPotion);
    }

    public void SpawnItem(Vector3 position)
    {
        int randItem = Random.Range(0, 100);
        GameObject clone;
        if (randItem <= 49)
        {
            clone = hp_pool.ActivePoolItem();
            clone.GetComponent<FieldItem>().Init(hp_pool);
        }
        else
        {
            clone = mp_pool.ActivePoolItem();
            clone.GetComponent<FieldItem>().Init(mp_pool);
        }
        clone.transform.position = position;
    }
}
