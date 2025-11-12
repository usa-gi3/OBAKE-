using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public InkController inkController;
    // Start is called before the first frame update
    void Start()
    {
        inkController.StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
