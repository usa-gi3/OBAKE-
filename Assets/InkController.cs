using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Ink.Runtime;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class InkController : MonoBehaviour
{
    [Header("Ink")]
    public TextAsset inkJSONAsset;

    [Header("UI")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public GameObject choiceButtonPrefab;
    public Transform choiceButtonContainer;

    private Story story;
    bool choiceSelected = false;
    bool blockClick = false;


    public void StartKnot(TextAsset inkJSON, string knotName)
    {
        if (inkJSON == null)
        {
            Debug.LogError("Ink JSON が null です");
            return;
        }

        inkJSONAsset = inkJSON;

        story = new Story(inkJSONAsset.text);
        story.ChoosePathString(knotName);   // ← 指定したknotを読む

        dialoguePanel.SetActive(true);
        ContinueStory();


    }

    void ContinueStory()
    {
        blockClick = false;
        dialogueText.text = "";

        string fullText = "";


        if (choiceSelected)
        {
            while (story.canContinue)
            {
                string line = story.Continue().Trim();
                fullText += line + "\n";
            }

            string last = story.currentText?.Trim();
            if (!string.IsNullOrEmpty(last))
            {
                fullText += last;
            }

            choiceSelected = false;
        }
        else
        {
            if (story.canContinue)
            {
                fullText = story.Continue().Trim();
            }
            else
            {
                fullText = story.currentText?.Trim();
            }
        }

        dialogueText.text = fullText;
        RefreshChoices();
    }


    void RefreshChoices()
    {
        Debug.Log("RefreshChoices呼び出し");
        // 既存の選択肢を削除
        foreach (Transform child in choiceButtonContainer)
        {
            Destroy(child.gameObject);
        }

        // 選択肢を生成
        for (int i = 0; i < story.currentChoices.Count; i++)
        {
            Choice choice = story.currentChoices[i];
            GameObject buttonObj = Instantiate(choiceButtonPrefab, choiceButtonContainer);
            buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = choice.text;

            int choiceIndex = i;
            buttonObj.GetComponent<Button>().onClick.AddListener(() => OnChoiceSelected(choiceIndex));
        }

        // 選択肢がない場合は終了
        if (!choiceSelected && story.currentChoices.Count == 0 && !story.canContinue)
        {
            EndDialogue();

            NPCchoice npc = FindObjectOfType<NPCchoice>();
            if (npc != null)
                npc.PlaySecondStory();
        }
    }

    void OnChoiceSelected(int choiceIndex)
    {
        Debug.Log("選択肢を選んだ: " + choiceIndex);

        blockClick = false;
        choiceSelected = false;

        story.ChooseChoiceIndex(choiceIndex);

        ContinueStory();
    }

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        Debug.Log("Dialogue ended.");

        // 2回目だったらリセット
        if (PlayerPrefs.HasKey("FirstStoryPlayed"))
        {
            PlayerPrefs.DeleteKey("FirstStoryPlayed");
            PlayerPrefs.DeleteKey("FirstStory");
            PlayerPrefs.Save();
            Debug.Log("ストーリー情報をリセットしました");
        }
    }

    void Update()
    {
        if (story == null || dialoguePanel == null || blockClick)
            return;

        if (dialoguePanel.activeSelf && Input.GetMouseButtonDown(0))
        {
            Debug.Log("クリックした");
            Debug.Log("choiceSelectedは" + choiceSelected);
            if (story.canContinue)
                ContinueStory();
        }
    }
}