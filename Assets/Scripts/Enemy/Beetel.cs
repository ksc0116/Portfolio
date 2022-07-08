using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Beetel : MonoBehaviour
{
    [SerializeField] Transform hpBar;
    Slider hpSlider;
    Camera cam;

    public GameObject expDropPrefab;

    public Transform target;
    public Transform[] patrolPositions;
    public int wayIndex = 0;
    public float moveSpeed = 1f;
    float pursuitRange = 2.5f;
    float attackRange = 1.5f;
    bool isPursuit=false;
    bool isRun;
    bool isAttack = false;
    bool isDie=false;
    float dist = 0;
    float lastAttackTime = 0f;
    float attackRate = 1f;
    float atk = 20f;
    float maxHp = 100f;
    float curHp;
    float m_exp = 35f;
    Animator anim;
    BoxCollider attackCollider;
    SkinnedMeshRenderer skinnedMeshRenderer;

    ExpDropMemoryPool m_pool;
    public Transform expOriginParent;

    DamageTextMemoryPool damageTextMemoryPool;
    private void Awake()
    {
        m_pool=GetComponent<ExpDropMemoryPool>();
        damageTextMemoryPool=GetComponent<DamageTextMemoryPool>();
        cam = Camera.main;
        isDie = false;
        curHp = maxHp;
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        attackCollider =GetComponent<BoxCollider>();
        anim = GetComponent<Animator>();    
        wayIndex = 0;
        transform.LookAt(patrolPositions[wayIndex].position);
        hpSlider= GetComponentInChildren<Slider>();   
        hpBar.gameObject.SetActive(false);
    }

    private void Update()
    {
        Quaternion q_hp = Quaternion.LookRotation(hpBar.position - cam.transform.position);
        Vector3 hp_Angle = Quaternion.RotateTowards(hpBar.rotation, q_hp, 1000).eulerAngles;
        hpBar.rotation = Quaternion.Euler(hp_Angle.x, hp_Angle.y, 0);
        hpSlider.value = curHp / maxHp;

        if (isPursuit == false)
        {
            if (isDie == true) return;

            isRun = false;
            anim.SetBool("isRun", isRun);
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
        }
        else if (isPursuit == true)
        {
            if (isDie == true) return;

            isRun = true;
            anim.SetBool("isRun", isRun);
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
        }

        dist = Vector3.Distance(transform.position, patrolPositions[wayIndex].position);



        Pursuit();

        Attack();

        if (dist < 1f)
        {
            IncreaseIndex();
        }
    }
    void Attack()
    {
        if (isDie == true) return;

        if (Vector3.Distance(transform.position, target.position) <= attackRange)
        {
            isAttack = true;
            anim.SetBool("isAttack", isAttack);
            moveSpeed = 0.0f;
            if (Time.time-lastAttackTime > attackRate)
            {
                lastAttackTime = Time.time;

                Debug.Log("АјАн");
                anim.SetTrigger("onAttack");
            }
        }
        else
        {
            isAttack = false;
            anim.SetBool("isAttack", isAttack);
        }
    }
    void Pursuit()
    {
        if (isDie == true) return;

        if(Vector3.Distance(transform.position, target.position) <= pursuitRange)
        {
            moveSpeed = 1.5f;
            transform.LookAt(target.position);

            isPursuit = true;

            isRun = true;
            anim.SetBool("isRun", isRun);
        }
        else
        {
            moveSpeed = 1f;

            isPursuit = false;

            isRun = false;
            anim.SetBool("isRun", isRun);

            transform.LookAt(patrolPositions[wayIndex].position);
        }
    }

    void IncreaseIndex()
    {
        wayIndex++;
        if(wayIndex >= patrolPositions.Length)
        {
            wayIndex = 0;
        }
        transform.LookAt(patrolPositions[wayIndex].position);
    }

    public void ActionOnOffAttackCollider()
    {
        StartCoroutine(OnOffAttackCollider());
    }

    IEnumerator OnOffAttackCollider()
    {
        attackCollider.enabled = true;
        yield return new WaitForSeconds(0.1f);
        attackCollider.enabled = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, pursuitRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    void TakeDamage(float damage)
    {
        if (isDie == true) return;
        hpBar.gameObject.SetActive(true);
        curHp-=damage;
        damageTextMemoryPool.SpawnDamageText(transform.position,damage,1.8f);
        StartCoroutine(ChangeColor());
        if(curHp <= 0)
        {
            StartCoroutine( Die() );
        }
    }
    IEnumerator Die()
    {
        Manager.instance.quest_Manager.dieBeetleCnt++;
        Manager.instance.quest_Manager.UpdateCurQuestText();

        isDie = true;
        anim.SetTrigger("onDie");
        m_pool.SpawnExpDrop(target, m_exp,expOriginParent,gameObject);

        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
    IEnumerator ChangeColor()
    {
        Color color = skinnedMeshRenderer.material.color;
        skinnedMeshRenderer.material.color=Color.red;
        yield return new WaitForSeconds(0.1f);
        skinnedMeshRenderer.material.color = color;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerAttackCollider")
        {
            TakeDamage(Manager.instance.playerStat_Manager.atk);
        }

        if (attackCollider.enabled == true)
        {
            if(other.tag == "Player")
            {
                other.GetComponent<PlayerController>().TakeDamage(atk);
            }
        }
    }
}
