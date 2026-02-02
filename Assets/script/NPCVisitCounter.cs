using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCVisitCounter : MonoBehaviour
{
    public string firstScene = "random Scene";
    public string secondScene = "SecondResultScene";
    public void OnStoryFinished()
    {
        int count = PlayerPrefs.GetInt("NPC_VISIT_COUNT", 0);

        count = (count % 2) + 1;

        PlayerPrefs.SetInt("NPC_VISIT_COUNT", count);
        PlayerPrefs.Save();

        if (count == 1)
        {
            SceneManager.LoadScene(firstScene);
        }
        else // count == 2
        {
            SceneManager.LoadScene(secondScene);
        }
    }

    [ContextMenu("Reset Visit Count")]
    void ResetCount()
    {
        PlayerPrefs.DeleteKey("NPC_VISIT_COUNT");
    }
    
}
