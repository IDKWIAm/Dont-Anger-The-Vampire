using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] RectTransform bossHealthbar;
    [SerializeField] GameObject[] bossWalls;
    [SerializeField] AudioSource bossFightMusic;
    [SerializeField] AudioSource bossHitSound;
    [SerializeField] Exit exit;

    private float _maxHealth;
    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

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
        exit.canExit = true;
        bossFightMusic.Stop();

        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        _rigidbody2D.velocity = new Vector2(0, 12);
        GetComponent<Animator>()?.SetTrigger("Death");
        GetComponent<Boss>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        Invoke("destroy", 3);
    }

    private void destroy()
    {
        Destroy(gameObject);
    }

    public void DealDamage(float damage)
    {
        health -= damage;

        bossHitSound.PlayOneShot(bossHitSound.clip);

        if (health <= 0)
        {
            Death();
        }
        bossHealthbar.anchorMax = new Vector2(health / _maxHealth, bossHealthbar.anchorMax.y);
    }
}
