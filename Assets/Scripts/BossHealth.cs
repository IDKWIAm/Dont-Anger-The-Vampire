using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    [SerializeField] private RectTransform bossHealthbar;
    [SerializeField] private GameObject[] bossWalls;

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


    public void DealDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            bossHealthbar.transform.parent.gameObject.SetActive(false);
            for (int i = 0; i < bossWalls.Length; i++)
            {
                bossWalls[i].SetActive(false);
            }
            _exit.canExit = true;

            Destroy(gameObject);
        }
        bossHealthbar.anchorMax = new Vector2(health / _maxHealth, bossHealthbar.anchorMax.y);
    }
}
