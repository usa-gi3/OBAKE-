using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dieplay : MonoBehaviour
{
    public FadeController fade;

    bool called = false;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("CallFadeIn", 20f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!called && Input.GetMouseButtonDown(0))
        {
            CallFadeIn();
        }
    }

    void CallFadeIn()
    {
        if (called) return;
        called = true;

        fade.FadeIn();
    }
}
