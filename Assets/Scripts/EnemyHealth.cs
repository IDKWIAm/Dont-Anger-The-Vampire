using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float health = 7f;

    private EnemyCounter _enemyCounter;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _enemyCounter = GameObject.FindGameObjectWithTag("Enemy Counter").GetComponent<EnemyCounter>();

        _enemyCounter.AddEnemy();
        ApplyDifficulty();
    }

    private void ApplyDifficulty()
    {
        float difficultyMultiplyer = PlayerPrefs.GetFloat("DifficultyMultiplyer");
        if (difficultyMultiplyer == 0) difficultyMultiplyer = 1;

        health *= difficultyMultiplyer;
    }

    private void Death()
    {
        _enemyCounter.AddCount();
        PlayerPrefs.SetInt("collectorsPunishedOnLevel", PlayerPrefs.GetInt("collectorsPunishedOnLevel") + 1);
        Destroy(gameObject);
    }

    public void DealDamage(float damage)
    {
        health -= damage;

        _animator.SetTrigger("Hit");

        if (health <= 0)
        {
            Death();
        }
    }
}
