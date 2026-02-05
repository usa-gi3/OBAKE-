using UnityEngine;
using System.Collections.Generic;

public class ChoiceServer : MonoBehaviour
{
    private int score = 0;   // 正解数のみ管理

    // 既に判定済みの storyId
    private readonly HashSet<string> registered = new HashSet<string>();

    // 名簿（PlayerPrefs から復元）
    private HashSet<string> roster = new HashSet<string>();

    void Start()
    {
        LoadRosterFromPrefs();
        Debug.Log("[ChoiceServer] 名簿取得完了");
    }

    void LoadRosterFromPrefs()
    {
        roster.Clear();

        // NPCMEIBO が保存しているキー
        for (int i = 0; i < 5; i++)
        {
            string key = $"SelectedStoryId_{i}";
            if (PlayerPrefs.HasKey(key))
            {
                roster.Add(PlayerPrefs.GetString(key));
            }
        }

        Debug.Log($"[ChoiceServer] 名簿人数: {roster.Count}");
    }

    /// <summary>
    /// choiceIndex
    /// 0 = 受け取る / 返す
    /// 1 = 受け取らない / 返さない
    /// </summary>
    public void RegisterChoice(string storyId, int choiceIndex)
    {
        if (registered.Contains(storyId)) return;
        registered.Add(storyId);

        bool inRoster = roster.Contains(storyId);
        bool acted = (choiceIndex == 0); // 何かしたかどうか

        bool isCorrect;

        if (inRoster)
        {
            // 名簿にいる → 無視したら不正解
            isCorrect = acted;
        }
        else
        {
            // 名簿にいない → 触ったら不正解
            isCorrect = !acted;
        }

        if (isCorrect)
        {
            score++;
            PlayerPrefs.SetInt("GOOD_SCORE", score);
            PlayerPrefs.Save();
            Debug.Log($"[ChoiceServer] {storyId} → 正 SCORE={score}");
        }
        else
        {
            Debug.Log($"[ChoiceServer] {storyId} → 不");
        }
    }

    public int GetScore() => score;
}
