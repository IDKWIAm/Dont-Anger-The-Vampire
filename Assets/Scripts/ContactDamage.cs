using UnityEngine;

public class ContactDamage : MonoBehaviour
{
    [SerializeField] private float contactDamage = 20f;
    [SerializeField] private float KBForce = 10f;
    [SerializeField] private float KBTime = 0.5f;

    private bool _knockFromRight;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            playerHealth.DealDamage(contactDamage);

            if (playerHealth.gameObject.transform.position.x < transform.position.x) _knockFromRight = true;
            else _knockFromRight = false;
            playerHealth.gameObject.GetComponent<PlayerController>().Knock(KBForce, KBTime, _knockFromRight);
        }
    }
}
