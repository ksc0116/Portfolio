using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;

public class QuickSkillFrame : MonoBehaviour
{
    //[SerializeField] Material dashMat;
    [SerializeField] TextMeshProUGUI qSkillUseMp;
    [SerializeField] TextMeshProUGUI wSkillUseMp;

    [SerializeField] GameObject playerBody;
    [SerializeField] Transform playerTransform;

    [SerializeField] Animator playerAnim;

    [SerializeField] Image qCoolTimeImage;
    float qSkillCoolTime=3f;
    bool isQ = false;

    [SerializeField] Image wCoolTimeImage;
    float wSkillCoolTime = 6f;
    bool isW = false;   

    [SerializeField] Image eCoolTimeImage;
    [SerializeField] GameObject[] motionTrail;
    float eSkillCoolTime = 4f;
    bool isE = false;
    private void Update()
    {
        qSkillUseMp.text = (Manager.instance.playerStat_Manager.qSkillUseMp).ToString();
        wSkillUseMp.text = (Manager.instance.playerStat_Manager.wSkillUseMp).ToString();
        if (Input.GetKeyDown(KeyCode.Q) && (Manager.instance.playerStat_Manager.isAttackAble == true) && (isQ == false) && (Manager.instance.playerStat_Manager.curMP>0) && (Manager.instance.playerStat_Manager.curMP>=Manager.instance.playerStat_Manager.qSkillUseMp)
            && Manager.instance.equip_Manager.isWeaponEquip == true)
        {
            // Q ½½·Ô
            Manager.instance.playerStat_Manager.isQSkill = true;
            playerAnim.SetTrigger("onQ");
            Manager.instance.playerStat_Manager.curMP -= Manager.instance.playerStat_Manager.qSkillUseMp;
            qCoolTimeImage.gameObject.SetActive(true);
            StartCoroutine(qCoolTimeStart());
        }
        else if (Input.GetKeyDown(KeyCode.W) && (Manager.instance.playerStat_Manager.isAttackAble == true) && (isW == false) && (Manager.instance.playerStat_Manager.curMP > 0) && (Manager.instance.playerStat_Manager.curMP >= Manager.instance.playerStat_Manager.wSkillUseMp)
            && Manager.instance.equip_Manager.isWeaponEquip == true)
        {
            // W ½½·Ô
            Manager.instance.playerStat_Manager.isWSkill = true;
            playerAnim.SetTrigger("onW");
            Manager.instance.playerStat_Manager.curMP -= Manager.instance.playerStat_Manager.wSkillUseMp;
            wCoolTimeImage.gameObject.SetActive(true);
            StartCoroutine(wCoolTimeStart());
        }
        else if (Input.GetKeyDown(KeyCode.E) && (isE==false) && (Manager.instance.playerStat_Manager.curMP > 0) && (Manager.instance.playerStat_Manager.curMP >= Manager.instance.playerStat_Manager.eSkillUseMp))
        {
            // E ½½·Ô
            Manager.instance.playerStat_Manager.curMP -= Manager.instance.playerStat_Manager.eSkillUseMp;
            eCoolTimeImage.gameObject.SetActive(true);
            StartCoroutine(eCoolTimeStart());
        }
    }
    // È¸Àü °ø°Ý
    IEnumerator qCoolTimeStart()
    {
        isQ = true;
        float tempCoolTime = qSkillCoolTime;

        while (tempCoolTime > 0)
        {
            tempCoolTime-=Time.deltaTime;
            qCoolTimeImage.fillAmount = tempCoolTime/ qSkillCoolTime;
            yield return null;
        }
        isQ = false;
        qCoolTimeImage.gameObject.SetActive(false);
    }

    IEnumerator wCoolTimeStart()
    {
        isW = true;
        float tempCoolTime = wSkillCoolTime;
        while(tempCoolTime > 0)
        {
            tempCoolTime -= Time.deltaTime;
            wCoolTimeImage.fillAmount=tempCoolTime/ wSkillCoolTime;
            yield return null;
        }
        isW = false;
        wCoolTimeImage.gameObject.SetActive(false);
    }

    // ´ë½¬
    IEnumerator eCoolTimeStart()
    {
        StartCoroutine(Dash());
        isE = true;
        float tempCoolTime = 3.5f;
        while(tempCoolTime > 0)
        {
            tempCoolTime-=Time.deltaTime;
            eCoolTimeImage.fillAmount=tempCoolTime/ eSkillCoolTime;
            yield return null;
        }
        isE = false;
        
        eCoolTimeImage.gameObject.SetActive(false);
    }
    IEnumerator Dash()
    {
        Manager.instance.sound_Manager.PlaySound(Manager.instance.sound_Manager.dashClip);
        for(int i = 0; i < motionTrail.Length; i++)
        {
            motionTrail[i].transform.localScale = Vector3.one;
        }
        Manager.instance.playerStat_Manager.isMoveAble = false;
        Rigidbody rigid = playerBody.GetComponent<Rigidbody>();
        NavMeshAgent nav = playerBody.GetComponent<NavMeshAgent>();
        nav.enabled = false;
        rigid.drag = 1f;
        rigid.AddForce(playerTransform.forward * 30f, ForceMode.Impulse);
        yield return new WaitForSeconds(0.1f);
        nav.enabled = true;
        rigid.drag = 1000f;
        Manager.instance.playerStat_Manager.isMoveAble = true;
        yield return new WaitForSeconds(0.15f);
        for (int i = 0; i < motionTrail.Length; i++)
        {
            motionTrail[i].transform.localScale = Vector3.zero;
        }
    }
}
