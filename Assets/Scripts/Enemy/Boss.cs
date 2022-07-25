using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;

public class Boss : MonoBehaviour
{
    [SerializeField] GameObject playerStunObj;
    [SerializeField] GameObject portal;
    [SerializeField] TextMeshProUGUI hpPercent;
    [SerializeField] TextMeshProUGUI hpCntText;
    [SerializeField] Image hpBarImage;
    [SerializeField] Image hpBarImage_Bg;
    [SerializeField] Image hpBarLastBack;
    [SerializeField] GameObject throwPrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject dangerRange;
    [SerializeField] GameObject smallDangerRange;
    [SerializeField] LayerMask layerMask;
    [SerializeField] LayerMask layerMask2;
    [SerializeField] GameObject earthQuakeEffectPrefab;
    [SerializeField] GameObject rockPrefab;
    [SerializeField] GameObject player;
    [SerializeField] Transform playerRotation;
    [SerializeField] Animator playerAnimator;

    [SerializeField] SphereCollider jumpAttackCollider;
    
    CapsuleCollider bodyCollider;

    float jumpAttackDamage = 20f;
    float moveSpeed = 2f;

    Rigidbody rigid;
    Animator anim;
    bool isStun=true;
    bool isSkill = false;
    float coolTime = 3f;
    float lastPattern = 0f;
    int patternIndex = 0;
    public float curHP = 0f;
    float maxHP = 300f;
    SkinnedMeshRenderer skinnedMeshRenderer;
    bool isDie = false;
    int hpBarIndex;
    int hpCnt;
    private void Awake()
    {
        hpBarLastBack.color = Color.green;
        hpBarIndex = (int)maxHP-100;
        hpCnt = hpBarIndex / 100;
        hpCntText.text=$"x{hpCnt}";
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        curHP = maxHP;
        bodyCollider=GetComponent<CapsuleCollider>();
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        anim.SetBool("isSkill", isSkill);

        if((curHP - hpBarIndex < 0) && curHP>=0)
        {
            hpCnt--;
            hpCntText.text = $"x{hpCnt}";
            hpBarIndex -= 100;
            hpBarImage.transform.localScale = Vector3.one;
            hpBarImage_Bg.transform.localScale = Vector3.one;
            if(hpBarIndex<=100)
            {
                hpBarImage.color = Color.green;
                hpBarLastBack.color = Color.blue;
            }
            if (hpBarIndex <=0)
            {
                hpBarImage.color= Color.blue;
                hpBarLastBack.color = Color.black;
            }
        }
        hpBarImage.transform.localScale = new Vector3(curHP<=0 ? 0 :  ((curHP - hpBarIndex) / 100), 1, 1);
        hpBarImage_Bg.transform.localScale = Vector3.Lerp(hpBarImage_Bg.transform.localScale, new Vector3(curHP <= 0 ? 0 : ((curHP - hpBarIndex) / 100), 1, 1), 5f * Time.deltaTime);

        if (Manager.instance.camera_Manager.isBossMove == true && isDie==false)
        {
            lastPattern += Time.deltaTime;
            if (lastPattern > coolTime)
            {
                lastPattern = 0.0f;
                patternIndex = Random.Range(0, 100);
                if (patternIndex < 35) StartCoroutine("JumpAttack");
                else if (patternIndex > 35 && patternIndex < 65) StartCoroutine("PursuitAttack");
                else anim.SetTrigger("onThrow");
            }
        }

        if (Manager.instance.camera_Manager.isBossMove == true && isSkill==false && isDie==false)
        {
            Move();
            LookTarget();
        }

        if (isStun == true && isDie == false)
        {
            if (rigid.velocity.y < 0)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.7f, layerMask))
                {
                    Manager.instance.sound_Manager.PlaySound(Manager.instance.sound_Manager.bossSkillClip);
                    lastPattern = 0.0f;
                    Instantiate(earthQuakeEffectPrefab, hit.point, Quaternion.identity);
                    Manager.instance.camera_Manager.OnShakeCameraPosition(0.2f, 1f);
                    Instantiate(rockPrefab, hit.point, Quaternion.identity);
                    transform.position= hit.point;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine( JumpAttack() );
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine( PursuitAttack() );
        }
    }
    void Move()
    {
        Vector3 moveDir = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, moveDir, moveSpeed*Time.deltaTime);
    }
    void LookTarget()
    {
        Vector3 to = new Vector3(player.transform.position.x, 9.3f, player.transform.position.z);

        Vector3 from = new Vector3(transform.position.x, 9.3f, transform.position.z);

        Quaternion rotation = Quaternion.LookRotation(to - from);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.05f);
    }
    public void ThrowAni()
    {
        StartCoroutine(ThrowRock());
    }
    IEnumerator ThrowRock()
    {
        isSkill = true;
        LookTarget();
        GameObject rock = Instantiate(throwPrefab, spawnPoint.position, Quaternion.identity);
        Manager.instance.sound_Manager.PlaySound(Manager.instance.sound_Manager.attackClip);
        rock.GetComponent<ThrowRock>().Init(player.transform,smallDangerRange);
        yield return new WaitForSeconds(0.5f);
        isSkill = false;
        lastPattern = 0.0f;
    }
    IEnumerator PursuitAttack()
    {
        isSkill = true;
        lastPattern = 0.0f;
        dangerRange.SetActive(true);
        dangerRange.transform.position= new Vector3(player.transform.position.x, player.transform.position.y+ 0.01f, player.transform.position.z);
        Vector3 start = transform.position;
        Vector3 end = player.transform.position;
        yield return new WaitForSeconds(2f);
        lastPattern = 0.0f;
        dangerRange.SetActive(false);
        isStun = false;
        anim.SetTrigger("onJumpAttack");
        float gravity = -9.81f;
        float percent = 0.0f;
        float currentTime = 0.0f;
        
        float jumpTime = 1f;
        float v0 = (end - start).y - gravity;

        transform.rotation=Quaternion.LookRotation(playerRotation.position -   transform.position);

        while (percent < 1)
        {
            lastPattern = 0.0f;
            currentTime += Time.deltaTime;
            percent = currentTime / jumpTime;

            Vector3 position = Vector3.Lerp(start, end, percent);

            position.y = start.y + (v0 * percent) + (gravity * percent * percent);

            transform.position = position;

            yield return null;
        }
        lastPattern = 0.0f;
        Manager.instance.sound_Manager.PlaySound(Manager.instance.sound_Manager.bossSkillClip);
        Instantiate(earthQuakeEffectPrefab, transform.position, Quaternion.identity);
        Manager.instance.camera_Manager.OnShakeCameraPosition(0.2f, 1f);
        Instantiate(rockPrefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        lastPattern = 0.0f;
        isSkill = false;
        bodyCollider.enabled = false;
        bodyCollider.enabled = true;
    }
    IEnumerator PlayerStun()
    {
        playerStunObj.SetActive(true);
        Manager.instance.sound_Manager.PlaySound(Manager.instance.sound_Manager.stunClip);
        player.GetComponent<NavMeshAgent>().ResetPath();
        Manager.instance.playerStat_Manager.isMoveAble = false;
        Manager.instance.playerStat_Manager.isAttackAble = false;
        playerAnimator.SetTrigger("onStun");
        yield return new WaitForSeconds(4f);
        playerStunObj.SetActive(false);
        Manager.instance.playerStat_Manager.isMoveAble = true;
        Manager.instance.playerStat_Manager.isAttackAble = true;
    }
    public void JumpAttackOn()
    {
        StartCoroutine(JumpAttackColliderOnOff());
    }
    IEnumerator JumpAttackColliderOnOff()
    {
        jumpAttackCollider.enabled = true;
        yield return new WaitForSeconds(0.1f);
        jumpAttackCollider.enabled = false;
    }

    IEnumerator JumpAttack()
    {
        rigid.isKinematic = false;
        lastPattern = 0.0f;
        isSkill = true;
        isStun= true;
        dangerRange.SetActive(true);
        dangerRange.transform.position = new Vector3(transform.position.x, transform.position.y+ 0.2f, transform.position.z);
        yield return new WaitForSeconds(2f);
        lastPattern = 0.0f;
        dangerRange.SetActive(false);
        anim.SetTrigger("onJump");
        rigid.AddForce(Vector3.up*20f,ForceMode.Impulse);
        yield return new WaitForSeconds(0.5f);
        lastPattern = 0.0f;
        rigid.AddForce(Vector3.down * 60f, ForceMode.Impulse);
        yield return new WaitForSeconds(1f);
        lastPattern = 0.0f;
        isSkill = false;
        rigid.isKinematic = true;

    }
    public void DieAnimation()
    {
        StartCoroutine(ChageTimeSclae());
    }
    IEnumerator ChageTimeSclae()
    {
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 1f;
    }
    IEnumerator Die()
    {
        dangerRange.SetActive(false);
        Manager.instance.quest_Manager.dieBossCnt++;
        StopCoroutine("PursuitAttack");
        StopCoroutine("JumpAttack");
        isDie = true;
        bodyCollider.enabled = false;
        anim.SetTrigger("onDie");
        yield return new WaitForSeconds(8f);
        Destroy(gameObject);
        hpBarImage.transform.parent.gameObject.SetActive(false);
        portal.SetActive(true);
    }
    void TakeDamage(float damage)
    {
        if (isDie == true) return;

        Manager.instance.sound_Manager.PlaySound(Manager.instance.sound_Manager.enemyDamageClip);

        curHP -= damage;
        
        int hpText = (int)(curHP * 100 / maxHP) < 0 ? 0 : (int)(curHP * 100 / maxHP);
        hpPercent.text = $"{hpText}%";

        StartCoroutine( ChangeColor() );
        if (curHP <= 0)
        {
            StartCoroutine( Die() );
        }
    }

    IEnumerator ChangeColor()
    {
        Color color = skinnedMeshRenderer.material.color;
        skinnedMeshRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        skinnedMeshRenderer.material.color = color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BossTrigger")
        {
            Manager.instance.camera_Manager.isTrigger = true;
        }

        if (jumpAttackCollider.enabled == true)
        {
            if (other.tag == "Player")
            {
                if (isStun == true)
                {
                    StartCoroutine(PlayerStun());
                }
                other.GetComponent<PlayerController>().TakeDamage(jumpAttackDamage);
            }
        }

        if (other.tag == "PlayerNormalAttack")
        {
            TakeDamage(Manager.instance.playerStat_Manager.atk);
        }

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
    }
}
