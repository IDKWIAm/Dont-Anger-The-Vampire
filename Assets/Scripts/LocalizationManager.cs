using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using TMPro;

public class LocalizationManager : MonoBehaviour
{
    public static int SelectedLanguage { get; private set; }

    public static event LanguageChangeHandler OnLanguageChange;

    public delegate void LanguageChangeHandler();

    private static Dictionary<string, List<string>> localization;

    [SerializeField] TMP_Dropdown languageDropdown;
    [SerializeField] TextAsset textFile;

    private void Awake()
    {
        if (localization == null) 
            LoadLocalization();

        if (!PlayerPrefs.HasKey("Language"))
        {
            PlayerPrefs.SetString("Language", Application.systemLanguage.ToString());
        }

        if (PlayerPrefs.GetString("Language") == "English")
            SelectedLanguage = 0;
        if (PlayerPrefs.GetString("Language") == "Russian")
            SelectedLanguage = 1;
    }

    private void Start()
    {
        if (languageDropdown != null) languageDropdown.value = SelectedLanguage;
    }

    public void SetLanguage(int id)
    {
        SelectedLanguage = id;
        if (id == 0)
            PlayerPrefs.SetString("Language", "English");
        if (id == 1)
            PlayerPrefs.SetString("Language", "Russian");
        OnLanguageChange?.Invoke();
    }

    private void LoadLocalization()
    {
        localization = new Dictionary<string, List<string>>();

        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(textFile.text);

        foreach (XmlNode key in xmlDocument["Keys"].ChildNodes)
        {
            string keyStr = key.Attributes["Name"].Value;

            var values = new List<string>();

            foreach (XmlNode translate in key["Translates"].ChildNodes)
                values.Add(translate.InnerText);

            localization[keyStr] = values;
        }
    }

    public static string GetTranslate(string key, int languageId = -1)
    {
        if (languageId == -1)
            languageId = SelectedLanguage;

        if (localization.ContainsKey(key))
            return localization[key][languageId];

        return key;
    }
}
