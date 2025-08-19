using UnityEngine;

public class EnemySummoner : MonoBehaviour
{
    [SerializeField] GameObject _enemy;

    public void SummonEnemy()
    {
        Instantiate(_enemy, transform.position, Quaternion.identity);
        _enemy.GetComponent<EnemyHealth>().summoned = true;
    }

    public void destroy()
    {
        Destroy(gameObject);
    }
}
