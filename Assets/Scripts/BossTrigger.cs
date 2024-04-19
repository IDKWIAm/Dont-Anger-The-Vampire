using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    [SerializeField] private GameObject boss;
    [Space]
    [SerializeField] private GameObject bossWall;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        boss.SetActive(true);
        if (bossWall != null) bossWall.SetActive(true);
    }
}
