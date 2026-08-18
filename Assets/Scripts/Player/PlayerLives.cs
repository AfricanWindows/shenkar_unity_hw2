using System;
using UnityEngine;

/// <summary>
/// Holds how many lives the player has left. Knows nothing about UI.
/// </summary>
public class PlayerLives : MonoBehaviour, ICounter
{
    [SerializeField] private int startLives = 3;

    private int lives;

    public int Value
    {
        get { return lives; }
    }

    public event Action<int> OnValueChanged;

    public event Action OnAllLivesLost;

    private void Awake()
    {
        lives = startLives;
    }

    private void OnEnable()
    {
        PlayerDeath.OnPlayerDied += LoseLife;
    }

    private void OnDisable()
    {
        PlayerDeath.OnPlayerDied -= LoseLife;
    }

    /// <summary>Used later by the Strike pickable (exercise item 4).</summary>
    public void AddLives(int amount)
    {
        if (amount <= 0)
            return;

        lives += amount;
        RaiseValueChanged();
    }

    public void LoseLife()
    {
        if (lives <= 0)
            return;

        lives--;
        RaiseValueChanged();

        if (lives <= 0 && OnAllLivesLost != null)
            OnAllLivesLost();
    }

    private void RaiseValueChanged()
    {
        if (OnValueChanged != null)
            OnValueChanged(lives);
    }
}
