using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    MemoryPool m_pool;
    Camera cam;
    float speed = 1.5f;

    float alphaSpeed = 2f;

    TextMeshPro textMesh;
    Color color=Color.white;

    private void Awake()
    {
        cam = Camera.main;
        textMesh = GetComponent<TextMeshPro>();
    }

    private void OnEnable()
    {
        color.a = 1f;
        
        textMesh.color = color;
        transform.localScale = Vector3.zero;
    }

    public void Init(MemoryPool p_pool)
    {
        m_pool = p_pool;
        StartCoroutine(ColorChange());
        StartCoroutine(AutoDestroy());
    }

    private void Update()
    {
        Quaternion q_hp = Quaternion.LookRotation(transform.position - cam.transform.position);
        Vector3 hp_angle = Quaternion.RotateTowards(transform.rotation, q_hp, 1000).eulerAngles;
        transform.rotation = Quaternion.Euler(hp_angle.x, hp_angle.y, 0);
        transform.Translate(new Vector3(0,speed*Time.deltaTime,0)); 
    }

    private IEnumerator ColorChange()
    {
        float percent = 0.0f;
        float currentTime = 0.0f;
        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / alphaSpeed;

            color.a = Mathf.Lerp(color.a, 0, percent);
            textMesh.color = color;
            yield return null;
        }
    }

    private IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(0.6f);
        m_pool.DeactivePoolItem(gameObject);
    }
}
