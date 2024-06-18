using System.Collections.Generic;
using UnityEngine;

public class MessageSender : MonoBehaviour
{
    [SerializeField] bool sendOnContact;
    [SerializeField] bool sendOnce;
    [SerializeField] float letterAppearingDelay = 0.04f;
    [SerializeField] List<DialogueVariables> messages;
    [SerializeField] DialogueManager dialogueManager;

    private bool _collided;

    private BoxCollider2D _collider;

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        
        for (int idx = 0; idx < messages.Count; idx++)
            LocalizeMessage(idx, messages[idx].speakerNameKey, messages[idx].messageKey);

        LocalizationManager.OnLanguageChange += OnLanguageChange;
    }

    void OnDestroy()
    {
        LocalizationManager.OnLanguageChange -= OnLanguageChange;
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

    private void OnLanguageChange()
    {
        for (int idx = 0; idx < messages.Count; idx++)
            LocalizeMessage(idx, messages[idx].speakerNameKey, messages[idx].messageKey);
    }

    private void Init(int idx)
    {
        messages[idx].speakerNameKey = messages[idx].message;
        messages[idx].messageKey = messages[idx].message;
    }

    public void LocalizeMessage(int messageIdx, string newSpeakerNameKey = null, string newMessageKey = null)
    {
        if (messages[messageIdx].speakerNameKey == null || messages[messageIdx].messageKey == null)
            Init(messageIdx);

        if (messages[messageIdx].speakerNameKey != null && messages[messageIdx].messageKey != null)
        {
            messages[messageIdx].speakerNameKey = newSpeakerNameKey;
            messages[messageIdx].messageKey = newMessageKey;
        }
        messages[messageIdx].speakerName = LocalizationManager.GetTranslate(messages[messageIdx].speakerNameKey);
        messages[messageIdx].message = LocalizationManager.GetTranslate(messages[messageIdx].messageKey);
    }
}
