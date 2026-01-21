using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class NPCMEIBO : MonoBehaviour
{
    public TextAsset inkJSONAsset;

    private List<string> usedCharacterNames = new List<string>();

    private string[] stories =
    {
        "story1", "story2", "story3",
        "story6", "story7", "story8", "story9", "story10", "story11"
    };

    // 名保持用
    public string firstname;
    public string secondname;
    public string thirdname;
    public string fourthname;
    public string fifthname;

    void Start()
    {
        int safety = 0;

        // 5人分の名前を取得
        while (usedCharacterNames.Count < 5 && safety < 50)
        {
            string name = GetCharacterName();

            if (!usedCharacterNames.Contains(name))
            {
                usedCharacterNames.Add(name);
            }

            safety++;
        }

        ShuffleList(usedCharacterNames);

       
        while (usedCharacterNames.Count < 5)
        {
            usedCharacterNames.Add("Unknown");
        }

        firstname = usedCharacterNames[0];
        secondname = usedCharacterNames[1];
        thirdname = usedCharacterNames[2];
        fourthname = usedCharacterNames[3];
        fifthname = usedCharacterNames[4];

        Debug.Log("名簿");
        Debug.Log($"1人目：{firstname}");
        Debug.Log($"2人目：{secondname}");
        Debug.Log($"3人目：{thirdname}");
        Debug.Log($"4人目：{fourthname}");
        Debug.Log($"5人目：{fifthname}");
    }

    private string GetCharacterName()
    {
        int safety = 0;

        while (safety < 20)
        {
            string randomStory = stories[Random.Range(0, stories.Length)];
            Story tempStory = new Story(inkJSONAsset.text);
            tempStory.ChoosePathString(randomStory);

            string name = ExtractCharacterName(tempStory);

            if (!string.IsNullOrEmpty(name))
            {
                return name;
            }

            safety++;
        }

        return "Unknown";
    }

   //char   kakuninn
    private string ExtractCharacterName(Story story)
    {
        if (story.canContinue)
        {
            story.Continue();
        }

        foreach (string tag in story.currentTags)
        {
            if (tag.StartsWith("char:"))
            {
                string name = tag.Substring(5).Trim();
                return string.IsNullOrEmpty(name) ? "Unknown" : name;
            }
        }

        return "Unknown";//namaetorennyatuannnounnn
    }

    private void ShuffleList(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);
            (list[i], list[rand]) = (list[rand], list[i]);
        }
    }
}
