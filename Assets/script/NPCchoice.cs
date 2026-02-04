using System.Collections.Generic;
using UnityEngine;

public class NPCchoice : MonoBehaviour
{
    public TextAsset inkJSONAsset;

    private string firstStory;
    private string secondStory;
    string[] stories =
        {
        "story1","story2","story3","story4","story5",
        "story6","story7","story8","story9","story10","story11"
    };

    public string GetStoryForThisVisit()
    {
        // 1回目
        if (!PlayerPrefs.HasKey("FirstStoryPlayed"))
        {
            //選出
            int index = Random.Range(0, stories.Length);
            firstStory = stories[index];

            //保存
            PlayerPrefs.SetString("FirstStory", firstStory);
            PlayerPrefs.SetInt("FirstStoryPlayed", 1);
            PlayerPrefs.Save();

            return firstStory;
        }

        // 2回目
        //一回目を取得
        firstStory = PlayerPrefs.GetString("FirstStory");

        if (!PlayerPrefs.HasKey("SecondStory"))
        {
            //被らないように選出
            List<string> remaining = new List<string>(stories);
            remaining.Remove(firstStory);
            secondStory = remaining[Random.Range(0, remaining.Count)];

            //２回目結果を保存
            PlayerPrefs.SetString("SecondStory", secondStory);
            PlayerPrefs.Save();
        }

        return PlayerPrefs.GetString("SecondStory");
    }
}
