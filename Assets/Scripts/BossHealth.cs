using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    [SerializeField] private GameObject[] bossWalls;

    public void DealDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            for (int i = 0; i < bossWalls.Length; i++)
            {
                bossWalls[i].SetActive(false);
            }

            Destroy(gameObject);
        }
    }
}
