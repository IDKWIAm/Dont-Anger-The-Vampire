using UnityEngine;

public class Bottle : MonoBehaviour
{
    private bool _collided;

    private PrologueManager _prologueManager;

    private void Start()
    {
        _prologueManager = GameObject.FindGameObjectWithTag("Prologue Manager").GetComponent<PrologueManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _collided)
        {
            _prologueManager.EnableChest();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _collided = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _collided = false;
        }
    }
}
