using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Endchoice : MonoBehaviour
{
   /* public InkController inkController;  // ← InkController を参照する
    public TextAsset inkJSONAsset;

    string endKnot;
    public int resultScore;

    void Start()
    {
        string endKnot = GetEndKnot();

        inkController.onStoryCompletelyFinished += OnEndStoryFinished;
        inkController.StartKnot(inkJSONAsset, endKnot);
    }

    string GetEndKnot()
    {
        if (resultScore <= 0) return "End5";
        if (resultScore == 1) return "End4";
        if (resultScore == 2) return "End2";
        return "End3";
    }

    void OnEndStoryFinished()
    {
        SceneManager.LoadScene("NextSceneName"); // ← 行きたいシーン
    }

    void OnDestroy()
    {
        inkController.onStoryCompletelyFinished -= OnEndStoryFinished;
    }*/
}
