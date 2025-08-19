using UnityEngine;

public class ButtonAnimator : MonoBehaviour
{
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
        float difficultyMultiplyer = PlayerPrefs.GetFloat("DifficultyMultiplyer");
        if (difficultyMultiplyer == 0.75f)
        {
            if (gameObject.name == "Easy Difficulty") ChangeColor(1);
            else ChangeColor(0);
        }
        else if (difficultyMultiplyer == 1)
        {
            if (gameObject.name == "Normal Difficulty") ChangeColor(2);
            else ChangeColor(0);
        }
        else if (difficultyMultiplyer == 1.25f)
        {
            if (gameObject.name == "Hard Difficulty") ChangeColor(3);
            else ChangeColor(0);
        }
    }

    public void ChangeColor(int colorNumber)
    {
        _animator.SetInteger("colorNumber", colorNumber);
    }
}
