using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private void Update()
    {
        if (curExp >= maxExp)
        {
            lev++;
            levelUpParticle.SetActive(true);
            levelUpText.SetActive(true);
            curHp = maxHP;
            curMP = maxMP;
            curExp = curExp - maxExp == 0 ? 0 : curExp - maxExp;
        }
    }
}
