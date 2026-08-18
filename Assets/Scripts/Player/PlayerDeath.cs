using System;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public static event Action OnPlayerDied;

    private Vector3 startPositon;

    private PlayerInvincible invincible;

    private void OnEnable()
    {
        SC_Death.OnSpikeCollision += OnSpikeCollision;
    }

    private void OnDisable()
    {
        SC_Death.OnSpikeCollision -= OnSpikeCollision;
    }

    void Awake()
    {
        startPositon = transform.position;
        invincible = GetComponent<PlayerInvincible>();
    }

    public void Respawn()
    {
        transform.position = startPositon;
    }

    /// <summary>Kills Mario: respawn + tell everyone (PlayerLives listens).</summary>
    public void Kill()
    {
        if (invincible != null && invincible.IsInvincible)
            return;

        Respawn();

        if (OnPlayerDied != null)
            OnPlayerDied();
    }

    private void OnSpikeCollision()
    {
        Kill();
    }
}
