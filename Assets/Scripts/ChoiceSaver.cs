using System.Collections.Generic;
using UnityEngine;

public class ChoiceServer : MonoBehaviour
{
    // 判定対象の名簿 storyId
    private readonly HashSet<string> roster = new HashSet<string>
    {
        "story1","story2","story3","story4","story5",
        "story6","story7","story8","story9","story10","story11"
    };

    private int good = 0;
    private int bad = 0;

    void Start()
    {
        Debug.Log("[ChoiceServer] Start");
        // PlayerPrefs から過去のスコアをロード
        good = PlayerPrefs.GetInt("GOOD_SCORE", 0);
        bad = PlayerPrefs.GetInt("BAD_SCORE", 0);
    }

    /// <summary>
    /// 選択肢が決まった時に呼ぶ
    /// </summary>
    public void RegisterChoice(string storyId, int choiceIndex)
    {
        Debug.Log($"[ChoiceServer] RegisterChoice called: storyId={storyId}, choiceIndex={choiceIndex}");

        // roster にある storyId が 0 を選んだら Good、それ以外は Bad
        if ((roster.Contains(storyId) && choiceIndex == 0) || (!roster.Contains(storyId) && choiceIndex != 0))
        {
            good++;
            Debug.Log($"[ChoiceServer] {storyId} → Good! 現在の Good: {good}");
        }
        else
        {
            bad++;
            Debug.Log($"[ChoiceServer] {storyId} → Bad! 現在の Bad: {bad}");
        }

        // 保存
        PlayerPrefs.SetInt("GOOD_SCORE", good);
        PlayerPrefs.SetInt("BAD_SCORE", bad);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// デバッグ用に現在スコア表示
    /// </summary>
    [ContextMenu("Print Score")]
    public void PrintScore()
    {
        Debug.Log($"[ChoiceServer] Good={good}, Bad={bad}");
    }
}
