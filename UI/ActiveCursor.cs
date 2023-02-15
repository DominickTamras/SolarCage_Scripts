using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCursor : MonoBehaviour
{

    public SaveManager bruhTest;
   
    void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
