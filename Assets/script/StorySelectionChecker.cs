using UnityEngine;

public class StorySelectionDebugLogger : MonoBehaviour
{
    public NPCMEIBO npcMeibo;
    public NPCchoice npcChoice;

    /// <summary>
    /// 1回目（FirstStory）の確認とログ出力
    /// </summary>
    public void LogFirstStoryResult()
    {
        string firstStory = PlayerPrefs.GetString("FirstStory", "");

        if (string.IsNullOrEmpty(firstStory))
        {
            Debug.LogWarning("FirstStory が取得できません");
            return;
        }

        bool isInFive = npcMeibo.selectedStoryIds.Contains(firstStory);

        Debug.Log(
            $"[1回目]\n" +
            $"選ばれたstoryId : {firstStory}\n" +
            $"最初の5人に含まれているか : {isInFive}"
        );
    }

    /// <summary>
    /// 2回目（SecondStory）の確認とログ出力
    /// </summary>
    public void LogSecondStoryResult(string secondStory)
    {
        if (string.IsNullOrEmpty(secondStory))
        {
            Debug.LogWarning("SecondStory が空です");
            return;
        }

        bool isInFive = npcMeibo.selectedStoryIds.Contains(secondStory);

        Debug.Log(
            $"[2回目]\n" +
            $"選ばれたstoryId : {secondStory}\n" +
            $"最初の5人に含まれているか : {isInFive}"
        );
    }
}
