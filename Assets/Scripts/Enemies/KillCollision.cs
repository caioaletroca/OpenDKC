using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCollision : MonoBehaviour
{
    public Enemy enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!enemy.Dead && collision.gameObject.tag == "Player")
        {
            // Make enemy die
            enemy.TakeDamage();

            // Effects on player
            collision.GetComponent<Player>().JumpEnemy();
        }
    }
}
