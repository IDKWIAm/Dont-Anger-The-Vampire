using UnityEngine;

public class MessageSender : MonoBehaviour
{
    [SerializeField] bool sendOnContact;
    [SerializeField] string[] messages;
    [SerializeField] float letterAppearingDelay = 0.05f;
    [SerializeField] DialogueManager dialogueManager;

    private bool _collided;

    private BoxCollider2D _collider;

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _collided && !dialogueManager.IsRunning())
        {
            SendMessage();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            if (sendOnContact)
            {
                SendMessage();
            }
            else _collided = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            if (sendOnContact) _collider.enabled = false;
            else
            {
                _collided = false;
                dialogueManager.CloseWindow();
            }
        }
    }

    private void SendMessage()
    {
        dialogueManager.OpenWindow();
        dialogueManager.DisplayMessage(messages, letterAppearingDelay);
    }
}
