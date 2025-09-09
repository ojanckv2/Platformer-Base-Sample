using System.Collections;
using Ojanck.Core.Scene;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player localPlayer;
    
    private View_PlayerHealth playerHealthView;

    [SerializeField] private PlayerInfo playerInfo;
    public PlayerInfo PlayerInfo => playerInfo;

    [SerializeField] private PlayerAnimation playerAnimation;
    public PlayerAnimation PlayerAnimation => playerAnimation;

    [SerializeField] private Rigidbody2D rb;
    public Rigidbody2D RigidBody => rb;

    private bool isGrounded;
    public bool IsGrounded => isGrounded;

    private bool isAttacking;
    public bool IsAttacking
    {
        get => isAttacking;
        set => isAttacking = value;
    }

    private float sphereRadius = 0.3f;
    private float sphereDistance = 1.2f;
    private string groundMask = "Ground";

    private int currentHealth;
    
    private bool isDead;
    public bool IsDead => isDead;

    private bool isHit;
    public bool IsHit => isHit;

    private void Awake()
    {
        if (localPlayer == null)
            localPlayer = this;

        currentHealth = playerInfo.health;
    }

    private void Start()
    {
        playerHealthView = SceneCoreView.GetSceneServiceView<View_PlayerHealth>();
        playerHealthView.SetRemainingHearts(currentHealth);
    }

    private void Update()
    {
        isGrounded = CheckIfGrounded();

        playerAnimation.SetYVelocity(rb.linearVelocityY, isGrounded);

        if (isAttacking || isHit || isDead) return;

        if (isGrounded && (Mathf.Abs(rb.linearVelocityX) > 0.1f))
            playerAnimation.SetState(PlayerState.Run);
        else if (isGrounded)
            playerAnimation.SetState(PlayerState.Idle);
        else if (!isGrounded && rb.linearVelocityY < -0.1f)
            playerAnimation.SetState(PlayerState.Fall);
    }

    public void OnPlayerHit()
    {
        if (isDead) return;
        isHit = true;

        currentHealth--;
        playerHealthView.SetRemainingHearts(currentHealth);

        playerAnimation.SetState(PlayerState.Hurt);

        // Apply knockback force
        float knockbackForce = 7f;
        Vector2 knockbackDirection = new Vector2(-transform.localScale.x, 1).normalized;
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

        if (previousHitCooldown != null)
            StopCoroutine(previousHitCooldown);
        
        previousHitCooldown = HitCooldown();
        StartCoroutine(previousHitCooldown);

        if (currentHealth <= 0)
        {
            isDead = true;
            playerAnimation.SetState(PlayerState.Dead);

            StartCoroutine(DeathCooldown());
        }
    }

    private IEnumerator previousHitCooldown;
    private IEnumerator HitCooldown()
    {
        yield return new WaitForSeconds(1f);
        isHit = false;
        previousHitCooldown = null;
    }

    private IEnumerator DeathCooldown()
    {
        yield return new WaitForSeconds(5f);

        // Handle post-death logic here (e.g., respawn, game over screen)
        isDead = false;
        currentHealth = playerInfo.health;
        playerHealthView.SetRemainingHearts(currentHealth);
    }

    private bool CheckIfGrounded()
    {
        return Physics2D.CircleCast(
            transform.position,       // Start from player center
            sphereRadius,             // Radius of the sphere
            Vector2.down,             // Direction: downward
            sphereDistance,
            LayerMask.GetMask(groundMask)
        );
    }
}

[System.Serializable]
public class PlayerInfo
{
    public int health = 3;
    public float speed = 5f;
    public float jumpForce = 7f;
}