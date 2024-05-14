using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health = 5;
    [SerializeField] private int startMaxHealth = 5;
    [SerializeField] private int maxHealth = 12;
    [SerializeField] private int heartsInRow = 6;
    
    [SerializeField] private Image heartPoint;
    [SerializeField] private Transform parentCanvas;
    [SerializeField] private RectTransform heartsStartPos;

    private List<GameObject> hearts = new List<GameObject>();

    private int drawedHeartsInRow;
    private int currentRow;

    private Animator _animator;


    private void Start()
    {
        _animator = transform.GetChild(0).GetComponent<Animator>();

        health = startMaxHealth;
        for (int i = 0; i < startMaxHealth; i++)
        {
            CreateHeart();
        }
        CheckHealth();
    }

    private void CreateHeart()
    {
        Image heart = Instantiate(heartPoint, parentCanvas);
        if (startMaxHealth <= heartsInRow) heart.rectTransform.localPosition = new Vector2(heartsStartPos.localPosition.x + heartPoint.rectTransform.rect.width * 2 * drawedHeartsInRow, heartsStartPos.localPosition.y);
        else heart.rectTransform.localPosition = new Vector2(heartsStartPos.localPosition.x + heartPoint.rectTransform.rect.width * 2 * drawedHeartsInRow, heartsStartPos.localPosition.y - heartPoint.rectTransform.rect.height * 2 * currentRow);
        hearts.Add(heart.gameObject);
        drawedHeartsInRow += 1;
        if (drawedHeartsInRow == heartsInRow)
        {
            drawedHeartsInRow -= heartsInRow;
            currentRow++;
        }
    }

    private void CheckHealth()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i > health - 1)
            {
                hearts[i].transform.GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                hearts[i].transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    public void HealthUp()
    {
        if (startMaxHealth < maxHealth)
        {
            startMaxHealth += 1;
            CreateHeart();
        }
        Heal();
    }

    public void Heal()
    {
        if (health == startMaxHealth) return;

        health += 1;
        CheckHealth();
    }

    public void DealDamage(int damage)
    {
        health -= damage;
        CheckHealth();
        _animator.SetTrigger("Hit");

        if (health <= 0)
        {
            PlayerPrefs.SetInt("collectorsPunishedOnLevel", 0);
            PlayerPrefs.SetInt("SecretsAmountOnLevel", 0);
            PlayerPrefs.SetInt("secretsFoundOnLevel", 0);

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
