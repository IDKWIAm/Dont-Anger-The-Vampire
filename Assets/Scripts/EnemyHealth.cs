﻿using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float health = 100f;

    private Exit _exit;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _exit = GameObject.FindGameObjectWithTag("Exit").GetComponent<Exit>();

        _exit.AddEnemy();
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
        _exit.AddCount();
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
