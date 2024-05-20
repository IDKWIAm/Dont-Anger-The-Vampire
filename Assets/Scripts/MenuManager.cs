using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [Space]
    [SerializeField] private Transform mainMenu;
    [SerializeField] private Transform settingsMenu;
    [SerializeField] private Transform paintings;
    [SerializeField] private Transform playDistancing;
    [SerializeField] private GameObject fadeIn;

    private Transform _target;

    private void Start()
    {
        Time.timeScale = 1;
        var backgroundMusic = GameObject.FindGameObjectWithTag("Background Music");
        if (backgroundMusic != null) Destroy(backgroundMusic);

        float difficultyMultiplyer = PlayerPrefs.GetFloat("DifficultyMultiplyer");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat("DifficultyMultiplyer", difficultyMultiplyer);
        _target = settingsMenu;
        AudioListener.volume = 0.5f;
    }

    private void Update()
    {
        paintings.position = Vector3.Lerp(paintings.position, _target.position, Mathf.Clamp(Time.deltaTime * speed, 0, 1));
    }

    public void OpenMainMenu()
    {
        _target = settingsMenu;
    }

    public void OpenSettings()
    {
        _target = mainMenu;
    }

    public void ChangeDifficulty(int difficultyNumber)
    {
        if (difficultyNumber == 0) PlayerPrefs.SetFloat("DifficultyMultiplyer", 0.75f);
        if (difficultyNumber == 1) PlayerPrefs.SetFloat("DifficultyMultiplyer", 1);
        if (difficultyNumber == 2) PlayerPrefs.SetFloat("DifficultyMultiplyer", 1.25f);
    }

    public void StartGame()
    {
        speed /= 3;
        _target = playDistancing;
        fadeIn.SetActive(true);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ChangeVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
