using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    public bool isAttack = false;
    public BoxCollider attackCollider;

    public void AttackStart()
    {
        isAttack = true;
    }

    public void AttackEnd()
    {
        isAttack = false;
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
}
