using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 4f;
    public float jumpForce = 7f;
    public float maxFallingSpeed = 10f;
    public float weaponShowTime = 0.1f;
    public bool faceRight;

    public bool fastFalling;
    public float fastFallingMultiplier = 3f;

    public Transform groundChecker;
    public GameObject weapon;
    public GameObject playerForm;
    public GameObject batForm;
    public LayerMask groundLayer;

    private bool _isMovingX;
    private bool _isMovingY;
    private bool _isFlying;

    private Rigidbody2D _playerRigidbody;

    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MoveUpdate();
        JumpUpdate();
        ChangeFormUpdate();
        AttackUpdate();
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundChecker.position, 0.2f, groundLayer);
    }

    private void MoveUpdate()
    {
        _isMovingX = false;

        if (Input.GetKey(KeyCode.A))
        {
            _playerRigidbody.velocity = new Vector2(-speed, _playerRigidbody.velocity.y);
            _isMovingX = true;
            if (faceRight)
            {
                var scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
                faceRight = false;
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            _playerRigidbody.velocity = new Vector2(speed, _playerRigidbody.velocity.y);
            _isMovingX = true;
            if (!faceRight)
            {
                var scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
                faceRight = true;
            }
        }

        if (_isFlying)
        {
            _isMovingY = false;

            if (Input.GetKey(KeyCode.W))
            {
                _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, speed);
                _isMovingY = true;
            }
            if (Input.GetKey(KeyCode.S))
            {
                _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, -speed);
                _isMovingY = true;
            }

            if (!_isMovingY)
            {
                _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, 0);
            }
        }

        if (!_isMovingX)
        {
            _playerRigidbody.velocity = new Vector2(0, _playerRigidbody.velocity.y);
        }
    }

    private void JumpUpdate()
    {
        if (_isFlying) return;

        _playerRigidbody.gravityScale = 1;

        if (Input.GetKey(KeyCode.S) && fastFalling)
        {
            _playerRigidbody.gravityScale = fastFallingMultiplier;
        }
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, jumpForce);
        }

        if (_playerRigidbody.velocity.y < -maxFallingSpeed)
        {
            _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, -maxFallingSpeed);
        }
    }

    private void AttackUpdate()
    {
        if (_isFlying) return;

        if (Input.GetMouseButtonDown(0))
        {
            weapon.SetActive(true);
            Invoke("DisableWeapon", weaponShowTime);
        }
    }

    private void DisableWeapon()
    {
        weapon.SetActive(false);
    }

    private void ChangeFormUpdate()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            weapon.SetActive(false);
            batForm.SetActive(true);
            playerForm.SetActive(false);
            _playerRigidbody.gravityScale = 0;
            _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, 0);
            _isFlying = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            batForm.SetActive(false);
            playerForm.SetActive(true);
            _playerRigidbody.gravityScale = 1;
            _isFlying = false;
        }
    }
}
