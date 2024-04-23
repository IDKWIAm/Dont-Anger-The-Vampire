using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    [SerializeField] private RectTransform bossHealthbar;
    [SerializeField] private GameObject[] bossWalls;

    private float _maxHealth;

    private void Start()
    {
        _maxHealth = health;
    }

    public void DealDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            for (int i = 0; i < bossWalls.Length; i++)
            {
                bossHealthbar.transform.parent.gameObject.SetActive(false);
                bossWalls[i].SetActive(false);
            }

            Destroy(gameObject);
        }
        bossHealthbar.anchorMax = new Vector2(health / _maxHealth, bossHealthbar.anchorMax.y);
    }
}
