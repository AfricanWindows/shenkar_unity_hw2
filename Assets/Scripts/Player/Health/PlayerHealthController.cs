using System;
using UnityEngine;

/// <summary>
/// CONTROLLER of the health feature (exercise item 2).
///
/// It is the only piece that talks to Unity: it listens to what happens in the game
/// (spikes, hearts), tells the MODEL what to do, and publishes the result so the
/// VIEW (UI_CounterView) can show it. It contains no health rules of its own -
/// the "maximum 3" rule lives in PlayerHealthModel.
/// </summary>
[DisallowMultipleComponent]
public class PlayerHealthController : MonoBehaviour, ICounter
{
    [Tooltip("Maximum hearts Mario can hold (exercise says 3)")]
    [SerializeField] private int maxHealth = 3;

    [Tooltip("Hearts Mario starts the level with")]
    [SerializeField] private int startHealth = 3;

    private PlayerHealthModel model;

    /// <summary>Raised when Mario runs out of health. Static, so the Game Over screen
    /// does not need a reference to a player that does not exist yet.</summary>
    public static event Action OnPlayerHealthEmpty;

    public int Value
    {
        get { return model == null ? 0 : model.Current; }
    }

    public int MaxValue
    {
        get { return model == null ? maxHealth : model.Max; }
    }

    public event Action<int> OnValueChanged;

    private void Awake()
    {
        model = new PlayerHealthModel(maxHealth, startHealth);
        model.OnHealthChanged += RaiseValueChanged;
        model.OnHealthEmpty += HandleHealthEmpty;
    }

    private void OnEnable()
    {
        // PlayerDeath already decides WHEN Mario is hit (it checks the star invincibility
        // and respawns him). Here we only turn that into "-1 heart".
        PlayerDeath.OnPlayerDied += LoseHealth;

        CounterRegistry.Register(CounterId.Health, this);
        RaiseValueChanged(Value);
    }

    private void OnDisable()
    {
        PlayerDeath.OnPlayerDied -= LoseHealth;
        CounterRegistry.Unregister(CounterId.Health, this);
    }

    /// <summary>Entry point used by HealthPowerUp when a heart is collected.</summary>
    public bool AddHealth(int amount)
    {
        return model != null && model.Add(amount);
    }

    public void LoseHealth()
    {
        if (model != null)
            model.Remove(1);
    }

    private void HandleHealthEmpty()
    {
        if (OnPlayerHealthEmpty != null)
            OnPlayerHealthEmpty();
    }

    private void RaiseValueChanged(int value)
    {
        if (OnValueChanged != null)
            OnValueChanged(value);
    }
}
