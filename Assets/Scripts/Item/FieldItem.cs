using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItem : MonoBehaviour
{
    ItemInfo m_itemInfo;
    public bool isFollow = false;
    MemoryPool pool;
    private void Awake()
    {
        m_itemInfo = GetComponent<ItemInfo>();
    }
    private void OnEnable()
    {
        StartCoroutine(FollowStart());
    }
    public void Init(MemoryPool p_pool)
    {
        pool = p_pool;
    }
    IEnumerator FollowStart()
    {
        yield return new WaitForSeconds(1f);
        isFollow = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (isFollow == true)
            {
                pool.DeactivePoolItem(gameObject);
                Manager.instance.inven_Manager.GetItem(m_itemInfo);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (isFollow == true)
            {
                pool.DeactivePoolItem(gameObject);
                Manager.instance.inven_Manager.GetItem(m_itemInfo);
            }
        }
    }
}
