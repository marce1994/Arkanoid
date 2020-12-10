﻿using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public event Action OnRestartGame;
    public event Action OnLossLife;

    public event Action<int> OnScorePoints;

    private void Awake()
    {
        OnRestartGame += () => { Debug.Log("onRestartGame"); };
        OnLossLife += () => { Debug.Log("onLossLife"); };
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
        OnRestartGame();
        StartCoroutine(ReloadScene());
    }

    public void LossLife()
    {
        OnLossLife();
    }

    public void AddPoints(int points)
    {
        OnScorePoints(points);
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

        OnLossLife = null;
    }

    private void OnDestroy()
    {
        if (instance == this)
            instance = null;

        OnLossLife = null;
    }
}
