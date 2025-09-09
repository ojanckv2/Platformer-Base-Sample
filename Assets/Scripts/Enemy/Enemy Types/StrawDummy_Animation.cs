using UnityEngine;

public enum StrawDummyState
{
    Idle,
    Dead
}

public class StrawDummy_Animation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private StrawDummyState currentState = StrawDummyState.Idle;
    public StrawDummyState CurrentState => currentState;

    public void SetState(StrawDummyState newState)
    {
        if (currentState == newState) return;

        switch (newState)
        {
            case StrawDummyState.Idle:
                Idle();
                break;
            case StrawDummyState.Dead:
                Dead();
                break;
            default:
                Debug.LogError("Unhandled state: " + newState);
                break;
        }

        currentState = newState;
    }

    private void Idle()
    {
        animator.SetBool("Dead", false);
    }

    public void OnHit()
    {
        animator.SetTrigger("Hit");
    }

    private void Dead()
    {
        animator.SetTrigger("Hit");
        animator.SetBool("Dead", true);
    }
}