using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    private EnemyInfo _enemyInfo;
    [SerializeField] protected EnemyInfo enemyInfo;
    protected int currentHealth;

    [SerializeField] private bool alreadyValidated = false;
    protected virtual void OnValidate()
    {
        if (_enemyInfo != enemyInfo)
        {
            _enemyInfo = enemyInfo;
            alreadyValidated = false;
        }

        if (alreadyValidated == false && enemyInfo != null)
        {
            currentHealth = enemyInfo.health;
            alreadyValidated = true;

            _enemyInfo = enemyInfo;
        }
    }

    public abstract void OnHit();
}
