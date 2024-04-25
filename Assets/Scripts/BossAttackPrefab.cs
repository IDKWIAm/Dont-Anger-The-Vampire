using UnityEngine;

public class BossAttackPrefab : MonoBehaviour
{
    [SerializeField] private float attackDelay = 1f;
    [SerializeField] private float selfDestroyTimer = 5f;

    [SerializeField] private GameObject warning;
    [SerializeField] private GameObject attack;

    private void Start()
    {
        warning.SetActive(true);
        Invoke("Attack", attackDelay);
        Invoke("SelfDestroy", selfDestroyTimer);
    }

    private void Attack()
    {
        warning.SetActive(false);
        attack.SetActive(true);
    }

    private void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
