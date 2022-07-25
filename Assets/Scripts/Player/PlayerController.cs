using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject backGroundPanel;
    [SerializeField] GameObject pausePanel;

    [SerializeField] BoxCollider attackCollider;
    [SerializeField] BoxCollider skillCollider;

    [SerializeField] GameObject expGetEffect;

    [SerializeField] GameObject playerWeapon;

    [SerializeField] LayerMask layerMask;

    [Header("[Inven & Stat]")]
    [SerializeField] GameObject invenFrame;
    [SerializeField] GameObject statAndEquipFrame;

    [SerializeField] GameObject touchEffect;

    [SerializeField] PlayerAnimatorController playerAnim;

    NavMeshAgent playerNav;
    Camera cam;
    Animator anim;
    public bool isMove=false;
    RaycastHit hit;
    DamageTextMemoryPool damageTextMemoryPool;
    private void OnEnable()
    {
        attackCollider.enabled=false;
        skillCollider.enabled = false;
    }
    private void Awake()
    {
        damageTextMemoryPool=GetComponent<DamageTextMemoryPool>();
        anim = GetComponentInChildren<Animator>();
        cam = Camera.main;
        playerNav = GetComponent<NavMeshAgent>();
        playerNav.updateRotation = false;
        playerNav.speed = Manager.instance.playerStat_Manager.moveSpeed;
    }
    void Update()
    {
        if (Manager.instance.equip_Manager.isWeaponEquip == true)
        {
            playerWeapon.SetActive(true);
        }
        else
        {
            playerWeapon.SetActive(false);
        }

        if (Input.GetMouseButton(1) && playerAnim.isAttack==false && EventSystem.current.IsPointerOverGameObject()==false && Manager.instance.playerStat_Manager.isMoveAble==true
            && Manager.instance.playerStat_Manager.isDie==false)
        {
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity ,layerMask))
            {
                touchEffect.SetActive(false);
                touchEffect.transform.position=new Vector3(hit.point.x,hit.point.y+0.05f,hit.point.z);
                touchEffect.SetActive(true);
                SetDestination(hit.point);
            }
        }

        LookMoveDirection();

        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false && Manager.instance.equip_Manager.isWeaponEquip==true && Manager.instance.playerStat_Manager.isAttackAble==true
            && Manager.instance.playerStat_Manager.isDie == false)
        {
            anim.SetTrigger("onAttack");
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
            {
                LookAttackDirection(hit.point);
            }
        }

        PlayerArrive();

        if (Input.GetKeyDown(KeyCode.I))
        {
            OnOffFrame(invenFrame);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            OnOffFrame(statAndEquipFrame);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && mainMenuPanel.activeSelf==false)
        {
            OnOffFrame(pausePanel);
            OnOffFrame(backGroundPanel);
        }
    }

    void PlayerArrive()
    {
        if (playerNav.velocity.magnitude <= 0.01f)
        {
            isMove = false;
            anim.SetBool("isMove", isMove);
        }
        else
        {
            isMove=true;
            anim.SetBool("isMove",isMove);
        }
    }

    void LookAttackDirection(Vector3 dest)
    {
        playerNav.SetDestination(transform.position);

        isMove = true;
        anim.SetBool("isMove", isMove);

        Vector3 to = new Vector3(dest.x, 0, dest.z);
        Vector3 from = new Vector3(anim.transform.position.x, 0, anim.transform.position.z);

        Quaternion rotation = Quaternion.LookRotation(to - from);
        anim.transform.rotation = rotation;
    }


    void SetDestination(Vector3 dest)
    {
        playerNav.SetDestination(dest);
        isMove = true;
        anim.SetBool("isMove",isMove);
    }

    void LookMoveDirection()
    {
        if (isMove==true)
        {
            Vector3 to = new Vector3(playerNav.steeringTarget.x, 0, playerNav.steeringTarget.z);
            Vector3 from = new Vector3(anim.transform.position.x, 0, anim.transform.position.z);

            Quaternion rotation = Quaternion.LookRotation(to - from);
            anim.transform.rotation = Quaternion.Slerp(anim.transform.rotation, rotation, 0.05f);
        }
    }

    void OnOffFrame(GameObject target)
    {
        target.SetActive(!target.activeSelf);
    }

    public void TakeDamage(float damage)
    {
        Manager.instance.playerStat_Manager.curHp-=damage;
        damageTextMemoryPool.SpawnDamageText(transform.position, damage, 1.5f,true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ExpDrop")
        {
            if (other.GetComponent<EXP_Drop>().isFollow == true)
            {
                StopCoroutine(ExpGet());
                StartCoroutine(ExpGet());
            }
        }
    }

    IEnumerator ExpGet()
    {
        expGetEffect.SetActive(true);
        yield return new WaitForSeconds(1f);
        expGetEffect.SetActive(false);
    }
}
