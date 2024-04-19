using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float damage = 10;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth))
        {
            enemyHealth.DealDamage(damage);
        }

        if (col.TryGetComponent<BossHealth>(out BossHealth bossHealth))
        {
            bossHealth.DealDamage(damage);
        }
    }
}
