using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;

public class NPCMEIBO : MonoBehaviour
{
    public string currentStoryId;

    public List<string> selectedStoryIds = new List<string>();

    private readonly string[] allStoryIds =
    {
        "story1", "story2", "story3",
        "story6", "story7", "story8", "story9", "story10", "story11"
    };

    void Start()
    {
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

    public void RecordCharacter()
    {
        if (string.IsNullOrEmpty(currentStoryId))
        {
            Debug.LogWarning("NPCMEIBO: currentStoryId ‚ª‹ó‚Å‚·");
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
                Debug.LogWarning($"NPCMEIBO: –¢“o˜^‚Ì storyId -> {currentStoryId}");
                return;
        }

        PlayerPrefs.Save();
    }

    string GetCharacterName(string storyId)
    {
        switch (storyId)
        {
            case "story1": return "‘Šì";
            case "story2": return "…ˆä";
            case "story3": return "‰L”";
            case "story4": return "’¹";
            case "story5": return "•s–¾";
            case "story6": return "Oğ";
            case "story7": return "•ˆä";
            case "story8": return "•é–ì";
            case "story9": return "“â";
            case "story10": return "‰¨“c";
            case "story11": return "‘‰³—";
            default: return "–¢’è‹`";
        }
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
            selectedStoryIds.Add(
                PlayerPrefs.GetString($"SelectedStoryId_{i}")
            );
        }
    }

    public void PrintSelectedCharacterNames()
    {
        Debug.Log("=== –¼•ë ===");

        foreach (string storyId in selectedStoryIds)
        {
            if (string.IsNullOrEmpty(storyId)) continue;

            Debug.Log(GetCharacterName(storyId));
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
