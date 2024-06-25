using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public EtityFX fx { get; private set; }
    public SpriteRenderer sr { get; private set; }

    public CharacterStats stats { get; private set; }

    public CapsuleCollider2D cd { get; private set; }
    #endregion
    [Header("Knockback Info")]
    [SerializeField] protected Vector2 kockbackDirection;
    [SerializeField] protected float knockbackDuration;
    protected bool isKnockback;
    [Header("Check Info")]
    public Transform attackCheck;
    public float attackCheckRadius;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallDistance;
    [SerializeField] protected LayerMask whatIsGround;
    public int facingDir { get; private set; } = 1;
    protected bool facingRight = true;

    public System.Action onFlipeed;
    protected virtual void Awake()
    {
        
    }
    protected virtual void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        fx = GetComponent<EtityFX>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        stats = GetComponent<CharacterStats>();
        cd = GetComponent<CapsuleCollider2D>();
    }

    protected virtual void Update()
    {
        
    }

    public virtual void SlowEntityFx(float _slowPercentage, float _slowDuration)
    {

    }

    public virtual void ReturnDefaultSpeed()
    {
        anim.speed = 1;
    }

    public void DamageImpact()
    {
        StartCoroutine("HitKnockback");
    }
    public virtual IEnumerator HitKnockback()
    {
        isKnockback = true;
        rb.velocity = new Vector2(kockbackDirection.x * -facingDir, kockbackDirection.y);
        yield return new WaitForSeconds(knockbackDuration);
        isKnockback = false;
    }


    #region Collision
    public virtual bool IsGroundedDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundDistance, whatIsGround);
    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallDistance, whatIsGround);
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallDistance, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
    #endregion
    #region Flip
    public virtual void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);

        if (onFlipeed != null)
            onFlipeed();
    }
    public virtual void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
        {
            Flip();
        }
        else if (_x < 0 && facingRight)
        {
            Flip();
        }
    }
    #endregion
    #region Velocity
    public void ZeroVelocity() 
    {
        if (isKnockback) 
            return;
        rb.velocity = new Vector2(0, 0);
    } 
    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        if (isKnockback)
            return;
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }
    #endregion

    public virtual void Die()
    {

    }
}
