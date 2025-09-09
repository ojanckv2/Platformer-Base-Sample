using System.Collections;
using UnityEngine;

public class Enemy_StrawDummy : EnemyBase
{
    [SerializeField] private StrawDummy_Animation strawDummyAnimation;

    private static readonly WaitForSeconds attackStateCD = new(3f);
    private bool isAttacked = false;
    private bool isDead = false;

    public override void OnHit()
    {
        if (isDead) return;

        isAttacked = true;

        currentHealth--;
        if (currentHealth <= 0)
        {
            if (previousCoroutine != null)
                StopCoroutine(previousCoroutine);

            strawDummyAnimation.SetState(StrawDummyState.Dead);
            StartCoroutine(DeathCoroutine());
            return;
        } else {
            strawDummyAnimation.OnHit();
        }

        if (previousCoroutine != null && isAttacked == true)
            StopCoroutine(previousCoroutine);

        previousCoroutine = ResetAttackState();
        StartCoroutine(previousCoroutine);
    }

    private IEnumerator previousCoroutine;
    private IEnumerator ResetAttackState()
    {
        yield return attackStateCD;
        currentHealth = enemyInfo.health;

        isAttacked = false;
        strawDummyAnimation.SetState(StrawDummyState.Idle);
    }

    private IEnumerator DeathCoroutine()
    {
        isDead = true;

        yield return new WaitForSeconds(enemyInfo.deadAnimationTime);

        isAttacked = false;
        isDead = false;
        currentHealth = enemyInfo.health;

        strawDummyAnimation.SetState(StrawDummyState.Idle);
    }
}
