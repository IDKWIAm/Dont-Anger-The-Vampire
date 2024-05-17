using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;

    private bool _textAppeared;
    private bool _isRunning;
    private float _timer;
    private bool _skip;

    private int _idx;

    private string[] _messages;
    private float _letterAppearingDelay;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (_textAppeared == true)
            {
                _idx++;
                _skip = false;
                if (_idx < _messages.Length)
                {
                    StartCoroutine(GradualTextAppearing(_messages[_idx], _letterAppearingDelay));
                    _textAppeared = false;
                }
                else
                {
                    _idx = 0;
                    CloseWindow();
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

    IEnumerator GradualTextAppearing(string message, float letterAppearingDelay)
    {
        _isRunning = true;

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

    public void DisplayMessage(string[] messages, float letterAppearingDelay)
    {
        _messages = messages;
        _letterAppearingDelay = letterAppearingDelay;

        StartCoroutine(GradualTextAppearing(messages[_idx], letterAppearingDelay));
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