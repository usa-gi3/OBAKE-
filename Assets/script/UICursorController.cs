using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICursorController : MonoBehaviour
{
    private void OnEnable()
    {
        // UIï\é¶Ç≥ÇÍÇΩèuä‘
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnDisable()
    {
        // UIè¡Ç¶ÇΩèuä‘
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
