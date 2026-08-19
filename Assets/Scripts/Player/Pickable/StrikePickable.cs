using UnityEngine;

/// <summary>
/// The heart pickable (exercise item 2): gives Mario one health point back.
/// PlayerHealthModel refuses it when he already holds the maximum of 3.
/// </summary>
public class StrikePickable : BasePickable
{
    [SerializeField] private int livesAmount = 1;

    protected override IPowerUp CreatePowerUp()
    {
        return new ExtraLifePowerUp(livesAmount);
    }
}
