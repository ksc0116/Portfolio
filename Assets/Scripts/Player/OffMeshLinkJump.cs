using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OffMeshLinkJump : MonoBehaviour
{
    [SerializeField] float jumpSpeed = 5.0f;
    [SerializeField] float gravity = -9.81f;
    NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        StartCoroutine(ResetCheck());
    }

    IEnumerator ResetCheck()
    {
        yield return new WaitUntil(() => IsOnJump());

        yield return StartCoroutine(JumpTo());
    }

    public bool IsOnJump()
    {
        if (navMeshAgent.isOnOffMeshLink)
        {
            OffMeshLinkData linkData = navMeshAgent.currentOffMeshLinkData;

            if(linkData.linkType == OffMeshLinkType.LinkTypeJumpAcross ||
                linkData.linkType == OffMeshLinkType.LinkTypeDropDown)
            {
                return true;
            }
        }
        return false;
    }

    IEnumerator JumpTo()
    {
        navMeshAgent.isStopped = true;

        OffMeshLinkData linkData = navMeshAgent.currentOffMeshLinkData;
        Vector3 start = linkData.startPos;
        Vector3 end = linkData.endPos;

        float jumpTime = Mathf.Max(0.3f, Vector3.Distance(start, end) / jumpSpeed);
        float currentTime = 0.0f;
        float percent = 0.0f;

        float v0 = (end - start).y - gravity;

        while (percent < 1)
        {
            currentTime+=Time.deltaTime;
            percent = currentTime / jumpTime;

            Vector3 position = Vector3.Lerp(start,end,percent);

            position.y = start.y + (v0 * percent) + (gravity * percent * percent);

            transform.position = position;

            yield return null;
        }

        navMeshAgent.CompleteOffMeshLink();

        navMeshAgent.isStopped=false;

        StartCoroutine(ResetCheck());
    }
}
