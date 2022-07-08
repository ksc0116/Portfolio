using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXP_Drop : MonoBehaviour
{
    public Transform target;
    float m_exp;
    float speed = 0.01f;
    Vector3 velocity = Vector3.zero;
    Animator anim;
    public bool isFollow = false;

    MemoryPool m_pool;

    Transform originParent;

    GameObject removeObj;
    public void Init(Transform p_target,float p_exp, MemoryPool p_pool, Transform p_originParent,GameObject p_removeObj)
    {
        originParent = p_originParent;
        transform.SetParent(originParent);
        removeObj= p_removeObj;
        m_pool = p_pool;
        m_exp = p_exp;
        target=p_target;
    }
    public void StartFollow()
    {
        anim=GetComponent<Animator>();
        anim.enabled = false;
        isFollow = true;
    }
    private void Update()
    {
        if (isFollow == true)
        {
            transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity,Time.deltaTime *speed);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.SetParent(originParent);
            m_pool.DeactivePoolItem(gameObject);
            Destroy(removeObj);
            Manager.instance.playerStat_Manager.curExp += m_exp;
        }
    }
}
