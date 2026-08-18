using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

/// <summary>
/// Collects every IWeapon found under the player and lets him switch and fire.
/// New weapons are registered automatically - no code change needed here (OCP).
/// </summary>
public class WeaponsHandler : MonoBehaviour
{
    [Tooltip("Where to look for weapons. Empty = this object's parent (the player).")]
    [SerializeField] private Transform weaponsRoot;

    private List<IWeapon> weapons = new List<IWeapon>();
    private int index = 0;

    public void Awake()
    {
        weapons = new List<IWeapon>();
        CollectWeapons();
    }

    private void CollectWeapons()
    {
        Transform root = weaponsRoot;
        if (root == null)
            root = transform.parent != null ? transform.parent : transform;

        IWeapon[] found = root.GetComponentsInChildren<IWeapon>(true);
        for (int i = 0; i < found.Length; i++)
            AddWeapon(found[i]);

        Debug.Log("WeaponsHandler: found " + weapons.Count + " weapon(s) under " + root.name);
    }

    public void AddWeapon(IWeapon weapon)
    {
        if(weapon != null && !weapons.Contains(weapon))
            weapons.Add(weapon);
    }

    public void SelectWeapon(int newIndex)
    {
        if (newIndex < 0 || newIndex >= weapons.Count)
            return;

        index = newIndex;
        Debug.Log("Selected weapon " + (index + 1) + ": " + weapons[index].GetType().Name);
    }

    void Update()
    {
        if (Keyboard.current == null)
            return;

        if (Keyboard.current.digit1Key.wasPressedThisFrame)
            SelectWeapon(0);
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
            SelectWeapon(1);

        if(Keyboard.current.leftCtrlKey.wasPressedThisFrame && index < weapons.Count)
            weapons[index].Attack();
    }
}
