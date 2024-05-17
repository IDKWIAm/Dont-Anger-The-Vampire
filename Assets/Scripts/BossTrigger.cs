using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    [SerializeField] Boss boss;
    [SerializeField] GameObject bossWall; // Can be null
    [SerializeField] GameObject bossHealthbar;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != 11) return;
        bossHealthbar.SetActive(true);
        boss.enabled = true;
        if (bossWall != null) bossWall.SetActive(true);
        Destroy(gameObject);
    }
}
