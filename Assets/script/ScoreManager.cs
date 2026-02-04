using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public NPCMapperWatcher mapper;
    public NPCMEIBO meibo;

    // スコア
    public int score;

    const string SCORE_KEY = "TotalScore";

    void Start()
    {
        // シーンに入ったときに既存スコアを読み込む
        score = PlayerPrefs.GetInt(SCORE_KEY, 0);
    }

    public void OnChoiceSelected(int choiceIndex)
    {
        // 0番以外は「受け取らない系」なので即終了
        if (choiceIndex != 0)
        {
            Debug.Log("拒否選択のため加点なし");
            return;
        }

        // 出現したストーリーID（Second優先）
        string storyId = mapper.GetSecondStoryID();
        if (string.IsNullOrEmpty(storyId))
        {
            storyId = mapper.GetFirstStoryID();
        }

        if (string.IsNullOrEmpty(storyId))
        {
            Debug.LogWarning("StoryID が取得できません");
            return;
        }

        // 名簿に存在するか（←ここはそのまま）
        bool isInMeibo = meibo.selectedStoryIds.Contains(storyId);

        if (isInMeibo)
        {
            score++;
            PlayerPrefs.SetInt(SCORE_KEY, score);
            PlayerPrefs.Save();

            Debug.Log($"加点 +1！（{storyId}） 現在スコア: {score}");
        }
        else
        {
            Debug.Log("名簿に存在しないため加点なし");
        }
    }
}
