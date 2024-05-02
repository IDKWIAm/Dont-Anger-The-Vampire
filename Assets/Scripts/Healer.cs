using UnityEngine;

public class Healer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            playerHealth.Heal();
            Destroy(gameObject);
        }
    }
}
