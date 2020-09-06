using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState {
    Play,
    Pause,
    Win,
    Loose
}

public class GameManager : MonoBehaviour
{
    public GameState gameState;
    private static GameManager instance;

    public event Action onRestartGame;
    public event Action onLossLife;

    public event Action<int> onScorePoints;

    private void Awake()
    {
        onRestartGame += () => { Debug.Log("onRestartGame"); };
        onLossLife += () => { Debug.Log("onLossLife"); };
    }

    public static GameManager GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<GameManager>();
            if (instance == null)
            {
                GameObject container = new GameObject("GameManager");
                instance = container.AddComponent<GameManager>();
            }
        }

        return instance;
    }

    public void RestartGame()
    {
        onRestartGame();
        StartCoroutine(ReloadScene());
    }

    public void LossLife()
    {
        onLossLife();
    }

    public void AddPoints(int points)
    {
        onScorePoints(points);
        if (GameObject.FindGameObjectsWithTag("Block").Count() > 0) return;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator ReloadScene()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(5);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnApplicationQuit()
    {
        if (instance == this)
            instance = null;

        onLossLife = null;
    }

    private void OnDestroy()
    {
        if (instance == this)
            instance = null;

        onLossLife = null;
    }
}
