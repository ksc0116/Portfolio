using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowRock : MonoBehaviour
{
    float damage = 15f;
    Transform target;
    Rigidbody rigid;
    GameObject m_dangerRange;
    private void Update()
    {
        rigid.AddTorque(transform.right*2f, ForceMode.Acceleration);
    }
    public void Init(Transform target,GameObject dangerRange)
    {
        m_dangerRange = dangerRange;
        rigid = GetComponent<Rigidbody>();
        this.target=target;
        transform.LookAt(target);
        StartCoroutine(OnMove());
    }

    IEnumerator OnMove()
    {
        float gravity = -20f;
        float currentTime = 0.0f;
        float percent = 0.0f;
        float jumpTime = 2.0f;
        m_dangerRange.SetActive(true);
        m_dangerRange.transform.position = new Vector3(target.position.x, target.position.y+0.5f, target.position.z);
        Vector3 start = transform.position;
        Vector3 end = target.position;
        float v0 = (end - start).y - gravity;
        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / jumpTime;

            Vector3 position = Vector3.Lerp(start, end, percent);

            position.y = start.y + (v0 * percent) + (gravity * percent * percent);

            transform.position = position;

            yield return null;
        }
        m_dangerRange.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Manager.instance.sound_Manager.PlaySound(Manager.instance.sound_Manager.rockBreakClip);
            other.GetComponent<PlayerController>().TakeDamage(damage);
            Manager.instance.camera_Manager.OnShakeCameraPosition(0.1f, 0.5f);
            m_dangerRange.SetActive(false);
            Destroy(gameObject);
        }
        else if(other.tag == "Ground")
        {
            Manager.instance.sound_Manager.PlaySound(Manager.instance.sound_Manager.rockBreakClip);
            m_dangerRange.SetActive(false);
            Manager.instance.camera_Manager.OnShakeCameraPosition(0.1f, 0.1f);
            Destroy(gameObject);
        }
    }
}
