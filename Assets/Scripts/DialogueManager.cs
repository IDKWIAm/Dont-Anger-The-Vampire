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

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (_textAppeared == true)
            {
                CloseWindow();
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

    public void DisplayMessage(string message, float letterAppearingDelay)
    {
        StartCoroutine(GradualTextAppearing(message, letterAppearingDelay));
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