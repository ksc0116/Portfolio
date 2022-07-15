using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Camera_Manager : MonoBehaviour
{
    [Header("BossSceneCamera")]
    [SerializeField] Vector3 forwardPosition;
    [SerializeField] Vector3 forwardRotation;
    public bool isBossScene = false;

    [Header("BossScene")]
    [SerializeField] GameObject boss;
    [SerializeField] GameObject dustPrefab;
    [SerializeField] HightLightEffect highLightEffect;
    [SerializeField] GameObject dangerText;
    [SerializeField] GameObject earthQuakePrefab;
    [SerializeField] Animator bossAnim;
    [SerializeField] GameObject bossHpBar;
    public bool isBossDrop = false;
    public bool isTrigger = false;
    public bool isBossMove=false;
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

    public bool isEvent = false;
    private void Awake()
    {
        cam = Camera.main;
        camOriginParent = cam.transform.parent;
        camOriginPosition=cam.transform.localPosition;
        camOriginRotation=cam.transform.localRotation;
    }

    public IEnumerator BossInCamera(GameObject inCamera, Transform p_BossTransform, Vector3 p_BossAfterPos)
    {
        isEvent = true;

        inCamera.SetActive(true);

        Vector3 startRotation = inCamera.transform.eulerAngles;

        float power = 10f;

        Manager.instance.sound_Manager.PlaySound(Manager.instance.sound_Manager.bossDownClip);

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
        Manager.instance.sound_Manager.sfxAudioSource.Stop();
        Manager.instance.sound_Manager.PlaySound(Manager.instance.sound_Manager.bossSkillClip);
        isArrive = true;
        Instantiate(earthQuakePrefab, p_BossTransform.position, Quaternion.identity);
        Manager.instance.camera_Manager.OnShakeCameraPosition(1.3f, 1f);
        GameObject dust = Instantiate(dustPrefab,p_BossTransform.position,Quaternion.identity);
        yield return new WaitForSeconds(1.3f);
        bossAnim.SetTrigger("onLand");

        yield return new WaitForSeconds(5f);

        StartCoroutine( highLightEffect.EffectOff() );
        bossHpBar.SetActive(true);
        Manager.instance.playerStat_Manager.isMoveAble = true;
        Manager.instance.playerStat_Manager.player.GetComponent<NavMeshAgent>().speed = 3.5f;
        isBossMove = true;

        isEvent = false;
    }
    IEnumerator LookBoss()
    {
        isEvent = true;
        while (isArrive == false)
        {
            Quaternion q_hp = Quaternion.LookRotation(boss.transform.position  - cam.transform.position);
            Vector3 hp_Angle = Quaternion.RotateTowards(cam.transform.rotation, q_hp, 1000).eulerAngles;
            cam.transform.rotation = Quaternion.Euler(hp_Angle.x, hp_Angle.y, 0);

            yield return null;
        }
        yield return new WaitForSeconds(5f);
        cam.transform.localPosition = camOriginPosition;
        cam.transform.localRotation = camOriginRotation;
        isEvent = false;
    }

    public IEnumerator MainCameraMoveInBoss()
    {
        isEvent = true;

        yield return new WaitForSeconds(0.3f);
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
        yield return new WaitForSeconds(2f);
        Manager.instance.sound_Manager.PlaySound(Manager.instance.sound_Manager.dangerClip);
        dangerText.SetActive(true);
        yield return new WaitForSeconds(2f);

        Vector3 startPos = cam.transform.position;
        shakeTime = 2f;
        shakeIntensity = 0.1f;
        Manager.instance.sound_Manager.sfxAudioSource.Stop();
        Manager.instance.sound_Manager.PlaySound(Manager.instance.sound_Manager.rockDownClip);
        while (shakeTime > 0.0f)
        {
            cam.transform.position = startPos + Random.insideUnitSphere * shakeIntensity;
            shakeTime -= Time.deltaTime;
            yield return null;
        }
        Manager.instance.sound_Manager.sfxAudioSource.Stop();
        isBossDrop = true;

        dangerText.SetActive(false);


        cam.transform.localPosition = camOriginPosition;
        cam.transform.localRotation = camOriginRotation;
        isEvent = false;
    }

    // ================================================================================================
    public IEnumerator MainCameraMove(Transform p_targetPosition)
    {
        isEvent = true;

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
        isEvent = false;
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
        isEvent = true;

        Vector3 startPos=cam.transform.position;
        while (shakeTime > 0.0f)
        {
            cam.transform.position = startPos + Random.insideUnitSphere * shakeIntensity;
            shakeTime-=Time.deltaTime;
            yield return null;
        }
        cam.transform.localPosition = camOriginPosition;
        isEvent = false;
    }
}
