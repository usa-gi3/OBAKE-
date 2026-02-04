using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talkcontroller : MonoBehaviour
{
    public InkController inkController;
    public NPCchoice npcChoice;
    public TextAsset inkJSON;

    void Start()
    {
        string knot = npcChoice.GetStoryForThisVisit();
        inkController.StartKnot(inkJSON, knot);
    }
}
