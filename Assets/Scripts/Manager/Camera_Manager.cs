using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Manager : MonoBehaviour
{
    public Vector3 originCamPosition;
    public GameObject playerObject;
    public GameObject talkTextObject;
    float moveTime = 6f;

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
