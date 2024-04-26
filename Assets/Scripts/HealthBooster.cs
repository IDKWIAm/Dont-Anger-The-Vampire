using UnityEngine;

public class HealthBooster : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            playerHealth.HealthUp();
            Destroy(gameObject);
        }
    }
}
