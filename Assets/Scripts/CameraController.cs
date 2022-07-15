using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform cameraArm;

    float zoomSpeed = 500f;
    float zoomMin = 1.5f;
    float zoomMax=15f;
    float firstDistance;
    float zoomDistance;

    private void Awake()
    {
        zoomDistance = Vector3.Distance(transform.position, cameraArm.position);
        firstDistance = zoomDistance;
        zoomMax = zoomDistance;
    }
    private void Update()
    {
        if (Manager.instance.camera_Manager.isBossScene == true)
        {
            zoomMax = 20f;
        }
        else
        {
            zoomMax = firstDistance;
        }
        if (Manager.instance.camera_Manager.isEvent == false)
        {
            zoomDistance -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime;
            zoomDistance = Mathf.Clamp(zoomDistance, zoomMin, zoomMax);

            transform.position = transform.rotation * new Vector3(0, 0, -zoomDistance) + cameraArm.position;
        }
    }
}
