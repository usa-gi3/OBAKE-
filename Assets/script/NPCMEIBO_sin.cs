using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;

public class NPCMEIBO : MonoBehaviour
{
    // 現在再生中の story
    public string currentStoryId;

    // 選ばれた5人の storyId
    public List<string> selectedStoryIds = new List<string>();

    // storyId 一覧
    private readonly string[] allStoryIds =
    {
        "story1", "story2", "story3",
        "story6", "story7", "story8", "story9", "story10", "story11"
    };

    void Start()
    {
        // ゲーム開始時に1回だけランダム5人を決定
        if (!PlayerPrefs.HasKey("StoryIdsInitialized"))
        {
            selectedStoryIds = GetRandomFiveStoryIds();
            SaveSelectedStoryIds();
        }
        else
        {
            LoadSelectedStoryIds();
        }

        DebugRandomFiveCharacters();
    }

    //いったん記録
    public void RecordCharacter()
    {
        if (string.IsNullOrEmpty(currentStoryId))
        {
            Debug.LogWarning("NPCMEIBO: currentStoryId が空です");
            return;
        }

        switch (currentStoryId.Trim())
        {
            case "story1": RecordAikawa(); break;
            case "story2": RecordItoi(); break;
            case "story3": RecordUkai(); break;
            case "story4": RecordBird(); break;
            case "story5": RecordUnknown(); break;
            case "story6": RecordSanjo(); break;
            case "story7": RecordKuroi(); break;
            case "story8": RecordKurono(); break;
            case "story9": RecordNagi(); break;
            case "story10": RecordKamota(); break;
            case "story11": RecordSaotome(); break;
            default:
                Debug.LogWarning($"NPCMEIBO: 未登録の storyId -> {currentStoryId}");
                return;
        }

        PlayerPrefs.Save();
    }

    //storyId → 名前変換
    string GetCharacterName(string storyId)
    {
        switch (storyId)
        {
            case "story1": return "相川";
            case "story2": return "糸井";
            case "story3": return "鵜飼";
            case "story4": return "鳥";
            case "story5": return "不明";
            case "story6": return "三条";
            case "story7": return "黒井";
            case "story8": return "暮野";
            case "story9": return "凪";
            case "story10": return "鴎田";
            case "story11": return "早乙女";
            default: return "未定義";
        }
    }

    //ランダム5人選出
    List<string> GetRandomFiveStoryIds()
    {
        List<string> pool = new List<string>(allStoryIds);
        List<string> result = new List<string>();

        for (int i = 0; i < 5; i++)
        {
            int index = Random.Range(0, pool.Count);
            result.Add(pool[index]);
            pool.RemoveAt(index);
        }

        return result;
    }

    // 選ばれた storyId を保存
    void SaveSelectedStoryIds()
    {
        for (int i = 0; i < selectedStoryIds.Count; i++)
        {
            PlayerPrefs.SetString($"SelectedStoryId_{i}", selectedStoryIds[i]);
        }

        PlayerPrefs.SetInt("StoryIdsInitialized", 1);
        PlayerPrefs.Save();
    }

    // 保存された storyId を復元
    void LoadSelectedStoryIds()
    {
        selectedStoryIds.Clear();

        for (int i = 0; i < 5; i++)
        {
            selectedStoryIds.Add(
                PlayerPrefs.GetString($"SelectedStoryId_{i}")
            );
        }
    }

    // デバッグ表示
    public void DebugRandomFiveCharacters()
    {
        Debug.Log("===== デバッグ名簿 =====");

        for (int i = 0; i < selectedStoryIds.Count; i++)
        {
            string id = selectedStoryIds[i];
            string name = GetCharacterName(id);
            Debug.Log($" {name}");
        }

        Debug.Log("===================================");
    }

    //個別記録
    void RecordAikawa() => PlayerPrefs.SetInt("NPC_Aikawa", 1);
    void RecordItoi() => PlayerPrefs.SetInt("NPC_Itoi", 1);
    void RecordUkai() => PlayerPrefs.SetInt("NPC_Ukai", 1);
    void RecordBird() => PlayerPrefs.SetInt("NPC_Bird", 1);
    void RecordUnknown() => PlayerPrefs.SetInt("NPC_Unknown", 1);
    void RecordSanjo() => PlayerPrefs.SetInt("NPC_Sanjo", 1);
    void RecordKuroi() => PlayerPrefs.SetInt("NPC_Kuroi", 1);
    void RecordKurono() => PlayerPrefs.SetInt("NPC_Kurono", 1);
    void RecordNagi() => PlayerPrefs.SetInt("NPC_Nagi", 1);
    void RecordKamota() => PlayerPrefs.SetInt("NPC_Kamota", 1);
    void RecordSaotome() => PlayerPrefs.SetInt("NPC_Saotome", 1);
}
