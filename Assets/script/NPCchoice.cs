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
        // --- ストーリー一覧 ---
        string[] stories =
        {
            "story1", "story2", "story3","story4", "story5",
            "story6", "story7", "story8", "story9", "story10", "story11"
        };

        // --- 1回目 ---
        int firstIndex = Random.Range(0, stories.Length);
        firstStory = stories[firstIndex];

        // --- 2回目（重複なし） ---
        List<string> remaining = new List<string>(stories);
        remaining.Remove(firstStory);
        secondStory = remaining[Random.Range(0, remaining.Count)];

        // 1回目を InkController に依頼
        inkController.StartKnot(inkJSONAsset, firstStory);
    }

    // 1話目が終わったら InkController が呼ぶ
    public void PlaySecondStory()
    {
        inkController.StartKnot(inkJSONAsset, secondStory);
    }
}
