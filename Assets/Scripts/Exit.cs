using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    private int enemiesKilledCount;
    private int _enemiesAmount;

    public void AddCount()
    {
        enemiesKilledCount += 1;
    }

    public void AddEnemy()
    {
        _enemiesAmount += 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (enemiesKilledCount == _enemiesAmount)
        {
            PlayerPrefs.SetInt("collectorsPunished", PlayerPrefs.GetInt("collectorsPunished") + PlayerPrefs.GetInt("collectorsPunishedOnLevel"));
            PlayerPrefs.SetInt("secretsAmount", PlayerPrefs.GetInt("secretsAmount") + PlayerPrefs.GetInt("secretsAmountOnLevel"));
            PlayerPrefs.SetInt("secretsFound", PlayerPrefs.GetInt("secretsFound") + PlayerPrefs.GetInt("secretsFoundOnLevel"));

            PlayerPrefs.SetInt("collectorsPunishedOnLevel", 0);
            PlayerPrefs.SetInt("SecretsAmountOnLevel", 0);
            PlayerPrefs.SetInt("secretsFoundOnLevel", 0);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
