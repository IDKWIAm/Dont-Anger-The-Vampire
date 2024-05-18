using System.Collections.Generic;
using UnityEngine;

public class MessageSender : MonoBehaviour
{
    [SerializeField] bool sendOnContact;
    [SerializeField] bool sendOnce;
    [SerializeField] float letterAppearingDelay = 0.05f;
    [SerializeField] List<DialogueVariables> messages;
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
        if (collision.tag == "Player")
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
        if (collision.tag == "Player")
        {
            if (sendOnContact && sendOnce) _collider.enabled = false;
            else
            {
                _collided = false;
                //dialogueManager.CloseWindow();
            }
        }
    }

    private void SendMessage()
    {
        dialogueManager.OpenWindow();
        dialogueManager.DisplayMessage(messages, letterAppearingDelay);
        if (sendOnce) _collider.enabled = false;
    }
}
