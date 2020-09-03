using UnityEngine;

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
        
    }

    public void AddPoints()
    {
    
    }

    private void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }
}
