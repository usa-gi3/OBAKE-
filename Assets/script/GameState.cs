using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance;

    public int score;
    public int outCount;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Load();
    }

    public void AddScore()
    {
        score++;
        Debug.Log($"Score +1 Å® {score}");
        Save();
    }

    public void AddOut()
    {
        outCount++;
        Debug.Log($"Out +1 Å® {outCount}");
        Save();
    }

    public void ResetAll()
    {
        score = 0;
        outCount = 0;
        Save();
    }

    void Save()
    {
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("Out", outCount);
        PlayerPrefs.Save();
    }

    void Load()
    {
        score = PlayerPrefs.GetInt("Score", 0);
        outCount = PlayerPrefs.GetInt("Out", 0);
    }
}
