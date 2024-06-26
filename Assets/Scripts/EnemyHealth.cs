﻿using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float health = 7f;
    [SerializeField] AudioSource hurtSound;

    public bool summoned;
    public float summonedSelfDestroy = 15f;

    private EnemyCounter _enemyCounter;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        if (!summoned)
        {
            _enemyCounter = GameObject.FindGameObjectWithTag("Enemy Counter")?.GetComponent<EnemyCounter>();
            _enemyCounter?.AddEnemy();
        }
        ApplyDifficulty();

        if (summoned)
        {
            health /= 2;
            if (summonedSelfDestroy > 100) return;
            Invoke("Death", summonedSelfDestroy);
        }
    }

    private void ApplyDifficulty()
    {
        float difficultyMultiplyer = PlayerPrefs.GetFloat("DifficultyMultiplyer");
        if (difficultyMultiplyer == 0) difficultyMultiplyer = 1;

        health *= difficultyMultiplyer;
    }

    private void Death()
    {
        if (!summoned) _enemyCounter?.AddCount();
        PlayerPrefs.SetInt("collectorsPunishedOnLevel", PlayerPrefs.GetInt("collectorsPunishedOnLevel") + 1);
        _animator.SetTrigger("Death");
        GetComponent<EnemyAI>().enabled = false;
        GetComponent<ContactDamage>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        Invoke("destroy", 3);
    }

    public void destroy()
    {
        Destroy(gameObject);
    }

    public void DealDamage(float damage)
    {
        health -= damage;

        _animator.SetTrigger("Hit");

        hurtSound.PlayOneShot(hurtSound.clip);

        if (health <= 0)
        {
            Death();
        }
    }
}
