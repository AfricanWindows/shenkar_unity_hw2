using UnityEngine;

public class StarPowerUp : IPowerUp
{
    public void ApplyPowerUp(GameObject player)
    {
        if(player != null)
        {
            Debug.Log("StarPowerUp applied to " + player.name);
            PlayerInvincible invincible = player.GetComponent<PlayerInvincible>();
            if(invincible != null)
            {
                invincible.ActivateInvincibility();
            }
        }
    }
}
