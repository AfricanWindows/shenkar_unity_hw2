using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Listens for "no lives left", shows the Game Over screen and restarts the level.
/// It does not count anything itself - that is PlayerLives' job.
/// </summary>
public class GameOverController : MonoBehaviour
{
    [SerializeField] private PlayerLives playerLives;

    [Tooltip("Panel with the GAME OVER text. Hidden while playing.")]
    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private float restartDelay = 2f;

    private bool isGameOver = false;

    private void OnEnable()
    {
        if (playerLives != null)
            playerLives.OnAllLivesLost += OnAllLivesLost;
    }

    private void OnDisable()
    {
        if (playerLives != null)
            playerLives.OnAllLivesLost -= OnAllLivesLost;
    }

    private void Start()
    {
        if (playerLives == null)
            Debug.LogError("GameOverController: playerLives is not assigned", this);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    private void OnAllLivesLost()
    {
        if (isGameOver)
            return;

        isGameOver = true;
        StartCoroutine(GameOverRoutine());
    }

    private IEnumerator GameOverRoutine()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        // Freeze the game so the player cannot keep moving during the screen.
        Time.timeScale = 0f;

        // Realtime, because timeScale is 0.
        yield return new WaitForSecondsRealtime(restartDelay);

        Time.timeScale = 1f;
        RestartLevel();
    }

    private void RestartLevel()
    {
        Scene current = SceneManager.GetActiveScene();

        if (current.buildIndex < 0)
        {
            Debug.LogError("Scene '" + current.name + "' is not in Build Settings. " +
                           "Open File > Build Profiles (Build Settings) and add it, otherwise it cannot be reloaded.");
            return;
        }

        SceneManager.LoadScene(current.buildIndex);
    }
}
