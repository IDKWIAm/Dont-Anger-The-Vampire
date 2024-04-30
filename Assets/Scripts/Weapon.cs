using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float damage = 10;
    [SerializeField] private float KBForce = 10f;
    [SerializeField] private float KBTime = 0.5f;

    private bool _knockFromRight;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth))
        {
            enemyHealth.DealDamage(damage);

            if (enemyHealth.gameObject.transform.position.x < transform.position.x - transform.localScale.x / 2) _knockFromRight = true;
            else _knockFromRight = false;
            enemyHealth.gameObject.GetComponent<EnemyAI>().Knock(KBForce, KBTime, _knockFromRight);
        }

        if (col.TryGetComponent<BossHealth>(out BossHealth bossHealth))
        {
            bossHealth.DealDamage(damage);
        }
    }
}
