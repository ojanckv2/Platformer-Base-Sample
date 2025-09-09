using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemies/Enemy Info")]
public class EnemyInfo : ScriptableObject
{
    public string enemyName;
    public int health;
    public float speed;
    public int damage;
    public float deadAnimationTime;
}