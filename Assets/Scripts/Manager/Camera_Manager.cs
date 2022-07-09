using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Manager : MonoBehaviour
{
    [Header("BossSceneCamera")]
    [SerializeField] Vector3 forwardPosition;
    [SerializeField] Vector3 forwardRotation;

    [Header("BossScene")]
    [SerializeField] GameObject boss;
    [SerializeField] GameObject dustPrefab;
    [SerializeField] HightLightEffect highLightEffect;
    [SerializeField] GameObject dangerText;
    public bool isBossDrop = false;
    public bool isTrigger = false;
    bool isFirst = false;
    bool isArrive = false;

    public Vector3 originCamPosition;
    public Vector3 originCamRotation;
    public GameObject playerObject;
    public GameObject talkTextObject;
    float moveTime = 4f;

    Camera cam;
    Transform camOriginParent;
    Vector3 camOriginPosition;
    Quaternion camOriginRotation;

    float shakeTime;
    float shakeIntensity;
    private void Awake()
    {
        cam = Camera.main;
        camOriginParent = cam.transform.parent;
        camOriginPosition=cam.transform.localPosition;
        camOriginRotation=cam.transform.localRotation;
    }

    public IEnumerator BossInCamera(GameObject inCamera, Transform p_BossTransform, Vector3 p_BossAfterPos)
    {
        inCamera.SetActive(true);

        Vector3 startRotation = inCamera.transform.eulerAngles;

        float power = 10f;

        while (Vector3.Distance(p_BossTransform.position, p_BossAfterPos) > 0.1f)
        {
            if (isTrigger == false)
            {
                float x = Random.Range(-1f, 1f);
                float y = Random.Range(-1f, 1f);
                float z = Random.Range(-1f, 1f);
                inCamera.transform.rotation = Quaternion.Euler(startRotation + new Vector3(x, y, z) * 0.1f * power);
            }
            else if(isTrigger == true)
            {
                if (isFirst == false)
                {
                    isFirst = true;
                    inCamera.SetActive(false);
                    StartCoroutine(LookBoss());
                }
            }
            
            p_BossTransform.Translate((p_BossAfterPos - p_BossTransform.position).normalized * 150f * Time.deltaTime);

            yield return null;
        }
        isArrive = true;
        Manager.instance.camera_Manager.OnShakeCameraPosition(2f, 1f);
        GameObject dust = Instantiate(dustPrefab,p_BossTransform.position,Quaternion.identity);
        yield return new WaitForSeconds(2f);

        StartCoroutine( highLightEffect.EffectOff() );

        yield return new WaitForSeconds(0.5f);
        Destroy(dust);
    }
    IEnumerator LookBoss()
    {
        while (isArrive == false)
        {
            Quaternion q_hp = Quaternion.LookRotation(boss.transform.position  - cam.transform.position);
            Vector3 hp_Angle = Quaternion.RotateTowards(cam.transform.rotation, q_hp, 1000).eulerAngles;
            cam.transform.rotation = Quaternion.Euler(hp_Angle.x, hp_Angle.y, 0);

            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
        cam.transform.localPosition = camOriginPosition;
        cam.transform.localRotation = camOriginRotation;
    }

    public IEnumerator MainCameraMoveInBoss()
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        yield return new WaitForSeconds(1.2f);
        while (percent < 1f)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / moveTime;

            cam.transform.position = Vector3.Lerp(cam.transform.position, camOriginParent.position + forwardPosition, 0.1f);
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, Quaternion.Euler(forwardRotation), 0.1f);

            yield return null;
        }
        dangerText.SetActive(true);
        yield return new WaitForSeconds(2f);

        Vector3 startPos = cam.transform.position;
        shakeTime = 2f;
        shakeIntensity = 0.1f;
        while (shakeTime > 0.0f)
        {
            cam.transform.position = startPos + Random.insideUnitSphere * shakeIntensity;
            shakeTime -= Time.deltaTime;
            yield return null;
        }
        isBossDrop = true;

        dangerText.SetActive(false);

        cam.transform.localPosition = camOriginPosition;
        cam.transform.localRotation = camOriginRotation;
    }

    // ================================================================================================
    public IEnumerator MainCameraMove(Transform p_targetPosition)
    {
        cam.transform.SetParent(p_targetPosition.parent);

        playerObject.SetActive(false);

        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1f)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / moveTime;

            cam.transform.position=Vector3.Lerp(cam.transform.position, p_targetPosition.position, percent);
            cam.transform.rotation=Quaternion.Lerp(cam.transform.rotation,p_targetPosition.rotation, percent);

            yield return null;
        }

        playerObject.SetActive(true);

        cam.transform.SetParent(camOriginParent);
        cam.transform.localPosition=camOriginPosition;
        cam.transform.localRotation=camOriginRotation;
    }

    public void ChangeCamera(GameObject p_onObject)
    {
        cam.enabled = false;
        p_onObject.SetActive(true);
        playerObject.SetActive(false);
        talkTextObject.SetActive(false);
    }

    public void OnMainCamera(GameObject p_onObject)
    {
        cam.enabled = true;
        p_onObject.SetActive(false);
        playerObject.SetActive(true);
    }

    public void OnShakeCameraPosition(float p_shakeTime, float p_shakeIntensity)
    {
        shakeTime = p_shakeTime;
        shakeIntensity = p_shakeIntensity;

        StopCoroutine("ShakeByPosition");
        StartCoroutine("ShakeByPosition");
    }

    IEnumerator ShakeByPosition()
    {
        Vector3 startPos=cam.transform.position;
        while (shakeTime > 0.0f)
        {
            cam.transform.position = startPos + Random.insideUnitSphere * shakeIntensity;
            shakeTime-=Time.deltaTime;
            yield return null;
        }

        cam.transform.localPosition = camOriginPosition;
    }
}
