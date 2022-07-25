using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuickSlotPanel : MonoBehaviour
{
    [SerializeField] Transform[] quickSlots;

    Image tempHpIcon;
    float hpPlusTime = 6f;
    float hpPlusTick = 1.5f;
    bool isHpPlus = true;

    Image tempMpIcon;
    float mpPlusTime = 6f;
    float mpPlusTick = 1f;
    bool isMpPlus = true;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (quickSlots[0].GetChild(1).gameObject.activeSelf == false) return;
            CheckItem(quickSlots[0]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (quickSlots[1].GetChild(1).gameObject.activeSelf == false) return;
            CheckItem(quickSlots[1]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (quickSlots[2].GetChild(1).gameObject.activeSelf == false) return;
            CheckItem(quickSlots[2]);
        }
    }

    void CheckItem(Transform slot)
    {
        if(slot.GetChild(1).GetComponent<Item_Action>().m_itemInfo.itemSprite.name == "hp")
        {
            if (isHpPlus == true && (Manager.instance.playerStat_Manager.curHp == Manager.instance.playerStat_Manager.maxHP) == false)
            {
                slot.GetChild(1).GetComponent<Item_Action>().m_itemInfo.Cnt--;
                if (slot.GetChild(1).GetComponent<Item_Action>().m_itemInfo.Cnt == 0)
                {
                    ReMoveItem(slot.GetChild(1).GetComponent<Item_Action>());
                }
                else
                {
                    slot.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = slot.GetChild(1).GetComponent<Item_Action>().m_itemInfo.Cnt.ToString();
                }
                tempHpIcon = Instantiate(slot.GetChild(1), slot.GetChild(1).parent).GetComponent<Image>();
                StartCoroutine( PlusHp(tempHpIcon,slot.GetChild(1).GetComponent<Image>()) );
            }
        }
        else if (slot.GetChild(1).GetComponent<Item_Action>().m_itemInfo.itemSprite.name == "mp")
        {
            if(isMpPlus == true && (Manager.instance.playerStat_Manager.curMP == Manager.instance.playerStat_Manager.maxMP) == false)
            {
                slot.GetChild(1).GetComponent<Item_Action>().m_itemInfo.Cnt--;
                if (slot.GetChild(1).GetComponent<Item_Action>().m_itemInfo.Cnt == 0)
                {
                    ReMoveItem(slot.GetChild(1).GetComponent<Item_Action>());
                }
                else
                {
                    slot.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = slot.GetChild(1).GetComponent<Item_Action>().m_itemInfo.Cnt.ToString();
                }
                tempMpIcon = Instantiate(slot.GetChild(1), slot.GetChild(1).parent).GetComponent<Image>();
                StartCoroutine( PlusMp(tempMpIcon, slot.GetChild(1).GetComponent<Image>()));
            }
        }
    }

    IEnumerator PlusHp(Image tempIcon,Image originIcon)
    {
        originIcon.raycastTarget = false;

        tempIcon.type = Image.Type.Filled;
        tempIcon.raycastTarget = false;
        tempIcon.color = Color.black;
        isHpPlus = false;
        float hpTotalPlusTime = hpPlusTime;
        float tickTime = 0f;
        while (hpTotalPlusTime > 0)
        {
            tempIcon.fillAmount = hpTotalPlusTime / hpPlusTime;
            hpTotalPlusTime -= Time.deltaTime;
            tickTime -= Time.deltaTime;
            if (tickTime < 0)
            {
                if (Manager.instance.playerStat_Manager.curHp <= Manager.instance.playerStat_Manager.maxHP)
                {
                    Manager.instance.playerStat_Manager.curHp += 5f;
                    if(Manager.instance.playerStat_Manager.curHp> Manager.instance.playerStat_Manager.maxHP)
                    {
                        Manager.instance.playerStat_Manager.curHp = Manager.instance.playerStat_Manager.maxHP;
                    }
                }
                tickTime = hpPlusTick;
            }
            yield return null;
        }
        originIcon.raycastTarget = true;
        Destroy(tempIcon.gameObject);
        isHpPlus = true;
    }

    IEnumerator PlusMp(Image tempIcon, Image originIcon)
    {
        originIcon.raycastTarget = false;

        tempIcon.type = Image.Type.Filled;
        tempIcon.raycastTarget = false;
        tempIcon.color = Color.black;

        isMpPlus = false;
        float mpTotalPlusTime = mpPlusTime;
        float tickTime = 0f;
        while (mpTotalPlusTime > 0)
        {
            tempIcon.fillAmount = mpTotalPlusTime / mpPlusTime;

            mpTotalPlusTime -= Time.deltaTime;
            tickTime -= Time.deltaTime;
            if (tickTime < 0)
            {
                if (Manager.instance.playerStat_Manager.curMP <= Manager.instance.playerStat_Manager.maxMP)
                {
                    Manager.instance.playerStat_Manager.curMP += 5f;
                    if (Manager.instance.playerStat_Manager.curMP > Manager.instance.playerStat_Manager.maxMP)
                    {
                        Manager.instance.playerStat_Manager.curMP = Manager.instance.playerStat_Manager.maxMP;
                    }
                }
                tickTime = mpPlusTick;
            }
            yield return null;
        }
        originIcon.raycastTarget = true;
        isMpPlus = true;
        Destroy(tempIcon.gameObject);
    }

    void ReMoveItem(Item_Action p_item)
    {
        p_item.GetComponent<Image>().sprite = null;
        p_item.m_itemInfo = null;
        p_item.gameObject.SetActive(false);
    }
}
