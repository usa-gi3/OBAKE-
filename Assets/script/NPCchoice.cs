using System.Collections.Generic;
using UnityEngine;

public class NPCchoice : MonoBehaviour
{
    public InkController inkController;  // ← InkController を参照する
    public TextAsset inkJSONAsset;

    private string firstStory;
    private string secondStory;
    void Start()
    {
        if (firstStory != "")
        {
            // 前回途中で終わっていた → 1回目を再開
            inkController.StartKnot(inkJSONAsset, firstStory);
            return;
        }

        if (!PlayerPrefs.HasKey("FirstStoryPlayed"))
        {
            // 1回目
            string[] stories = { "story1", "story2", "story3","story4", "story5",
        "story6", "story7", "story8", "story9", "story10", "story11"};
            int firstIndex = Random.Range(0, stories.Length);
            firstStory = stories[firstIndex];

            PlayerPrefs.SetString("FirstStory", firstStory);
            PlayerPrefs.SetInt("FirstStoryPlayed", 1);
            PlayerPrefs.Save();

            inkController.StartKnot(inkJSONAsset, firstStory);           
        }

        
    }

    public void PlaySecondStory()
    {
        string[] stories =
        {
        "story1", "story2", "story3","story4", "story5",
        "story6", "story7", "story8", "story9", "story10", "story11"
        };

        // 1回目のストーリーを取得
        firstStory = PlayerPrefs.GetString("FirstStory");

        // 1回目と被らないストーリーを選ぶ
        List<string> remaining = new List<string>(stories);
        remaining.Remove(firstStory);
        secondStory = remaining[Random.Range(0, remaining.Count)];

        PlayerPrefs.SetString("SecondStory", secondStory);
        PlayerPrefs.Save();
        // 2回目のストーリーを再生
        inkController.StartKnot(inkJSONAsset, secondStory);
    }
}
