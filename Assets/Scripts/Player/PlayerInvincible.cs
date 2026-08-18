using System.Collections;
using UnityEngine;

public class PlayerInvincible : MonoBehaviour
{
    private bool _isInvincible = false;
    public bool IsInvincible { get { return _isInvincible; } }

    public float powerUpDuration = 5f; // Duration of invincibility in seconds

    private SpriteRenderer _spriteRenderer; 

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ActivateInvincibility()
    {
        Debug.Log("Invincibility activated for " + gameObject.name);
        StartCoroutine(InvincibilityCoroutine());   
    }

    private IEnumerator InvincibilityCoroutine()
    {
        _isInvincible = true;
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(powerUpDuration);
        _spriteRenderer.color = Color.white;
        _isInvincible = false;
        Debug.Log("Invincibility ended for " + gameObject.name);
    }
}
