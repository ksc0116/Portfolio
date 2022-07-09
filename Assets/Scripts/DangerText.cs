using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerText : MonoBehaviour
{
    Camera cam;
    private void Awake()
    {
        cam = Camera.main;
    }
    private void Update()
    {
        Quaternion q_hp = Quaternion.LookRotation(transform.position - cam.transform.position);
        Vector3 hp_angle = Quaternion.RotateTowards(transform.rotation, q_hp, 1000).eulerAngles;
        transform.rotation = Quaternion.Euler(hp_angle.x, hp_angle.y, 0);
    }
}
