using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LocalizedText : MonoBehaviour
{
    [SerializeField] string key;

    private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        Localize(key);
        LocalizationManager.OnLanguageChange += OnLanguageChange;
    }

    private void OnDestroy()
    {
        LocalizationManager.OnLanguageChange -= OnLanguageChange;
    }

    private void OnLanguageChange()
    {
        Localize(key);
    }

    private void Init()
    {
        key = text.text;
    }

    public void Localize(string newKey = null)
    {
        if (key == null)
            Init();

        if (key != null)
            key = newKey;

        text.text = LocalizationManager.GetTranslate(key);
    }
}
