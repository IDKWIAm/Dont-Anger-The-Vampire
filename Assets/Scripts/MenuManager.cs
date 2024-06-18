using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [Space]
    [SerializeField] Transform mainMenu;
    [SerializeField] Transform settingsMenu;
    [SerializeField] Transform paintings;
    [SerializeField] Transform playDistancing;
    [SerializeField] GameObject fadeIn;
    [SerializeField] Slider slider;

    private Transform _target;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("DifficultyMultiplyer")) PlayerPrefs.SetFloat("DifficultyMultiplyer", 1);
        if (!PlayerPrefs.HasKey("Audio Volume")) PlayerPrefs.SetFloat("Audio Volume", 0.5f);
        
        float difficultyMultiplyer = PlayerPrefs.GetFloat("DifficultyMultiplyer");
        float audioVolume = PlayerPrefs.GetFloat("Audio Volume");
        string language = PlayerPrefs.GetString("Language");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat("DifficultyMultiplyer", difficultyMultiplyer);
        PlayerPrefs.SetFloat("Audio Volume", audioVolume);
        PlayerPrefs.SetString("Language", language);
    }

    private void Start()
    {
        Time.timeScale = 1;
        var backgroundMusic = GameObject.FindGameObjectWithTag("Background Music");
        if (backgroundMusic != null) Destroy(backgroundMusic);

        _target = settingsMenu;
        ChangeVolume(PlayerPrefs.GetFloat("Audio Volume"));
        slider.value = PlayerPrefs.GetFloat("Audio Volume");
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
        PlayerPrefs.SetFloat("Audio Volume", volume);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
