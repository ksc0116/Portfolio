using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Item_Action : MonoBehaviour, IBeginDragHandler, IEndDragHandler,IDragHandler
{
    public ItemInfo m_itemInfo;
    Image img;
    public void OnBeginDrag(PointerEventData eventData)
    {
        img = GetComponent<Image>();
        img.raycastTarget = false;
        Manager.instance.inven_Manager.selectItem = transform;
        Manager.instance.inven_Manager.curParent = transform.parent;
        transform.SetParent(Manager.instance.inven_Manager.dragParent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Manager.instance.inven_Manager.isDragging = true;
        transform.position = Input.mousePosition;
        transform.GetChild(0).gameObject.SetActive(false);
        if (m_itemInfo.itemKind == ItemKind.Weapon)
        {
            Manager.instance.equip_Manager.selectImage.gameObject.SetActive(true);
            Manager.instance.equip_Manager.selectImage.SetParent(Manager.instance.equip_Manager.equipSlot[m_itemInfo.itemNum]);
            Manager.instance.equip_Manager.selectImage.localPosition = Vector3.zero;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Manager.instance.inven_Manager.isDragging = false;

        if (Manager.instance.inven_Manager.isDrop == false)
        {
            if (Manager.instance.inven_Manager.curParent.GetComponent<Item_Drop>().inEquip == true)
            {
                transform.GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }


            if (Manager.instance.equip_Manager.selectImage.gameObject.activeSelf == true)
            {
                Manager.instance.equip_Manager.selectImage.SetParent(Manager.instance.inven_Manager.dragParent);
                Manager.instance.equip_Manager.selectImage.gameObject.SetActive(false);
            }

            transform.SetParent(Manager.instance.inven_Manager.curParent);
            transform.localPosition = Vector3.zero;
            img.raycastTarget = true;
            return;
        }

        if (m_itemInfo.itemKind == ItemKind.Weapon)
        {
            if((Manager.instance.inven_Manager.curMousePosition== Manager.instance.equip_Manager.equipSlot[0]) ||
                Manager.instance.inven_Manager.curMousePosition == Manager.instance.equip_Manager.equipSlot[1])
            {
                transform.SetParent(Manager.instance.inven_Manager.curParent);
                transform.localPosition = Vector3.zero;
                img.raycastTarget = true;

                Manager.instance.inven_Manager.isDrop = false;

                return;
            }

            
            if (Manager.instance.inven_Manager.curMousePosition.GetComponent<Item_Drop>().inQuick == true)
            {
                transform.SetParent(Manager.instance.inven_Manager.curParent);
                transform.localPosition = Vector3.zero;
                if (Manager.instance.inven_Manager.curParent.GetComponent<Item_Drop>().inEquip==true)
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                }
                else
                {
                    transform.GetChild(0).gameObject.SetActive(true);
                }

                Manager.instance.inven_Manager.isDrop = false;

                img.raycastTarget = true;
                return;
            }
        }
        else if (m_itemInfo.itemKind == ItemKind.Potion)
        {
            if (Manager.instance.inven_Manager.curMousePosition.GetComponent<Item_Drop>().inEquip == true)
            {
                transform.SetParent(Manager.instance.inven_Manager.curParent);
                transform.localPosition = Vector3.zero;
                transform.GetChild(0).gameObject.SetActive(true);
                img.raycastTarget = true;

                Manager.instance.inven_Manager.isDrop = false;

                return;
            }
        }

        Manager.instance.inven_Manager.isDrop = false;

        img.raycastTarget = true;

        if (m_itemInfo.itemKind == ItemKind.Weapon)
        {
            Manager.instance.playerStat_Manager.atk_Bonus = m_itemInfo.atkBonus;
        }
        else if (m_itemInfo == null)
        {
            Manager.instance.playerStat_Manager.atk = Manager.instance.playerStat_Manager.originAtk;
        }

        transform.SetParent(Manager.instance.inven_Manager.curMousePosition);
        transform.localPosition= Vector3.zero;

        if(Manager.instance.inven_Manager.curMousePosition.GetComponent<Item_Drop>().inQuick == true)
        {
            Manager.instance.quickSlot_Manager.quickSlots[Manager.instance.inven_Manager.curMousePosition.GetComponent<Item_Drop>().slotIndex].GetChild(1).GetComponent<Item_Action>().m_itemInfo = m_itemInfo;
        }


        if (Manager.instance.inven_Manager.curMousePosition.GetComponent<Item_Drop>().inEquip == true)
        {
            Manager.instance.equipSlot_Manager.equipSlots[Manager.instance.inven_Manager.curMousePosition.GetComponent<Item_Drop>().slotIndex].GetChild(1).GetComponent<Item_Action>().m_itemInfo = m_itemInfo;
        }

        if (Manager.instance.inven_Manager.curMousePosition.GetComponent<Item_Drop>().inInven == true)
        {
            Manager.instance.invenSlot_Manager.invenSlots[Manager.instance.inven_Manager.curMousePosition.GetComponent<Item_Drop>().slotIndex].GetChild(0).GetComponent<Item_Action>().m_itemInfo = m_itemInfo;
        }

        if (Manager.instance.inven_Manager.curMousePosition.GetComponent<Item_Drop>().inInven == true || Manager.instance.inven_Manager.curMousePosition.GetComponent<Item_Drop>().inQuick == true)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
