using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Moves Mario left and right.
///
/// It OWNS its speed: no other class writes into the field from outside. A temporary
/// boost is asked for through SetSpeedMultiplier, so the lightning effect never has to
/// know what the normal speed is, or remember to put it back.
/// </summary>
public class PlayerMovement : MonoBehaviour, IFacing
{
    [Tooltip("Normal walking speed, before any power up")]
    [SerializeField] private float speed = 5f;

    private float speedMultiplier = 1f;
    private float facingDirection = 1f;
    private float direction;
    private Rigidbody2D rigid;

    /// <summary>Speed actually used right now: normal speed times the active multiplier.</summary>
    public float CurrentSpeed
    {
        get { return speed * speedMultiplier; }
    }

    /// <summary>Which way Mario looks right now. Weapons aim by this, not by the scale.</summary>
    public float FacingDirection
    {
        get { return facingDirection; }
    }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Used by timed effects. 1.5 means "+50% while the effect lasts", 1 means normal.
    /// </summary>
    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier > 0f ? multiplier : 1f;
    }

    private void FixedUpdate()
    {
        direction = 0f;
        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
                direction = -1f;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
                direction = 1f;
        }

        if (direction != 0 && rigid != null)
        {
            rigid.linearVelocity = new Vector2(direction * CurrentSpeed, rigid.linearVelocity.y);

            facingDirection = direction > 0 ? 1f : -1f;
            transform.localScale = new Vector3(facingDirection, 1, 1);
        }
    }
}
