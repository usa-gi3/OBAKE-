using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Endchoice : MonoBehaviour
{
    public int GetResultScore()
    {
        return PlayerPrefs.GetInt("GOOD_SCORE", 0);
    }

    public string GetEndKnot(int score)
    {
        if (score <= 0) return "End5";
        if (score == 1) return "End2";
        if (score == 2) return "End3";
        return "End6";
    }
}
