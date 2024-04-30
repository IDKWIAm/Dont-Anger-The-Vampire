using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private GameObject falseWall; //Can be null

    private Animator _animator;
    private BoxCollider2D _collider;

    private void Start()
    {
        _animator = transform.parent.GetComponent<Animator>();
        _collider = transform.parent.GetComponent<BoxCollider2D>();

        if (falseWall != null) PlayerPrefs.SetInt("SecretsAmount", PlayerPrefs.GetInt("SecretsAmount") + 1);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _animator.SetTrigger("Triggered");
            
            if (falseWall != null)
            {
                falseWall.GetComponent<Animator>().SetTrigger("Triggered");
                if (_collider.enabled == true) PlayerPrefs.SetInt("secretsFound", PlayerPrefs.GetInt("secretsFound") + 1);
            }
            _collider.enabled = false;
        }
    }
}
