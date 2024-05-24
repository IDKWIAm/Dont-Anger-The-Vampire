using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    [SerializeField] GameObject fadeIn;
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] AudioSource doorOpeningClosingSound;

    [SerializeField] float letterAppearingDelay = 0.05f;
    [SerializeField] List<DialogueVariables> cantExitMessages;

    [HideInInspector] public bool canExit;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
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
                dialogueManager.OpenWindow();
                dialogueManager.DisplayMessage(cantExitMessages, letterAppearingDelay);
            }
        }
    }

    public void LoadNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex + 1 == SceneManager.sceneCountInBuildSettings) SceneManager.LoadScene(0);
        else SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
