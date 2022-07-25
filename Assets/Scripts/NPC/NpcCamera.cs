using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCamera : MonoBehaviour
{
    Vector3 originPos;
    [SerializeField] Transform targetPos;
    float moveSpeed = 2f;
    private void Awake()
    {
        originPos = transform.position;
    }

    private void OnEnable()
    {
        StartCoroutine(GoTarget());
    }
    IEnumerator GoTarget()
    {
        while (Vector3.Distance(transform.position, targetPos.position) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos.position, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
    private void OnDisable()
    {
        transform.position = originPos;
    }
}
