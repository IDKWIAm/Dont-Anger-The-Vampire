using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [Space]
    [SerializeField] private Transform mainMenu;
    [SerializeField] private Transform settingsMenu;
    [SerializeField] private Transform paintings;

    private Transform _target;

    private void Start()
    {
        Time.timeScale = 1;
        PlayerPrefs.DeleteAll();
        _target = settingsMenu;
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

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
