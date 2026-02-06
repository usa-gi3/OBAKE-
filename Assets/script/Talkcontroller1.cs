using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Talkcontroller1 : MonoBehaviour
{
    public InkController inkController;
    public Endchoice endchoice;
    public TextAsset inkJSON;
    public FadeController fadeController;

    void Start()
    {
        inkController.onStoryFinished += OnStoryFinished;

        int score = endchoice.GetResultScore();  
        string knot = endchoice.GetEndKnot(score);

        inkController.StartKnot(inkJSON, knot);
    }

    void OnStoryFinished()
    {
        Debug.Log("会話が完全に終了した");

        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        StartCoroutine(FadeAndLoadScene("start Scene"));
    }

    private IEnumerator FadeAndLoadScene(string sceneName)
    {
        // フェードアウト
        yield return fadeController.FadeCoroutine(0f, 1f);

        // PlayerPrefs をクリア
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        // シーン切替
        SceneManager.LoadScene("start Scene");
    }

    void OnDestroy()
    {
        if (inkController != null)
            inkController.onStoryFinished -= OnStoryFinished;
    }
}
