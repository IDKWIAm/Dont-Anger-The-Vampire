using UnityEngine;

public class MessageSender : MonoBehaviour
{
    [SerializeField] private string message;
    [SerializeField] private float letterAppearingDelay = 0.1f;
    [SerializeField] private DialogueManager dialogueManager;

    private bool _collided;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _collided && !dialogueManager.IsRunning())
        {
            dialogueManager.OpenWindow();
            dialogueManager.DisplayMessage(message, letterAppearingDelay);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _collided = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _collided = false;
        dialogueManager.CloseWindow();
    }
}
