using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;

    public bool Dead = false;

    public virtual void TakeDamage()
    {
        // Trigger death animation
        animator.SetTrigger("Die");

        // Destroy the enemy
        Destroy(this.gameObject, 2);
    }
}
