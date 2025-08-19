using UnityEngine;

public class Bottle : MonoBehaviour
{
    [SerializeField] AudioSource bottleSound;

    private bool _collided;

    private PrologueManager _prologueManager;

    private void Start()
    {
        _prologueManager = GameObject.FindGameObjectWithTag("Prologue Manager").GetComponent<PrologueManager>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && _collided)
        {
            _prologueManager.EnableChest();
            bottleSound.PlayOneShot(bottleSound.clip);
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
