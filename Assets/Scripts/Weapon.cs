using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage = 10;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth))
        {
            enemyHealth.DealDamage(damage);
        }
    }
}
