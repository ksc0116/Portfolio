using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePoint : MonoBehaviour
{
    [SerializeField] Texture2D mouse;  
    private void Start()
    {
        Cursor.SetCursor(mouse,Vector2.zero ,CursorMode.Auto);
    }
}
