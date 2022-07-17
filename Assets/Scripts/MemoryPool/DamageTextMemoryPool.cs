using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageTextMemoryPool : MonoBehaviour
{
    [SerializeField] GameObject damageTextPrefab;

    MemoryPool m_pool;

    private void Awake()
    {
        m_pool=new MemoryPool(damageTextPrefab);
    }

    public void SpawnDamageText(Vector3 position,float damage,float height,bool isPlayer)
    {
        GameObject clone = m_pool.ActivePoolItem();
        
        clone.transform.localScale = Vector3.one;
        if (isPlayer == false)
        {
            clone.GetComponent<DamageText>().Init(m_pool, Color.red);
            clone.GetComponent<TextMeshPro>().text=$"-{damage}";
        }
        else if (isPlayer == true)
        {
            clone.GetComponent<DamageText>().Init(m_pool, Color.white);
            clone.GetComponent<TextMeshPro>().text = $"-{damage}";
        }
        clone.transform.position = position + new Vector3(0, height, 0);
        clone.transform.rotation=Quaternion.identity;
    }
}
