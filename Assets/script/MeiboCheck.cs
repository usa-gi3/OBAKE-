using UnityEngine;
using TMPro;

public class MeiboCheck : MonoBehaviour
{
    public GameObject meiboPanel;
    public TextMeshProUGUI[] nameTexts;

    bool isOpen = false;
    bool justOpened = false;   // ★ 追加：開いた直後フラグ

    void Start()
    {
        if (meiboPanel != null)
            meiboPanel.SetActive(false);
    }

    void Update()
    {
        if (!isOpen) return;

        // ★ 開いた直後のクリックは無視
        if (justOpened)
        {
            justOpened = false;
            return;
        }

        // ★ それ以降はどこクリックしても消す
        if (Input.GetMouseButtonDown(0))
        {
            HideMeibo();
            isOpen = false;
        }
    }

    void OnMouseDown()
    {
        if (isOpen) return;

        ShowMeibo();
        isOpen = true;
        justOpened = true;   // ★ 追加
    }

    void ShowMeibo()
    {
        if (NPCMEIBO.Instance == null) return;

        meiboPanel.SetActive(true);

        foreach (var tmp in nameTexts)
        {
            if (tmp != null)
                tmp.text = "";
        }

        for (int i = 0; i < NPCMEIBO.Instance.selectedStoryIds.Count; i++)
        {
            if (i >= nameTexts.Length) break;

            string storyId = NPCMEIBO.Instance.selectedStoryIds[i];
            nameTexts[i].text =
                NPCMEIBO.Instance.GetCharacterName(storyId);
        }
    }

    void HideMeibo()
    {
        meiboPanel.SetActive(false);
    }
}
