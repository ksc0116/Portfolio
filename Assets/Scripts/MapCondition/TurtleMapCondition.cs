using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleMapCondition : MonoBehaviour
{
    [SerializeField] GameObject[] destroyObj;
    [SerializeField] TurtleShellSpawner spawner;
    [SerializeField] GameObject inCamera;

    bool isFirst = false;

    private void Update()
    {
        if (isFirst == false)
        {
            if (Manager.instance.quest_Manager.dieTurtleShellCnt == spawner.spawnPoint.Length)
            {
                isFirst = true;
                StartCoroutine(MapClear());
                //Manager.instance.camera_Manager.ChangeCamera(inCamera);
                //for (int i = 0; i < destroyObj.Length; i++)
                //{
                //    Manager.instance.disable_Manager.DestroyParticle(destroyObj[i].transform);
                //    Manager.instance.disable_Manager.DestroyObject(destroyObj[i]);
                //}
                //Manager.instance.camera_Manager.OnMainCamera(inCamera);
            }
        }

    }
    IEnumerator MapClear()
    {
        StartCoroutine(Manager.instance.camera_Manager.MainCameraMove(inCamera.transform));
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < destroyObj.Length; i++)
        {
            Manager.instance.disable_Manager.DestroyParticle(destroyObj[i].transform);
            Manager.instance.disable_Manager.DestroyObject(destroyObj[i]);
        }
    }
}
