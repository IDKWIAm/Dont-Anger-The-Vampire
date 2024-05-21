using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI messageText;
    [SerializeField] TextMeshProUGUI speakerNameText;

    private bool _textAppeared;
    private bool _isRunning;
    private float _timer;
    private bool _skip;

    private int _idx;

    private List<DialogueVariables> _messages;
    private float _letterAppearingDelay;

    [HideInInspector] public bool needAttack;
    private PrologueManager _prologueManager;
    private BossTrigger _bossTrigger;

    private void Start()
    {
        _prologueManager = GameObject.FindGameObjectWithTag("Prologue Manager")?.GetComponent<PrologueManager>();
        _bossTrigger = GameObject.FindGameObjectWithTag("Boss Trigger")?.GetComponent<BossTrigger>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (_textAppeared == true)
            {
                _idx++;
                _skip = false;
                if (_idx < _messages.Count)
                {
                    StartCoroutine(GradualTextAppearing(_messages[_idx].speakerName, _messages[_idx].speakerColor, _messages[_idx].message, _letterAppearingDelay));
                    _textAppeared = false;
                }
                else
                {
                    _idx = 0;
                    CloseWindow();
                    if (needAttack)
                    { 
                        _prologueManager?.EnemyAttack();
                        _bossTrigger?.AngerBoss();
                    }
                }
            }
            else
            {
                _skip = true;
            }
        }

        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
    }

    IEnumerator GradualTextAppearing(string speakerName, Color speakerColor, string message, float letterAppearingDelay)
    {
        _isRunning = true;

        speakerNameText.color = speakerColor;
        if (speakerName == "") speakerNameText.transform.parent.gameObject.SetActive(false);
        else 
        {
            speakerNameText.transform.parent.gameObject.SetActive(true);
            speakerNameText.text = speakerName; 
        }

        for (int i = 0; i < message.Length; i++)
        {
            if (!_skip)
            {
                messageText.text = message.Substring(0, i) + message[i];

                if (i > 0 && message[i - 1] == '.') yield return new WaitForSeconds(letterAppearingDelay * 5);

                yield return new WaitForSeconds(letterAppearingDelay);
            }
            else
            {
                messageText.text = message;
                break;
            }
        }
        
        _textAppeared = true;
    }

    public void DisplayMessage(List<DialogueVariables> messages, float letterAppearingDelay)
    {
        _messages = messages;
        _letterAppearingDelay = letterAppearingDelay;

        StartCoroutine(GradualTextAppearing(messages[_idx].speakerName, messages[_idx].speakerColor, messages[_idx].message, letterAppearingDelay));
    }

    public void OpenWindow()
    {
        gameObject.SetActive(true);
    }

    public void CloseWindow()
    {
        _skip = false;
        _textAppeared = false;
        _isRunning = false;
        gameObject.SetActive(false);
    }

    public bool IsRunning()
    {
        return _isRunning;
    }
}