using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItem : MonoBehaviour
{
    ItemInfo m_itemInfo;
    private void Awake()
    {
        m_itemInfo = GetComponent<ItemInfo>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameObject.SetActive(false);
            Manager.instance.inven_Manager.GetItem(m_itemInfo);
        }
    }
}
