using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health = 5;
    [SerializeField] int startMaxHealth = 5;
    [SerializeField] int maxHealth = 12;
    [SerializeField] int heartsInRow = 6;

    [SerializeField] GameObject deathBG;
    [SerializeField] GameObject deathText;
    [SerializeField] SpriteRenderer vampireForm;
    [SerializeField] Image heartPoint;
    [SerializeField] Transform parentCanvas;
    [SerializeField] RectTransform heartsStartPos;

    private List<GameObject> hearts = new List<GameObject>();

    private int drawedHeartsInRow;
    private int currentRow;

    private Animator _animator;


    private void Start()
    {
        Time.timeScale = 1;
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

    private void Death()
    {
        PlayerPrefs.SetInt("collectorsPunishedOnLevel", 0);
        PlayerPrefs.SetInt("secretsAmountOnLevel", 0);
        PlayerPrefs.SetInt("secretsFoundOnLevel", 0);

        deathText.SetActive(true);
        deathBG.SetActive(true);
        foreach (GameObject heart in hearts)
        {
            heart.SetActive(false);
        }
        vampireForm.sortingOrder = 3;

        Time.timeScale = 0;
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
            Death();
        }
    }
}
