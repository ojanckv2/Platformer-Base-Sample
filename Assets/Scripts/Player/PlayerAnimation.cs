using UnityEngine;

public enum PlayerState
{
    Idle,
    Run,
    Jump,
    Fall,
    Attack,
    Hurt,
    Dead
}

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;

    private PlayerState currentState = PlayerState.Idle;
    public PlayerState CurrentState => currentState;

    public void SetState(PlayerState newState)
    {
        if (currentState == newState) return;

        if (newState != PlayerState.Dead || newState != PlayerState.Hurt)
        {
            playerAnimator.SetBool("isDead", false);
            playerAnimator.SetBool("isHurt", false);
        }

        switch (newState)
        {
            case PlayerState.Idle:
                Idle();
                break;
            case PlayerState.Run:
                Run();
                break;
            case PlayerState.Jump:
                Jump();
                break;
            case PlayerState.Fall:
                Fall();
                break;
            case PlayerState.Attack:
                Attack();
                break;
            case PlayerState.Hurt:
                Hurt();
                break;
            case PlayerState.Dead:
                Dead();
                break;
        }
    }

    public void SetYVelocity(float yVelocity, bool isGrounded)
    {
        playerAnimator.SetBool("isGrounded", isGrounded);
        playerAnimator.SetFloat("yVelocity", yVelocity);
    }

    private void Idle()
    {
        playerAnimator.SetBool("Run", false);
        currentState = PlayerState.Idle;
    }

    private void Run()
    {
        playerAnimator.SetBool("Run", true);
        currentState = PlayerState.Run;
    }

    private void Jump()
    {
        playerAnimator.SetTrigger("Jump");
        currentState = PlayerState.Jump;
    }

    private void Fall()
    {
        currentState = PlayerState.Fall;
    }

    private void Attack()
    {
        playerAnimator.SetTrigger("Attack");
        currentState = PlayerState.Attack;
    }

    private void Hurt()
    {
        playerAnimator.SetTrigger("Hurt");
        playerAnimator.SetBool("isHurt", true);
        currentState = PlayerState.Hurt;
    }

    private void Dead()
    {
        playerAnimator.SetTrigger("Hurt");
        playerAnimator.SetBool("isHurt", true);
        playerAnimator.SetBool("isDead", true);
        currentState = PlayerState.Dead;
    }
}