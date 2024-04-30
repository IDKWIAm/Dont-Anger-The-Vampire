using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float health = 100f;

    public void DealDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            PlayerPrefs.SetInt("collectorsPunished", PlayerPrefs.GetInt("collectorsPunished") + 1);
            Destroy(gameObject);
        }
    }
}
