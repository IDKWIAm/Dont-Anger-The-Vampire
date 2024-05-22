using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    [SerializeField] GameObject boss;
    [SerializeField] GameObject bossWall; // Can be null
    [SerializeField] GameObject bossHealthbar;
    [SerializeField] AudioSource bossfightMusic;
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] PlayerController playerController;

    private AudioSource backgroundMusic;

    void Start()
    {
        backgroundMusic = GameObject.FindGameObjectWithTag("Background Music")?.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController _playerController))
        {
            playerController.DisableMovement();
            playerController.enabled = false;
            dialogueManager.needAttack = true;
        }
    }

    public void AngerBoss()
    {
        bossHealthbar.SetActive(true);
        boss.GetComponent<Boss>().enabled = true;
        boss.GetComponent<BossHealth>().enabled = true;
        boss.GetComponent<Animator>().SetBool("Anger", true);
        if (bossWall != null) bossWall.SetActive(true);
        playerController.enabled = true;
        backgroundMusic?.Stop();
        bossfightMusic?.Play();
        Destroy(gameObject);
    }
}
