using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    [SerializeField] GameObject fadeIn;
    [SerializeField] AudioSource doorOpeningClosingSound;
    [SerializeField] DialogueManager dialogueManager;

    [HideInInspector] public bool canExit;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            if (Input.GetButtonDown("Interact") && !dialogueManager.IsRunning())
            {
                if (canExit)
                {
                    doorOpeningClosingSound.PlayOneShot(doorOpeningClosingSound.clip);

                    PlayerPrefs.SetInt("Player health", playerHealth.health);
                    PlayerPrefs.SetInt("Player start max health", playerHealth.startMaxHealth);

                    PlayerPrefs.SetInt("collectorsPunished", PlayerPrefs.GetInt("collectorsPunished") + PlayerPrefs.GetInt("collectorsPunishedOnLevel"));
                    PlayerPrefs.SetInt("secretsAmount", PlayerPrefs.GetInt("secretsAmount") + PlayerPrefs.GetInt("secretsAmountOnLevel"));
                    PlayerPrefs.SetInt("secretsFound", PlayerPrefs.GetInt("secretsFound") + PlayerPrefs.GetInt("secretsFoundOnLevel"));

                    PlayerPrefs.SetInt("collectorsPunishedOnLevel", 0);
                    PlayerPrefs.SetInt("secretsAmountOnLevel", 0);
                    PlayerPrefs.SetInt("secretsFoundOnLevel", 0);

                    fadeIn.SetActive(true);
                }
                else
                {
                    GetComponent<MessageSender>()?.SendMessage();
                }
            }
        }
    }

    public void LoadNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex + 1 == SceneManager.sceneCountInBuildSettings) SceneManager.LoadScene(0);
        else SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
