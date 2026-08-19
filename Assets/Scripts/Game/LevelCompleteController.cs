using UnityEngine;

/// <summary>
/// Shows the "LEVEL COMPLETE" screen when a door reports the level is finished.
/// The door only detects - showing the screen is this class' job.
///
/// It listens to a STATIC event, so it works with a door created by the Level Creator
/// and does not need a reference to an object that did not exist at edit time.
/// </summary>
public class LevelCompleteController : MonoBehaviour
{
    [Tooltip("Panel with the LEVEL COMPLETE text. Hidden while playing.")]
    [SerializeField] private GameObject levelCompletePanel;

    private void OnEnable()
    {
        LevelExitDoor.OnLevelCompleted += OnLevelCompleted;
    }

    private void OnDisable()
    {
        LevelExitDoor.OnLevelCompleted -= OnLevelCompleted;
    }

    private void Start()
    {
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
