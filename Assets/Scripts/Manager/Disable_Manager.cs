using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disable_Manager : MonoBehaviour
{
    [SerializeField] GameObject destroyParticlePrefab;
    [SerializeField] GameObject downParticlePrefab;
    Camera cam;
    private void Awake()
    {
        cam = Camera.main;
    }
    float spawnTime = 0.5f;
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
        //Manager.instance.camera_Manager.isEvent = true;
        Transform particlePosition = selectPosition;

        Manager.instance.sound_Manager.PlaySound(Manager.instance.sound_Manager.rockDownClip);


        StartCoroutine(CameraShake());

        //Manager.instance.camera_Manager.OnShakeCameraPosition(2.8f, 0.1f);

        float time = 0.5f;

        while (Vector3.Distance(selectPosition.position, p_targetPos.position) > 0.01f)
        {
            time+=Time.deltaTime;
            if (time > 0.5f)
            {
                Instantiate(downParticlePrefab, particlePosition.position, Quaternion.identity);
                time = 0;
            }
            selectPosition.position = Vector3.MoveTowards(selectPosition.position, p_targetPos.position, p_downSpeed * Time.deltaTime);
            yield return null;
        }
        Destroy(transform.gameObject);
        yield return new WaitForSeconds(1f);
        Manager.instance.camera_Manager.isEvent = false;
    }
    IEnumerator CameraShake()
    {
        Vector3 startPos = cam.transform.position;
        float shakeTime = 2.8f;
        while (shakeTime > 0.0f)
        {
            cam.transform.position = startPos + Random.insideUnitSphere * 0.1f;
            shakeTime -= Time.deltaTime;
            yield return null;
        }
        cam.transform.localPosition = startPos;
    }
}
