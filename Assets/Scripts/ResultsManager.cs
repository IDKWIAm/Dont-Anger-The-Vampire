using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultsManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI collectorsPunishedValue;
    [SerializeField] private TextMeshProUGUI secretsFoundValue;

    private void Start()
    {
        collectorsPunishedValue.text = PlayerPrefs.GetInt("collectorsPunished").ToString();
        secretsFoundValue.text = PlayerPrefs.GetInt("secretsFound").ToString() + " / " + PlayerPrefs.GetInt("secretsAmount");
    }

    public void BackToMainManu()
    {
        SceneManager.LoadScene(0);
    }
}
