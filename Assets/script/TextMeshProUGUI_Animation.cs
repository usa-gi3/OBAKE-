using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextMeshProUGUI_Animation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmpUGUI; // 表示対象のTextMeshProUGUI
    private float interval = 0.1f;   // 文字を出す間隔（秒）

    private string fullText;      // 全文（アニメーション対象の文字列）
    private string currentText; // 現在表示されている文字列
    private int index = 0;        // 何文字目まで表示したかを管理するカウンタ

    void Awake()
    {
        tmpUGUI = GetComponent<TextMeshProUGUI>();
    }

    public void PlayText(string text)
    {
        CancelInvoke();

        fullText = text;
        currentText = "";
        index = 0;

        tmpUGUI.text = "";
        InvokeRepeating(nameof(ShowNextChar), 0f, interval);
    }

    void ShowNextChar()
    {
        // まだ全文を表示していない場合
        if (index < fullText.Length)
        {
            // 1文字追加
            currentText += fullText[index];

            // TextMeshProに反映
            tmpUGUI.text = currentText;

            // 次の文字へ進める
            index++;
        }
        else
        {
            // 全文表示が終わったら繰り返し呼び出しを停止
            CancelInvoke(nameof(ShowNextChar));
        }
    }
}
