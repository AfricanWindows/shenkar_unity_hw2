using UnityEngine;

/// <summary>The strike pickable: gives Mario a health point back, same as a heart.</summary>
public class ExtraLifePowerUp : IPowerUp
{
    private readonly int amount;

    public ExtraLifePowerUp(int amount)
    {
        this.amount = amount;
    }

    public void ApplyPowerUp(GameObject player)
    {
        if (player == null)
            return;

        PlayerHealthController health = player.GetComponent<PlayerHealthController>();
        if (health == null)
        {
            Debug.LogWarning("ExtraLifePowerUp: no PlayerHealthController on " + player.name);
            return;
        }

        health.AddHealth(amount);
    }
}
