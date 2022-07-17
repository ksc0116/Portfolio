using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimatorController : MonoBehaviour
{
    public bool isAttack = false;
    public BoxCollider attackCollider;
    public BoxCollider skillCollider;
    public GameObject player;

    NavMeshAgent playerNav;
    Rigidbody playerRigid;

    private void OnEnable()
    {
        playerNav = player.GetComponent<NavMeshAgent>();
        playerRigid = player.GetComponent<Rigidbody>();
        attackCollider.enabled = false; 
        skillCollider.enabled = false; 
        Manager.instance.playerStat_Manager.isAttackAble = true;
        Manager.instance.playerStat_Manager.isMoveAble = true;
    }
    public void AttackStart()
    {
        isAttack = true;
        Manager.instance.playerStat_Manager.isMoveAble = false;
    }

    public void AttackEnd()
    {
        isAttack = false;
        Manager.instance.playerStat_Manager.isMoveAble = true;
    }
    
    public void ActionOnOffAttackCollider()
    {
        StartCoroutine(OnOffAttackCollider());
    }
   
    
    IEnumerator OnOffAttackCollider()
    {
        attackCollider.enabled=true;
        yield return new WaitForSeconds(0.1f);
        attackCollider.enabled = false;
    }
    public void ThirdAttackEffect()
    {
        StartCoroutine(OnAttackEffect());
    }
    IEnumerator OnAttackEffect()
    {
        playerNav.enabled = false;
        playerRigid.drag = 1f;
        playerRigid.AddForce(transform.forward * 5f, ForceMode.Impulse);
        yield return new WaitForSeconds(0.1f);
        playerNav.enabled = true;
        playerRigid.drag = 1000f;
    }

    public void OnSkillCollider()
    {
        skillCollider.enabled=true;
    }
    public void OffSkillCollider()
    {
        skillCollider.enabled = false;
        if(Manager.instance.playerStat_Manager.isQSkill==true) Manager.instance.playerStat_Manager.isQSkill=false;
    }

    public void MoveStop()
    {
        Debug.Log("Stop");
        player.GetComponent<NavMeshAgent>().ResetPath();
        Manager.instance.playerStat_Manager.isMoveAble = false;
        Manager.instance.playerStat_Manager.isAttackAble = false;
    }
    public void MoveAble()
    {
        Debug.Log("Able");
        Manager.instance.playerStat_Manager.isMoveAble = true;
        Manager.instance.playerStat_Manager.isAttackAble = true;
    }
}
