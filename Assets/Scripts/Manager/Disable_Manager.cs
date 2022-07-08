using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disable_Manager : MonoBehaviour
{
    [SerializeField] GameObject destroyParticlePrefab;
    [SerializeField] GameObject downParticlePrefab;
    public void DestroyObject(GameObject p_removeObj)
    {
        Destroy(p_removeObj);
    }
    public void DestroyParticle(Transform p_position)
    {
        Instantiate(destroyParticlePrefab,p_position.position,Quaternion.identity);
    }

    public IEnumerator GoDownObject(Transform selectPosition,Transform p_targetPos, float p_downSpeed,float p_downTime)
    {
        Transform particlePosition = selectPosition;

        GameObject particle = Instantiate(downParticlePrefab,particlePosition.position,Quaternion.identity);

        Manager.instance.camera_Manager.OnShakeCameraPosition(2.8f, 0.1f);

        while (Vector3.Distance(selectPosition.position, p_targetPos.position) > 0.01f)
        {
            selectPosition.position = Vector3.MoveTowards(selectPosition.position, p_targetPos.position, p_downSpeed * Time.deltaTime);
            yield return null;
        }

        Destroy(particle);
        Destroy(transform.gameObject);
    }
}
