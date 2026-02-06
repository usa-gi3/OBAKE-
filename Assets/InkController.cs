using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Ink.Runtime;
using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class InkController : MonoBehaviour
{
    public event Action<string> onInkResult;

    [Header("Ink")]
    public TextAsset inkJSONAsset;

    [Header("UI")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public GameObject choiceButtonPrefab;
    public Transform choiceButtonContainer;
    public CharacterExpressionSet[] characterSets;
    public Image characterImage; // UI の画像
    public TextMeshProUGUI_Animation textAnimator; //アニメーション用
    public event Action onStoryFinished;

    [Header("ChoiceServer")]
    public string currentStoryId;
    private ChoiceServer choiceServer;

    private Story story;
    bool choiceSelected = false;
    bool blockClick = false;
    bool storyEnded = false;
    bool waitForLastClick = false;

    void Awake()
    {
        choiceServer = FindObjectOfType<ChoiceServer>();
        if (choiceServer == null)
            Debug.LogWarning("[InkController] ChoiceServer が見つかりません");
    }

    void Start()
    {
        textAnimator.onTextFinished += OnTextFinished;
    }

    void OnTextFinished()
    {
        RefreshChoices();
    }

    public void StartKnot(TextAsset inkJSON, string knotName)
    {
        if (inkJSON == null)
        {
            Debug.LogError("Ink JSON が null です");
            return;
        }

        inkJSONAsset = inkJSON;

        story = new Story(inkJSONAsset.text);
        story.ChoosePathString(knotName);

        currentStoryId = knotName;

        dialoguePanel.SetActive(true);
        ContinueStory();
    }

    void ContinueStory()
    {
        ClearChoices();

        blockClick = false;
        dialogueText.text = "";

        string fullText = "";

        if (choiceSelected)
        {
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
                HandleTags(story.currentTags);
            }
            else
            {
                fullText = story.currentText?.Trim();
            }
        }
        textAnimator.PlayText(fullText);
    }

    void HandleTags(List<string> tags)
    {
        foreach (string tag in tags)
        {
            if (tag.StartsWith("char:"))
            {
                string[] parts = tag.Substring(5).Split(' ');

                string charName = parts[0];
                string expName = parts[1];

                foreach (var set in characterSets)
                {
                    if (set.characterName == charName)
                    {
                        foreach (var exp in set.expressions)
                        {
                            if (exp.expressionName == expName)
                            {
                                characterImage.sprite = exp.sprite;
                                return;
                            }
                        }
                    }
                }
            }
        }
    }

    void RefreshChoices()
    {
        if (storyEnded) return;

        Debug.Log("RefreshChoices呼び出し");
        ClearChoices();

        for (int i = 0; i < story.currentChoices.Count; i++)
        {
            Choice choice = story.currentChoices[i];
            GameObject buttonObj = Instantiate(choiceButtonPrefab, choiceButtonContainer);
            buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = choice.text;

            int choiceIndex = i;
            buttonObj.GetComponent<Button>().onClick.AddListener(() => OnChoiceSelected(choiceIndex));
        }

        if (!choiceSelected && story.currentChoices.Count == 0 && !story.canContinue)
        {
            if (!waitForLastClick)
            {
                waitForLastClick = true;
                blockClick = false;
                Debug.Log("最終テキスト表示完了。クリック待ち");
                return;
            }

            storyEnded = true;

            string result = "";
            if (story.variablesState.Contains("doorResult"))
                result = story.variablesState["doorResult"].ToString();

            onInkResult?.Invoke(result);
            onStoryFinished?.Invoke();
            EndDialogue();

            Debug.Log("OnStoryFinished が呼ばれた");
            FindObjectOfType<NPCVisitCounter>()?.OnStoryFinished();
        }
    }

    void OnChoiceSelected(int choiceIndex)
    {
        Debug.Log("選択肢を選んだ: " + choiceIndex);

        if (choiceServer != null && !string.IsNullOrEmpty(currentStoryId))
        {
            choiceServer.RegisterChoice(currentStoryId, choiceIndex);
        }

        blockClick = false;
        choiceSelected = false;

        story.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        Debug.Log("Dialogue ended.");

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
        if (Time.timeScale == 0f) return;

        if (story == null || dialoguePanel == null || blockClick)
            return;

        if (!dialoguePanel.activeSelf)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            if (waitForLastClick && !storyEnded)
            {
                Debug.Log("最終クリック → シーン遷移");
                RefreshChoices();
                return;
            }

            if (story.canContinue)
                ContinueStory();
        }
    }

    public void SetInkVariable(string varName, object value)
    {
        if (story == null)
        {
            Debug.LogWarning("Story is null");
            return;
        }

        story.variablesState[varName] = value;
    }


    void ClearChoices()
    {
        if (choiceButtonContainer == null)
        {
            return;
        }
        foreach (Transform child in choiceButtonContainer)
        {
            Destroy(child.gameObject);
        }
    }
}
