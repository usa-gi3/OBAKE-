using System.Collections.Generic;
using UnityEngine;

public class Visiter : MonoBehaviour
{
    private List<string> selectedStoryIds;
    private string firstStoryId;
    private string secondStoryId;

    void Start()
    {
        LoadMeiboAndChoice();
        CheckVisitors();
    }

    void LoadMeiboAndChoice()
    {
        // –¼•ë‚Í PlayerPrefs ‚©‚çŽæ“¾
        selectedStoryIds = new List<string>();
        for (int i = 0; i < 5; i++)
        {
            string id = PlayerPrefs.GetString($"SelectedStoryId_{i}", "");
            if (!string.IsNullOrEmpty(id))
                selectedStoryIds.Add(id);
        }

        // ˆê‰ñ–Ú‚Ì—ˆ–KŽÒ
        if (PlayerPrefs.HasKey("FirstStory"))
        {
            firstStoryId = PlayerPrefs.GetString("FirstStory");
        }
        else
        {
            Debug.LogWarning("Visiter: FirstStory ‚ª‘¶Ý‚µ‚Ü‚¹‚ñ");
            firstStoryId = "";
        }

        // “ñ‰ñ–Ú‚Ì—ˆ–KŽÒ
        if (PlayerPrefs.HasKey("SecondStory"))
        {
            secondStoryId = PlayerPrefs.GetString("SecondStory");
        }
        else
        {
            Debug.LogWarning("Visiter: SecondStory ‚ª‘¶Ý‚µ‚Ü‚¹‚ñ");
            secondStoryId = "";
        }
    }

    void CheckVisitors()
    {
        CheckVisitor(firstStoryId, "ˆê‰ñ–Ú‚Ì—ˆ–KŽÒ");
        CheckVisitor(secondStoryId, "“ñ‰ñ–Ú‚Ì—ˆ–KŽÒ");
    }

    void CheckVisitor(string storyId, string label)
    {
        if (string.IsNullOrEmpty(storyId))
            return;

        // –¼•ë‚É‚¢‚é‚©ƒ`ƒFƒbƒN
        string result = selectedStoryIds.Contains(storyId) ? "y‚ ‚èz" : "y‚È‚µz";

        // ƒLƒƒƒ‰ƒNƒ^[–¼
        string characterName = GetCharacterName(storyId);

        Debug.Log($"{label}: {characterName} ¨ {result}");
    }

    // NPCMEIBO ‚©‚ç GetCharacterName ‚ðƒRƒs[
    string GetCharacterName(string storyId)
    {
        switch (storyId)
        {
            case "story1": return "‘Šì";
            case "story2": return "Ž…ˆä";
            case "story3": return "‰LŽ”";
            case "story4": return "’¹";
            case "story5": return "•s–¾";
            case "story6": return "ŽOð";
            case "story7": return "•ˆä";
            case "story8": return "•é–ì";
            case "story9": return "“â";
            case "story10": return "‰¨“c";
            case "story11": return "‘‰³—";
            default: return "–¢’è‹`";
        }
    }
}
