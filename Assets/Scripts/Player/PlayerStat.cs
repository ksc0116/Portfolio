using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStat : MonoBehaviour
{
    [SerializeField] GameObject levelUpText;
    [SerializeField] GameObject levelUpParticle;

    public float playerGold = 200;

    public float curHp=100f;
    public float maxHP = 100f;

    public float curMP=100f;
    public float maxMP=100f;


    public int lev = 1;
    public float maxExp = 100f;
    public float curExp = 0f;

    public float atk=50f;
    public float originAtk=50f;
    public float atk_Bonus = 0f;

    public float def = 30f;
    public float originDef= 30;
    public float def_Bonus = 0f;

    public float moveSpeed=4f;
    public bool isMoveAble = true;
    public bool isAttackAble = true;
    public GameObject player;

    public bool isPortal = false;

    public int qSkillDamage = 50;
    public int wSkillDamage = 30;
    public float qSkillUseMp = 10f;
    public float wSkillUseMp = 20f;
    public float eSkillUseMp = 5f;
    public bool isQSkill = false;
    public bool isWSkill = false;

    private void Update()
    {
        if (curExp >= maxExp)
        {
            Manager.instance.sound_Manager.PlaySound(Manager.instance.sound_Manager.levelUpClip);
            lev++;
            qSkillUseMp += 5f;
            wSkillUseMp += 5f;
            qSkillDamage += 10;
            wSkillDamage += 15;
            maxHP += 50f;
            maxMP += 100f;
            atk += 5f;
            originAtk += 5f;
            levelUpParticle.SetActive(true);
            levelUpText.SetActive(true);
            curHp = maxHP;
            curMP = maxMP;
            curExp = curExp - maxExp == 0 ? 0 : curExp - maxExp;
        }
    }
}
