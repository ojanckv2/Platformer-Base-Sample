using UnityEngine;

public class Trap_HitBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.OnPlayerHit();
        }
    }
}
