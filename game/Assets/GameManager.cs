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

    public GameManager GetInstance() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);
        return instance;
    }

    private void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }
}
