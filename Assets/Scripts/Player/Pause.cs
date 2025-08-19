using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject deathTexts;
    [SerializeField] Slider slider;

    private bool _opened;

    private void Start()
    {
        ChangeVolume(PlayerPrefs.GetFloat("Audio Volume"));
        slider.value = PlayerPrefs.GetFloat("Audio Volume");
    }

    private void Update()
    {
        if (deathTexts.activeSelf == true) return;

        if (Input.GetButtonDown("Pause"))
        {
            if (!_opened) Open();
            else Close();
        }
    }

    public void Open()
    {
        pauseMenu.SetActive(true);
        _opened = true;
        Time.timeScale = 0;
    }

    public void Close()
    {
        pauseMenu.SetActive(false);
        _opened = false;
        Time.timeScale = 1;
    }

    public void ChangeVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Audio Volume", volume);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
