using UnityEngine;
using System.Collections.Generic;

public class ChoiceServer : MonoBehaviour
{
    private int good = 0;
    private int bad = 0;

    private readonly HashSet<string> registered = new HashSet<string>();
    private readonly HashSet<string> roster = new HashSet<string>();

    void Start()
    {
        LoadRosterFromNPCMEIBO();
    }

    void LoadRosterFromNPCMEIBO()
    {
        roster.Clear();

        if (NPCMEIBO.Instance == null)
        {
            Debug.LogError("[ChoiceServer] NPCMEIBO.Instance が存在しません");
            return;
        }

        foreach (var id in NPCMEIBO.Instance.selectedStoryIds)
        {
            roster.Add(id);
            Debug.Log($"[ChoiceServer] 名簿追加: {id}");
        }

        Debug.Log($"[ChoiceServer] 名簿人数: {roster.Count}");
    }

    /// <summary>
    /// choiceIndex
    /// 0 = 受け取る / 返す
    /// 1 = 受け取らない / 返さない
    /// </summary>
    public void RegisterChoice(string storyId, int choiceIndex)
    {
        if (registered.Contains(storyId)) return;
        registered.Add(storyId);

        bool inRoster = roster.Contains(storyId);
        bool acted = (choiceIndex == 0);

        bool isGood;

        if (inRoster)
        {
            // 名簿にいる人を無視したら Bad
            isGood = acted;
        }
        else
        {
            // 名簿にいない人に触ったら Bad
            isGood = !acted;
        }

        if (isGood)
        {
            good++;
            Debug.Log($"[ChoiceServer] {storyId} → Good（{good}）");
        }
        else
        {
            bad++;
            Debug.Log($"[ChoiceServer] {storyId} → Bad（{bad}）");
        }

        PlayerPrefs.SetInt("GOOD_SCORE", good);
        PlayerPrefs.SetInt("BAD_SCORE", bad);
        PlayerPrefs.Save();
    }

    public int GetGood() => good;
    public int GetBad() => bad;
}
