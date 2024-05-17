﻿using UnityEngine;

public class MessageSender : MonoBehaviour
{
    [SerializeField] string[] messages;
    [SerializeField] float letterAppearingDelay = 0.1f;
    [SerializeField] DialogueManager dialogueManager;

    private bool _collided;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _collided && !dialogueManager.IsRunning())
        {
            dialogueManager.OpenWindow();
            dialogueManager.DisplayMessage(messages, letterAppearingDelay);
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
