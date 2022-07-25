using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillInfo : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    public GameObject skillInfoFrame;
    public string frontStr = "";
    public string backStr = "";
    public string format;
    public Sprite skillSprite;
    Image skillImage;

    public bool isQ;
    public bool isW;
    public bool isE;

    public int damage;
    public float useMp;
    public float coolTime;
    private void Awake()
    {
        skillImage = GetComponent<Image>();
        skillSprite = skillImage.sprite;
       
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isQ)
        {
            damage = Manager.instance.playerStat_Manager.qSkillDamage;
            useMp = Manager.instance.playerStat_Manager.qSkillUseMp;
            coolTime = 3f;
            format = string.Format(frontStr + damage.ToString() + backStr);
        }
        else if (isW)
        {
            damage = Manager.instance.playerStat_Manager.wSkillDamage;
            useMp = Manager.instance.playerStat_Manager.wSkillUseMp;
            coolTime = 6f;
            format = string.Format(frontStr + damage.ToString() + backStr);
        }
        else
        {
            useMp = Manager.instance.playerStat_Manager.eSkillUseMp;
            coolTime = 4f;
            format = string.Format(frontStr + backStr);
        }
        skillInfoFrame.GetComponent<SkillInfoFrame>().GetSkillInfo(this);
        skillInfoFrame.SetActive(false);
        skillInfoFrame.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        skillInfoFrame.SetActive(false);
    }
}
