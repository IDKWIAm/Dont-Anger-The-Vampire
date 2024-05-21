using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject deathTexts;

    private void Update()
    {
        if (deathTexts.activeSelf == true) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Open();
        }
    }

    public void Open()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Close()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void ChangeVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
