using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int health = 5;
    [SerializeField] private Image heartPoint;
    [SerializeField] private Transform parentCanvas;
    [SerializeField] private RectTransform heartsStartPos;

    private List<GameObject> hearts = new List<GameObject>();

    private int drawedHearts;

    private void Start()
    {
        health = maxHealth;
        for (int i = 0; i < maxHealth; i++)
        {
            CreateHeart();
        }
        CheckHealth();
    }

    private void CreateHeart()
    {
        Image heart = Instantiate(heartPoint, parentCanvas);
        heart.rectTransform.localPosition = new Vector2(heartsStartPos.localPosition.x + heartPoint.rectTransform.rect.width * 2 * drawedHearts, heartsStartPos.localPosition.y);
        hearts.Add(heart.gameObject);
        drawedHearts += 1;
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
        maxHealth += 1;
        health += 1;
        CreateHeart();
        CheckHealth();
    }

    public void DealDamage(int damage)
    {
        health -= damage;
        CheckHealth();
        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
