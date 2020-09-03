using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreText;
    public Text hightScoreText;

    int score;
    int hightScore;

    private static UIManager instance;

    public UIManager GetInstance() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);

        return instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        hightScore = PlayerPrefs.GetInt("hight_score", 0);
        UpdateUI();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateUI();
    }

    void UpdateUI() {
        scoreText.text = $"{score}";
        hightScoreText.text = $"{hightScore}";
    }

    private void OnApplicationQuit()
    {
        if (score > hightScore)
            PlayerPrefs.SetInt("hight_score", score);

        if (instance == this)
            instance = null;
    }

    private void OnDestroy()
    {
        if(score > hightScore)
            PlayerPrefs.SetInt("hight_score", score);

        if (instance == this)
            instance = null;
    }
}
