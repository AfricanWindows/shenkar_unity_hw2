using UnityEngine;

/// <summary>
/// Remembers whether Mario picked up the level key. Nothing else.
/// </summary>
public class PlayerKeys : MonoBehaviour
{
    private int keys = 0;

    public bool HasKey
    {
        get { return keys > 0; }
    }

    public void AddKey(int amount)
    {
        if (amount <= 0)
            return;

        keys += amount;
    }
}
