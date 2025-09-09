using System.Collections;
using Ojanck.Core.Scene;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Player player;
    private PlayerInfo playerInfo;
    private PlayerAnimation playerAnimation;

    private Rigidbody2D playerRB;

    private InputManager inputManager;
    private bool isLookingRight = true;

    [SerializeField] private float attackDuration = 0.5f;
    [SerializeField] private GameObject attackHitbox;

    private void Start()
    {
        player = Player.localPlayer;
        playerInfo = player.PlayerInfo;
        playerAnimation = player.PlayerAnimation;
        playerRB = player.RigidBody;

        inputManager = SceneCore.GetService<InputManager>();
        inputManager.onJumpInput.AddListener(Jump);
        inputManager.onLeftInput.AddListener(() => Move(false));
        inputManager.onRightInput.AddListener(() => Move(true));
        inputManager.onAttackInput.AddListener(Attack);
    }

    public void Jump()
    {
        if (player.IsAttacking || player.IsHit || player.IsDead) return;

        if (player.IsGrounded)
        {
            playerRB.linearVelocity = new Vector2(playerRB.linearVelocityX, 0f);
            playerRB.AddForce(Vector2.up * playerInfo.jumpForce, ForceMode2D.Impulse);

            playerAnimation.SetState(PlayerState.Jump);
        }
    }

    public void Move(bool isRight)
    {
        if (player.IsAttacking || player.IsHit || player.IsDead) return;

        float direction = isRight ? 1f : -1f;
        playerRB.linearVelocity = new Vector2(direction * playerInfo.speed, playerRB.linearVelocityY);

        if (isLookingRight != isRight)
        {
            isLookingRight = isRight;

            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * (isRight ? 1 : -1);

            transform.localScale = scale;
        }
    }

    public void Attack()
    {
        if (player.IsAttacking || player.IsHit || player.IsDead) return;

        player.IsAttacking = true;
        playerAnimation.SetState(PlayerState.Attack);
        attackHitbox.SetActive(true);

        if (player.IsGrounded && (playerRB.linearVelocityX > 0.1f || playerRB.linearVelocityX < -0.1f))
        {
            Vector2 attackDirection = new Vector2(transform.localScale.x, 0).normalized;
            playerRB.linearVelocity = new Vector2(0, playerRB.linearVelocityY);
            playerRB.AddForce(attackDirection * 2f, ForceMode2D.Impulse);
        }

        StartCoroutine(StartAttackCooldown());
    }

    private IEnumerator StartAttackCooldown()
    {
        yield return new WaitForSeconds(attackDuration);

        player.IsAttacking = false;
        attackHitbox.SetActive(false);
    }
}
