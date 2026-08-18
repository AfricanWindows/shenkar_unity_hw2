using UnityEngine;

/// <summary>Pickable that gives Mario an extra life (exercise item 4).</summary>
public class StrikePickable : BasePickable
{
    [SerializeField] private int livesAmount = 1;

    protected override IPowerUp CreatePowerUp()
    {
        return new ExtraLifePowerUp(livesAmount);
    }
}
