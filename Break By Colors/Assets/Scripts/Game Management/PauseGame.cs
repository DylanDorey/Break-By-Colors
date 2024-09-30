using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public bool paused = false;

    private void OnEnable()
    {
        GameEventBus.Subscribe(GameState.pauseGame, Pause);
        GameEventBus.Subscribe(GameState.resumeGame, Resume);
    }

    private void OnDisable()
    {
        GameEventBus.Unsubscribe(GameState.pauseGame, Pause);
        GameEventBus.Unsubscribe(GameState.resumeGame, Resume);
    }

    /// <summary>
    /// Pauses the game
    /// </summary>
    public void Pause()
    {
        Time.timeScale = 0f;
        paused = true;
    }

    /// <summary>
    /// Resumes the game
    /// </summary>
    public void Resume()
    {
        Time.timeScale = 1f;
        paused = false;
    }
}
