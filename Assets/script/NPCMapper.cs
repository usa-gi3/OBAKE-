using System.Collections.Generic;
using UnityEngine;

public class NPCMapperWatcher : MonoBehaviour
{
    // ストーリーIDとNPC名の対応表
    private Dictionary<string, string> storyToNPC = new Dictionary<string, string>()
    {
        {"story1", "相川"},
        {"story2", "糸井"},
        {"story3", "鵜飼"},
        {"story4", "鳥"},
        {"story5", "不明"},
        {"story6", "三条"},
        {"story7", "黒井"},
        {"story8", "暮野"},
        {"story9", "凪"},
        {"story10", "鴎田"},
        {"story11", "早乙女"}
    };

    private string lastFirstStory = "";  // 前回の FirstStory 値
    private string lastSecondStory = ""; // 前回の SecondStory 値

    void Update()
    {
        WatchAndSave("FirstStory", "FirstNPC", ref lastFirstStory);
        WatchAndSave("SecondStory", "SecondNPC", ref lastSecondStory);
    }

    // ストーリーIDを監視して、NPC名とセットで保存
    private void WatchAndSave(string storyKey, string npcKey, ref string lastStory)
    {
        string currentStory = PlayerPrefs.GetString(storyKey, "");

        // 空でなく、前回と違う場合に保存
        if (!string.IsNullOrEmpty(currentStory) && currentStory != lastStory)
        {
            if (storyToNPC.TryGetValue(currentStory, out string npcName))
            {
                // ストーリーIDとNPC名をセットで保存
                PlayerPrefs.SetString(storyKey, currentStory);
                PlayerPrefs.SetString(npcKey, npcName);
                PlayerPrefs.Save();
                Debug.Log($"{storyKey} に {currentStory}, {npcKey} に {npcName} を保存しました");
                lastStory = currentStory;
            }
            else
            {
                Debug.LogWarning($"{currentStory} に対応する NPC が存在しません");
            }
        }
    }

    // 外部から NPC 名とストーリーIDを取得可能
    public string GetFirstStoryID() => PlayerPrefs.GetString("FirstStory", "");
    public string GetSecondStoryID() => PlayerPrefs.GetString("SecondStory", "");
    public string GetFirstNPC() => PlayerPrefs.GetString("FirstNPC", "");
    public string GetSecondNPC() => PlayerPrefs.GetString("SecondNPC", "");
}
