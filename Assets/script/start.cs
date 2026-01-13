using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class start : MonoBehaviour
{
    public FadeController fade;

    bool called = false;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("CallFadeOut", 20f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!called && Input.GetMouseButtonDown(0))
        {
            CallFadeOut();
        }
    }

    void CallFadeOut()
    {
        if (called) return;
        called = true;

        fade.FadeOut();
    }
}
