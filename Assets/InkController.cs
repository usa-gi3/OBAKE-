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
        if (story.canContinue)
        {
            string text = story.Continue().Trim();
            dialogueText.text = text;

            // タグをチェックしてシーン移動
            foreach (string tag in story.currentTags)
            {
                Debug.Log("Current Tag: " + tag);
                if (tag.StartsWith("scene:"))
                {
                    string sceneName = tag.Substring("scene:".Length).Trim();
                    Debug.Log("Loading Scene: " + sceneName);
                    SceneManager.LoadScene(sceneName);
                }
            }
        }

        RefreshChoices();
    }

    void RefreshChoices()
    {
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
        if (story.currentChoices.Count == 0 && !story.canContinue)
        {
            EndDialogue();

            NPCchoice npc = FindObjectOfType<NPCchoice>();
            if (npc != null)
                npc.PlaySecondStory();
        }
    }

    void OnChoiceSelected(int choiceIndex)
    {
        story.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        Debug.Log("Dialogue ended.");
    }

    void Update()
    {
        if (story == null || dialoguePanel == null)
            return;

        if (dialoguePanel.activeSelf && Input.GetMouseButtonDown(0))
        {
            if (story.canContinue)
                ContinueStory();
        }
    }
}