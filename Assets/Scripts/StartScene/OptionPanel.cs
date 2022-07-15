using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionPanel : MonoBehaviour
{
    [SerializeField] GameObject mainMenuPanel;
    public void ExitButton()
    {
        mainMenuPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
