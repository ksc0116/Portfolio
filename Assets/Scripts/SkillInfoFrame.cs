using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SkillInfoFrame : MonoBehaviour
{
    SkillInfo skillInfo;
    [SerializeField] TextMeshProUGUI useMpText;
    [SerializeField] TextMeshProUGUI coolTimeText;
    [SerializeField] TextMeshProUGUI infoText;
    [SerializeField] Image skiilIcon;
    public void GetSkillInfo(SkillInfo p_skillInfo)
    {
        skillInfo = p_skillInfo;
    }
    private void OnEnable()
    {
        skiilIcon.sprite = skillInfo.skillSprite;
        useMpText.text = $"���� {skillInfo.useMp} �Ҹ�";
        coolTimeText.text = $"���� ���ð� {skillInfo.coolTime}��";
        infoText.text = skillInfo.format;
    }
}
