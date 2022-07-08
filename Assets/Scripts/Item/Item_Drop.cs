using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Item_Drop : MonoBehaviour, IPointerEnterHandler,IDropHandler
{
    public bool inInven;
    public bool inQuick;
    public bool inEquip;
    public void OnDrop(PointerEventData eventData)
    {
        Manager.instance.inven_Manager.isDrop = true;

        if (Manager.instance.equip_Manager.selectImage.gameObject.activeSelf == true)
        {
            Manager.instance.equip_Manager.selectImage.SetParent(Manager.instance.inven_Manager.dragParent);
            Manager.instance.equip_Manager.selectImage.gameObject.SetActive(false);
        }

        if (Manager.instance.inven_Manager.curMousePosition == Manager.instance.inven_Manager.curParent)
        {
            Manager.instance.inven_Manager.selectItem.SetParent(Manager.instance.inven_Manager.curParent);
            return;
        }

        if (inInven == true)
        {
            Image item = transform.GetChild(0).GetComponent<Image>();
            item.transform.SetParent(Manager.instance.inven_Manager.curParent);
            item.transform.localPosition = Vector3.zero;
        }
        else if (inEquip==true)
        {
            if (Manager.instance.inven_Manager.selectItem.GetComponent<Item_Action>().m_itemInfo.itemKind == ItemKind.Potion) return;

            if(Manager.instance.equip_Manager.equipSlot[Manager.instance.inven_Manager.selectItem.GetComponent<Item_Action>().m_itemInfo.itemNum]
                != transform)
            {
                return;
            }

            Image item = transform.GetChild(1).GetComponent<Image>();
            item.transform.SetParent(Manager.instance.inven_Manager.curParent);
            item.transform.localPosition = Vector3.zero;
        }
        else if(inQuick==true)
        {
            if (Manager.instance.inven_Manager.selectItem.GetComponent<Item_Action>().m_itemInfo.itemKind == ItemKind.Weapon) return;

            Image item = transform.GetChild(1).GetComponent<Image>();
            item.transform.SetParent(Manager.instance.inven_Manager.curParent);
            item.transform.localPosition = Vector3.zero;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Manager.instance.inven_Manager.curMousePosition = transform;
    }
}
