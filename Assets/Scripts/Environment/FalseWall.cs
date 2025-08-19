using UnityEngine;

public class FalseWall : MonoBehaviour
{
    bool triggered;

    Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();

        PlayerPrefs.SetInt("secretsAmountOnLevel", PlayerPrefs.GetInt("secretsAmountOnLevel") + 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController playerController) && !triggered)
        {
            _animator.SetTrigger("Triggered");

            PlayerPrefs.SetInt("secretsFoundOnLevel", PlayerPrefs.GetInt("secretsFoundOnLevel") + 1);

            triggered = true;
        }
    }
}
