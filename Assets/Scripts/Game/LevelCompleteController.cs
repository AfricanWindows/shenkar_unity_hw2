using UnityEngine;

/// <summary>
/// Shows the "LEVEL COMPLETE" screen when the door reports the level is finished.
/// The door only detects - showing the screen is this class' job.
/// </summary>
public class LevelCompleteController : MonoBehaviour
{
    [SerializeField] private LevelExitDoor door;

    [Tooltip("Panel with the LEVEL COMPLETE text. Hidden while playing.")]
    [SerializeField] private GameObject levelCompletePanel;

    private void OnEnable()
    {
        if (door != null)
            door.OnLevelCompleted += OnLevelCompleted;
    }

    private void OnDisable()
    {
        if (door != null)
            door.OnLevelCompleted -= OnLevelCompleted;
    }

    private void Start()
    {
        if (door == null)
            Debug.LogError("LevelCompleteController: door is not assigned", this);

        if (levelCompletePanel != null)
            levelCompletePanel.SetActive(false);
    }

    private void OnLevelCompleted()
    {
        if (levelCompletePanel != null)
            levelCompletePanel.SetActive(true);

        Time.timeScale = 0f;
    }
}
