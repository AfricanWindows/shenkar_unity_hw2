using UnityEngine;

public class ProjectileAxe : MonoBehaviour
{
    public float speedX = 5f;
    public float speedY = 5f;
    public float lifetime = 3f;

    [SerializeField] private int damage = 1;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Attack(float direction)
    {
        if(rb != null)
        {
            transform.localScale = new Vector3(direction, 1, 1);
            rb.AddForce(new Vector2(direction * speedX, speedY));
            Destroy(gameObject, lifetime);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col == null)
            return;

        IDamageable target = col.GetComponent<IDamageable>();
        if (target == null)
            return;

        target.TakeDamage(damage);
        Destroy(gameObject);
    }
}
