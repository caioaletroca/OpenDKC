using UnityEngine;

public class DeathCollision : MonoBehaviour
{
    public Enemy enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!enemy.Dead && collision.gameObject.tag == "Player")
        {
            var player = collision.gameObject.GetComponent<KongController>();

            //if (player.Attack)
            //    enemy.TakeDamage();
            //else
            //    player.TakeDamage();
        }
            
    }
}
