using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] Sprite openedChestSprite;
    [SerializeField] AudioSource chestOpeningSound;
    [SerializeField] GameObject movementTip;
    [SerializeField] GameObject interactionTip;
    [SerializeField] GameObject flyTip;
    [SerializeField] GameObject fallTip;
    [SerializeField] GameObject attackTip;
    [SerializeField] GameObject dashTip;

    private bool _collided;

    private SpriteRenderer _chestSprite;

    private PlayerController _playerController;
    private PrologueManager _prologueManager;
    private Exit _exit;

    void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _prologueManager = GameObject.FindGameObjectWithTag("Prologue Manager").GetComponent<PrologueManager>();
        _exit = GameObject.FindGameObjectWithTag("Exit").GetComponent<Exit>();
        _chestSprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (_collided)
        {
            if (Input.GetKeyDown(KeyCode.E) && _playerController.weaponEnabled == false)
            {
                _playerController.weaponEnabled = true;
                _exit.canExit = true;
                _prologueManager.EnableEnemy();
                if (movementTip != null) movementTip.SetActive(false);
                if (interactionTip != null) interactionTip.SetActive(false);
                if (flyTip != null) flyTip.SetActive(false);
                if (fallTip != null) fallTip.SetActive(false);
                if (attackTip != null) attackTip.SetActive(true);
                if (dashTip != null) dashTip.SetActive(false);
                _chestSprite.sprite = openedChestSprite;
                chestOpeningSound.PlayOneShot(chestOpeningSound.clip);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") _collided = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") _collided = false;
    }
}
