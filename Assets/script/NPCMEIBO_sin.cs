using System.Collections.Generic;
using UnityEngine;

public class NPCMEIBO : MonoBehaviour
{
    // ★ 追加：Singleton
    public static NPCMEIBO Instance { get; private set; }

    public string currentStoryId;
    public List<string> selectedStoryIds = new List<string>();

    // 名簿に載る可能性があるIDのみ
    private readonly string[] allStoryIds =
    {
        "story1", "story2", "story3",
        "story6", "story7", "story8", "story9", "story10", "story11"
    };

    void Awake()
    {
        // ★ 追加：多重生成防止
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // このプレイ用の名簿が無ければ作る
        if (!PlayerPrefs.HasKey("StoryIdsInitialized"))
        {
            selectedStoryIds = GetRandomFiveStoryIds();
            SaveSelectedStoryIds();
        }
        else
        {
            LoadSelectedStoryIds();
        }

        PrintSelectedCharacterNames();
    }

    // 出現記録（名簿にいるかどうかは別問題）
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
            case "story4": RecordBird(); break;      // 名簿外
            case "story5": RecordUnknown(); break;   // 名簿外
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

    void SaveSelectedStoryIds()
    {
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.DeleteKey($"SelectedStoryId_{i}");
        }

        for (int i = 0; i < selectedStoryIds.Count; i++)
        {
            PlayerPrefs.SetString($"SelectedStoryId_{i}", selectedStoryIds[i]);
        }

        PlayerPrefs.SetInt("StoryIdsInitialized", 1);
        PlayerPrefs.Save();
    }

    void LoadSelectedStoryIds()
    {
        selectedStoryIds.Clear();

        for (int i = 0; i < 5; i++)
        {
            string id = PlayerPrefs.GetString($"SelectedStoryId_{i}", "");
            if (!string.IsNullOrEmpty(id))
                selectedStoryIds.Add(id);
        }
    }

    public void PrintSelectedCharacterNames()
    {
        Debug.Log("=== 名簿 ===");
        foreach (string storyId in selectedStoryIds)
        {
            Debug.Log(GetCharacterName(storyId));
        }
    }

    public string GetCharacterName(string storyId)
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
