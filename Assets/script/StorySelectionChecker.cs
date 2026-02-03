using UnityEngine;
using System.Collections.Generic; 

public class StorySelectionChecker : MonoBehaviour
{
    /// <summary>
    /// 現在選ばれている storyId が
    /// 名簿（ランダム5人）に含まれているか判定
    /// </summary>
    public bool IsStoryInMeibo(string storyId)
    {
        if (string.IsNullOrEmpty(storyId))
        {
            Debug.LogWarning("StorySelectionChecker: storyId が空です");
            return false;
        }

        List<string> selectedStoryIds = new List<string>();

        for (int i = 0; i < 5; i++)
        {
            string id = PlayerPrefs.GetString($"SelectedStoryId_{i}", "");
            if (!string.IsNullOrEmpty(id))
            {
                selectedStoryIds.Add(id);
            }
        }

        bool result = selectedStoryIds.Contains(storyId);

        Debug.Log(
            $"判定: {storyId} は名簿に載っているか → {result}"
        );

        return result;
    }
}
