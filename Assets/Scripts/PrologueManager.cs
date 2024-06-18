using UnityEngine;

public class PrologueManager : MonoBehaviour
{
    [SerializeField] VampireFlair vampireFlair;
    [SerializeField] GameObject chest;
    [SerializeField] GameObject bottle;
    [SerializeField] GameObject collector;
    [SerializeField] DialogueManager dialogueManager;

    private BoxCollider2D _collider;
    private PlayerController _playerController;

    private void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _collider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            _playerController.DisableMovement();
            _playerController.enabled = false;
            dialogueManager.needAttack = true;
        }
    }

    public void EnableEnemy()
    {
        collector.SetActive(true);
        _collider.enabled = true;
    }

    public void EnableChest()
    {
        chest.GetComponent<BoxCollider2D>().enabled = true;
        vampireFlair.AddObject(chest);
    }

    public void EnemyAttack()
    {
        collector.GetComponent<EnemyAI>().enabled = true;
        collector.GetComponent<EnemyHealth>().enabled = true;
        collector.GetComponent<Animator>().enabled = true;
        _playerController.enabled = true;
    }
}
