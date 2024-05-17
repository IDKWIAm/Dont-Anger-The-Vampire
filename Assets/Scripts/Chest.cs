using UnityEngine;

public class Chest : MonoBehaviour
{
    private bool _collided;
    private bool _opened;

    private PlayerController _playerController;

    void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (_collided)
        {
            if (Input.GetKeyDown(KeyCode.E) && !_opened)
            {
                _playerController.weaponEnabled = true;
                _opened = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") _collided = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") _collided = false;
    }
}
