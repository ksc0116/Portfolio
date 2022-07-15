using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleMapCondition : MonoBehaviour
{
    [SerializeField] GameObject[] beetles;
    [SerializeField] GameObject inCamera;
    [SerializeField] GameObject destroyObj;
    [SerializeField] Transform downPosition;

    float downSpeed =0.6f;
    bool isFirst = false;

    private void Update()
    {
        if (Manager.instance.quest_Manager.isSecEvent == false)
        {
            if (isFirst == false)
            {
                if (beetles.Length == Manager.instance.quest_Manager.dieBeetleCnt)
                {
                    Manager.instance.quest_Manager.isSecEvent = true;
                    isFirst = true;
                    StartCoroutine(MapClear());
                }
            }
        }
    }
    IEnumerator MapClear()
    {
        StartCoroutine( Manager.instance.camera_Manager.MainCameraMove(inCamera.transform));
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine( Manager.instance.disable_Manager.GoDownObject(destroyObj.transform, downPosition, downSpeed,2f) );
    }
}
