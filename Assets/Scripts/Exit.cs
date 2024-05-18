using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    [SerializeField] GameObject fadeIn;

    [HideInInspector] public bool canExit;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController playerController) && canExit)
        {
            PlayerPrefs.SetInt("collectorsPunished", PlayerPrefs.GetInt("collectorsPunished") + PlayerPrefs.GetInt("collectorsPunishedOnLevel"));
            PlayerPrefs.SetInt("secretsAmount", PlayerPrefs.GetInt("secretsAmount") + PlayerPrefs.GetInt("secretsAmountOnLevel"));
            PlayerPrefs.SetInt("secretsFound", PlayerPrefs.GetInt("secretsFound") + PlayerPrefs.GetInt("secretsFoundOnLevel"));

            PlayerPrefs.SetInt("collectorsPunishedOnLevel", 0);
            PlayerPrefs.SetInt("secretsAmountOnLevel", 0);
            PlayerPrefs.SetInt("secretsFoundOnLevel", 0);

            fadeIn.SetActive(true);
        }
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
