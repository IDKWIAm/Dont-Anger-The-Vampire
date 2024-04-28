using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject bossWall;
    [SerializeField] private GameObject bossHealthbar;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bossHealthbar.SetActive(true);
        boss.SetActive(true);
        if (bossWall != null) bossWall.SetActive(true);
        Destroy(gameObject);
    }
}
