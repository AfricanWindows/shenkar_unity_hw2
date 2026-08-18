using UnityEngine;

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

        PlayerLives playerLives = player.GetComponent<PlayerLives>();
        if (playerLives == null)
        {
            Debug.LogWarning("ExtraLifePowerUp: no PlayerLives on " + player.name);
            return;
        }

        playerLives.AddLives(amount);
    }
}
