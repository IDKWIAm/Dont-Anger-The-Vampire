using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] RectTransform bossHealthbar;
    [SerializeField] GameObject[] bossWalls;
    [SerializeField] AudioSource bossFightMusic;

    private float _maxHealth;

    private Exit _exit;

    private void Start()
    {
        _exit = GameObject.FindGameObjectWithTag("Exit").GetComponent<Exit>();

        ApplyDifficulty();
        _maxHealth = health;
    }

    private void ApplyDifficulty()
    {
        float difficultyMultiplyer = PlayerPrefs.GetFloat("DifficultyMultiplyer");
        if (difficultyMultiplyer == 0) difficultyMultiplyer = 1;

        health *= difficultyMultiplyer;
    }

    private void Death()
    {
        bossHealthbar.transform.parent.gameObject.SetActive(false);
        for (int i = 0; i < bossWalls.Length; i++)
        {
            bossWalls[i].SetActive(false);
        }
        _exit.canExit = true;

        bossFightMusic.Stop();

        Destroy(gameObject);
    }


    public void DealDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Death();
        }
        bossHealthbar.anchorMax = new Vector2(health / _maxHealth, bossHealthbar.anchorMax.y);
    }
}
