using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public enum TurtleState {  None=-1, Wander,Pursuit,Attack,Die}

public class TurtleShell : MonoBehaviour
{
    [SerializeField] Transform hpBar;
    Slider hpSlider;
    Camera cam;

    public float maxHp = 100f;
    public float atk = 15f;
    float curHp;
    bool isDie = false;
    bool isWalk;
    Animator anim;
    NavMeshAgent navMeshAgent;
    Transform target;
    float pursuitLimitRange = 2f;
    float attackRange = 1f;
    float attackRate = 1f;
    float lastAttackTime = 0;

    float m_exp = 10f;

    GameObject m_ExpDropPrefab;

    public TurtleState turtleState = TurtleState.None;
    public SkinnedMeshRenderer skinnedMeshRenderer;
    BoxCollider attackCollider;

    ExpDropMemoryPool expDropMemoryPool;
    Transform expOriginParent;

    DamageTextMemoryPool damageTextMemoryPool;

    DropItemMemoryPool dropItemMemoryPool;
    private void Awake()
    {
        dropItemMemoryPool=GetComponent<DropItemMemoryPool>();
        expDropMemoryPool =GetComponent<ExpDropMemoryPool>();
        damageTextMemoryPool=GetComponent<DamageTextMemoryPool>();
        cam =Camera.main;
        isDie = false;
        hpSlider=GetComponentInChildren<Slider>();
        anim = GetComponent<Animator>();
        curHp = maxHp;
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        hpBar.gameObject.SetActive(false);
    }
    public void Init(Transform p_target,GameObject expDropPrefab,Transform p_expOriginParent)
    {
        expOriginParent = p_expOriginParent;
        attackCollider = GetComponent<BoxCollider>();
        target = p_target;
        m_ExpDropPrefab = expDropPrefab;
        ChangeState(TurtleState.Wander);
    }

    private void Update()
    {
        Quaternion q_hp = Quaternion.LookRotation(hpBar.position - cam.transform.position);
        Vector3 hp_Angle = Quaternion.RotateTowards(hpBar.rotation, q_hp, 1000).eulerAngles;
        hpBar.rotation = Quaternion.Euler(hp_Angle.x, hp_Angle.y, 0);
        hpSlider.value = curHp / maxHp;
    }

    public void ChangeState(TurtleState newState)
    {
        if (turtleState == newState) return;

        StopCoroutine(turtleState.ToString());

        turtleState = newState;

        StartCoroutine(turtleState.ToString());
    }

    IEnumerator Wander()
    {
        if (isDie == true) yield break;

        isWalk = true;
        anim.SetBool("isWalk",isWalk);

        navMeshAgent.speed = 0.5f;

        navMeshAgent.SetDestination(CalculateWanderPosition());

        Vector3 to = new Vector3(navMeshAgent.destination.x,0.4f,navMeshAgent.destination.z);
        Vector3 from = new Vector3(transform.position.x, 0.4f, transform.position.z);
        transform.rotation = Quaternion.LookRotation(to - from);

        while (true)
        {
            to = new Vector3(navMeshAgent.destination.x, 0.4f, navMeshAgent.destination.z);
            from = new Vector3(transform.position.x, 0.4f, transform.position.z);

            if ((to - from).magnitude < 0.01f)
            {
                navMeshAgent.SetDestination(CalculateWanderPosition());
            }

            CalculateDistanceToTargetAndSelectState();

            yield return null;  
        }
    }

    Vector3 CalculateWanderPosition()
    {
        float wanderRadius = 2.5f;
        int wanderJitter = 0;
        int wanderJitterMin = 0;
        int wanderJitterMax = 360;

        wanderJitter=Random.Range(wanderJitterMin,wanderJitterMax);
        Vector3 targetPosition = transform.position + SetAngle(wanderRadius, wanderJitter);

        return targetPosition;
    }

    Vector3 SetAngle(float radius,int angle)
    {
        Vector3 position = Vector3.zero;

        position.x = Mathf.Cos(angle)*radius;
        position.z = Mathf.Sin(angle)*radius;

        return position;    
    }

    IEnumerator Pursuit()
    {
        if(isDie==true) yield break;

        while (true)
        {
            navMeshAgent.SetDestination(target.position);

            LookRotationToTarget();

            CalculateDistanceToTargetAndSelectState();

            yield return null;
        }
    }

    void LookRotationToTarget()
    {
        if(isDie ==true) return;

        Vector3 to = new Vector3(target.position.x,0,target.position.z);

        Vector3 from= new Vector3(transform.position.x,0,transform.position.z);

        transform.rotation = Quaternion.LookRotation(to - from);
    }

    void CalculateDistanceToTargetAndSelectState()
    {
        if (target == null || isDie == true) return;

        float distance = Vector3.Distance(target.position,transform.position);
        if (distance <= attackRange)
        {
            ChangeState(TurtleState.Attack);
        }
        else if (distance <= pursuitLimitRange)
        {
            ChangeState(TurtleState.Pursuit);
        }
        else if (distance >pursuitLimitRange)
        {
            ChangeState(TurtleState.Wander);
        }
    }
    IEnumerator Attack()
    {
        if(isDie==true) yield break;

        isWalk = false;
        anim.SetBool("isWalk", isWalk);

        navMeshAgent.ResetPath();

        while (true)
        {
            LookRotationToTarget();

            CalculateDistanceToTargetAndSelectState();

            if (Time.time - lastAttackTime > attackRate)
            {
                lastAttackTime = Time.time;
                if (isDie == false)
                {
                    anim.SetTrigger("onAttack");
                }
            }
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawRay(transform.position, navMeshAgent.destination - transform.position);

        /*        Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, targetRecognitionRange);*/

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, pursuitLimitRange);

        Gizmos.color = new Color(0.39f, 0.04f, 0.04f);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    IEnumerator Die()
    {
        Manager.instance.quest_Manager.dieTurtleShellCnt++;

        isDie = true;
        anim.SetTrigger("onDie");
        int rand = Random.Range(0, 100);
        if (rand <= 90)
        {
            dropItemMemoryPool.SpawnItem(transform.position);
        }
        expDropMemoryPool.SpawnExpDrop(target, m_exp,expOriginParent,gameObject);
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    void TakeDamage(float damage)
    {
        if (isDie == true) return;

        Manager.instance.sound_Manager.PlaySound(Manager.instance.sound_Manager.enemyDamageClip);

        damageTextMemoryPool.SpawnDamageText(transform.position,damage,1.2f);

        hpBar.gameObject.SetActive(true);

        curHp -= damage;
        StartCoroutine(ChangeColor());
        if (curHp <= 0)
        {
            navMeshAgent.ResetPath();
            ChangeState(TurtleState.Die);
        }
    }

    IEnumerator ChangeColor()
    {
        Color color=skinnedMeshRenderer.material.color;
        skinnedMeshRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        skinnedMeshRenderer.material.color = color;
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerAttackCollider")
        {
            if (Manager.instance.playerStat_Manager.isQSkill == true)
            {
                TakeDamage(Manager.instance.playerStat_Manager.qSkillDamage);
            }
            else
            {
                TakeDamage(Manager.instance.playerStat_Manager.wSkillDamage);
            }
        }

        if(other.tag == "PlayerNormalAttack")
        {
            TakeDamage(Manager.instance.playerStat_Manager.atk);
        }

        if (attackCollider.enabled == true)
        {
            if (other.tag == "Player")
            {
                target.GetComponent<PlayerController>().TakeDamage(atk);
            }
        }
    }
}
