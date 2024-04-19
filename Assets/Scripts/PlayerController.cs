using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float maxFallingSpeed = 10f;
    [SerializeField] private float weaponShowTime = 0.1f;
    [SerializeField] private bool faceRight;
    [Space]
    [SerializeField] private bool doubleJumpAbility;
    [Space]
    [SerializeField] private bool dashAbility;
    [SerializeField] private float pressingInterval = 0.25f;
    [SerializeField] private float dashTime = 1.5f;
    [SerializeField] private float dashStrength = 8f;
    [SerializeField] private float dashCooldown = 1f;
    [Space]
    [SerializeField] private bool fastFallingAbility;
    [SerializeField] private float fastFallingMultiplier = 3f;
    [Space]
    [SerializeField] private Transform groundChecker;
    [SerializeField] private GameObject weapon;
    [SerializeField] private GameObject playerForm;
    [SerializeField] private GameObject batForm;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private DialogueManager dialogueManager;

    private bool _isMovingX;
    private bool _isMovingY;
    private bool _isFlying;
    private bool _doubleJumpPerformed;
    private bool _dashPerformed;
    private bool _dashRight;
    private bool _dashLeft;
    private bool _dPressed;
    private bool _aPressed;
    private float _timer;

    private Rigidbody2D _playerRigidbody;

    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        TimerUpdate();
        MoveUpdate();
        DashUpdate();
        JumpUpdate();
        ChangeFormUpdate();
        AttackUpdate();
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundChecker.position, 0.2f, groundLayer);
    }

    private void TimerUpdate()
    {
        if (_aPressed || _dPressed)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                _aPressed = false;
                _dPressed = false;
            }
        }
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

        if (!_isMovingX && !_dashRight && !_dashLeft)
        {
            _playerRigidbody.velocity = new Vector2(0, _playerRigidbody.velocity.y);
        }
    }

    private void JumpUpdate()
    {
        if (_isFlying) return;

        _playerRigidbody.gravityScale = 1;

        if (IsGrounded()) _doubleJumpPerformed = false;

        if (Input.GetKey(KeyCode.S) && fastFallingAbility)
        {
            _playerRigidbody.gravityScale = fastFallingMultiplier;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded())
            {
                _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, jumpForce);
            } 
            else if (doubleJumpAbility && !_doubleJumpPerformed)
            {
                _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, jumpForce);
                _doubleJumpPerformed = true;
            }
        }

        if (_playerRigidbody.velocity.y < -maxFallingSpeed)
        {
            _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, -maxFallingSpeed);
        }
    }

    private void AttackUpdate()
    {
        if (_isFlying) return;
        if (dialogueManager.IsRunning()) return;

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
            _doubleJumpPerformed = true;
        }
    }

    private void DashUpdate()
    {
        if (_isFlying) return;

        if (_dashRight)
        {
            _playerRigidbody.velocity = new Vector2(dashStrength, _playerRigidbody.velocity.y);
        } 
        else if (_dashLeft)
        {
            _playerRigidbody.velocity = new Vector2(-dashStrength, _playerRigidbody.velocity.y);
        }

        if (!dashAbility) return;

        if (IsGrounded()) _dashPerformed = false;

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (_aPressed && IsGrounded() || _aPressed && !_dashPerformed)
            {
                _aPressed = false;
                _dashPerformed = true;
                _timer = 0;
                _dashLeft = true;
                dashAbility = false;
                Invoke("DisableDashLeft", dashTime);
                Invoke("ActivateDash", dashTime + dashCooldown);
            }
            else
            {
                _aPressed = true;
                _timer = pressingInterval;
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (_dPressed && IsGrounded() || _dPressed && !_dashPerformed)
            {
                _dPressed = false;
                _dashPerformed = true;
                _timer = 0;
                _dashRight = true;
                dashAbility = false;
                Invoke("DisableDashRight", dashTime);
                Invoke("ActivateDash", dashTime + dashCooldown);
            }
            else
            {
                _dPressed = true;
                _timer = pressingInterval;
            }
        }
    }

    private void DisableDashRight()
    {
        _dashRight = false;
    }

    private void DisableDashLeft()
    {
        _dashLeft = false;
    }

    private void ActivateDash()
    {
        dashAbility = true;
    }
}
