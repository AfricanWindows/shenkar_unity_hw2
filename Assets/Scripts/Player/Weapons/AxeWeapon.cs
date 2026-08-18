using System;
using UnityEngine;

public class AxeWeapon : MonoBehaviour, IReloadWeapon, ICounter
{
    public GameObject projectile;

    [Tooltip("How many axes Mario starts the level with")]
    [SerializeField] private int startAmmo = 0;

    private int ammo;

    public int Value
    {
        get { return ammo; }
    }

    public event Action<int> OnValueChanged;

    private void Awake()
    {
        ammo = startAmmo;
    }

    public void Attack()
    {
        if (projectile == null || ammo <= 0)
            return;

        GameObject curProjectile = Instantiate(projectile, transform.position, new Quaternion(0, 0, 0, 0));
        ProjectileAxe scProjectile = curProjectile.GetComponent<ProjectileAxe>();
        if (scProjectile != null)
        {
            float direction = 1;
            if (transform.parent != null)
                direction = transform.parent.localScale.x;
            scProjectile.Attack(direction);
        }

        ammo--;
        RaiseValueChanged();
    }

    public void AddAmmo(int amount)
    {
        if (amount <= 0)
            return;

        ammo += amount;
        RaiseValueChanged();
    }

    public void Reload()
    {
        AddAmmo(1);
    }

    private void RaiseValueChanged()
    {
        if (OnValueChanged != null)
            OnValueChanged(ammo);
    }
}
