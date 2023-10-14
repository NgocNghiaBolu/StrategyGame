using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    public int healthEnemy = 5;
    [Tooltip("Adds amount to maxHitPoints when Enemy dies.")]
    public int difficultyRamp = 1;
    public int presentHealth = 0;

    Enemy enemy;

    void OnEnable()
    {
        presentHealth = healthEnemy;
    }

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    private void OnParticleCollision(GameObject other)
    {
        TakeDamage();
    }

    public void TakeDamage()
    {
        presentHealth--;
        if(presentHealth <= 0)
        {
            gameObject.SetActive(false);
            enemy.RewardCoin();
        }
    }
}
