using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    [SerializeField] GameObject boss;
    [SerializeField] GameObject bossWall; // Can be null
    [SerializeField] GameObject bossHealthbar;
    [SerializeField] AudioSource bossfightMusic;

    private AudioSource backgroundMusic;

    void Start()
    {
        backgroundMusic = GameObject.FindGameObjectWithTag("Background Music")?.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != 11) return;
        bossHealthbar.SetActive(true);
        boss.GetComponent<Boss>().enabled = true;
        boss.GetComponent<BossHealth>().enabled = true;
        if (bossWall != null) bossWall.SetActive(true);
        backgroundMusic?.Stop();
        bossfightMusic?.Play();
        Destroy(gameObject);
    }
}
