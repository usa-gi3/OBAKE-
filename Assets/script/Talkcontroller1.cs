using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Talkcontroller1 : MonoBehaviour
{
    public InkController inkController;
    public Endchoice endchoice;
    public TextAsset inkJSON;

    void Start()
    {
        inkController.onStoryFinished += OnStoryFinished;

        int score = endchoice.GetResultScore();  
        string knot = endchoice.GetEndKnot(score);

        inkController.StartKnot(inkJSON, knot);
    }

    void OnStoryFinished()
    {
        Debug.Log("âÔòbÇ™äÆëSÇ…èIóπÇµÇΩ");

        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();


    }

    void OnDestroy()
    {
        if (inkController != null)
            inkController.onStoryFinished -= OnStoryFinished;
    }
}
