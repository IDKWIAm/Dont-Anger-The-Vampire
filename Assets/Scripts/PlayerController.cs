using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 7f;
    [SerializeField] float flySpeed = 8f;
    [SerializeField] float flyingTime = 3f;
    [SerializeField] float coyoteTime = 0.1f;
    [SerializeField] float maxFallingSpeed = 10f;
    [SerializeField] public bool weaponEnabled = true;
    [SerializeField] float weaponShowTime = 0.1f;
    [SerializeField] float attackCooldown = 0.08f;

    [Header("Double Jump")]
    [SerializeField] bool doubleJumpEnabled;

    [Header("Dash")]
    [SerializeField] bool dashEnabled;
    [SerializeField] float pressingInterval = 0.25f;
    [SerializeField] float dashTime = 1.5f;
    [SerializeField] float dashStrength = 8f;
    [SerializeField] float dashCooldown = 1f;

    [Header("Fast Falling")]
    [SerializeField] bool fastFallingEnabled;
    [SerializeField] float fastFallingMultiplier = 3f;

    [Space]

    [SerializeField] Transform groundChecker;
    [SerializeField] Transform additionalGroundChecker;
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject vampireForm;
    [SerializeField] GameObject batForm;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] AudioSource swingSound;

    private float _baseSpeed;

    private bool _faceRight;

    private bool _isMovingX;
    private bool _isMovingY;
    private bool _isFlying;

    private bool _doubleJumpPerformed;
    private bool _dashPerformed;
    private bool _flyAbilityPerformed;

    private bool _dashRight;
    private bool _dashLeft;
    private bool _dPressed;
    private bool _aPressed;

    private float _dashIntervalTimer;
    [HideInInspector] public float _KBCounter;
    private float _coyoteCounter;
    private float _flyingTimer;
    private float _attackTimer;

    private float _defaultGravity;

    private Rigidbody2D _playerRigitbody;
    private Transform _flyBarValue;
    private Animator _vampireFormAnimator;

    private void Start()
    {
        _playerRigitbody = GetComponent<Rigidbody2D>();
        _vampireFormAnimator = vampireForm.GetComponent<Animator>();

        _defaultGravity = _playerRigitbody.gravityScale;
        _baseSpeed = speed;
        _flyBarValue = batForm.transform.GetChild(0);
        if (transform.localScale.x > 0) _faceRight = true;
    }

    void Update()
    {
        if (Time.timeScale == 0) return;
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

    private bool CoyoteJumpEnable()
    {
        return _coyoteCounter > 0;
    }
    

    private void TimerUpdate()
    {
        if (_aPressed || _dPressed)
        {
            _dashIntervalTimer -= Time.deltaTime;
            if (_dashIntervalTimer <= 0)
            {
                _aPressed = false;
                _dPressed = false;
            }
        }
        if (_attackTimer > 0) _attackTimer -= Time.deltaTime;
        if (_coyoteCounter > 0) _coyoteCounter -= Time.deltaTime;
        if (_flyingTimer > 0) _flyingTimer -= Time.deltaTime;
    }

    private void MoveUpdate()
    {
        _isMovingX = false;
        _vampireFormAnimator.SetBool("IsRunning", false);

        if (_KBCounter > 0)
        {
            _KBCounter -= Time.deltaTime;
            return;
        }

        if (Input.GetKey(KeyCode.A))
        {
            _playerRigitbody.velocity = new Vector2(-speed, _playerRigitbody.velocity.y);
            _isMovingX = true;
            if (_faceRight)
            {
                MirrorCharacter();
                _faceRight = false;
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            _playerRigitbody.velocity = new Vector2(speed, _playerRigitbody.velocity.y);
            _isMovingX = true;
            if (!_faceRight)
            {
                MirrorCharacter();
                _faceRight = true;
            }
        }

        if (_isFlying)
        {
            _flyBarValue.localScale = new Vector2(_flyingTimer / flyingTime, _flyBarValue.localScale.y);
            _isMovingY = false;

            if (Input.GetKey(KeyCode.W))
            {
                _playerRigitbody.velocity = new Vector2(_playerRigitbody.velocity.x, speed);
                _isMovingY = true;
            }
            if (Input.GetKey(KeyCode.S))
            {
                _playerRigitbody.velocity = new Vector2(_playerRigitbody.velocity.x, -speed);
                _isMovingY = true;
            }

            if (!_isMovingY)
            {
                _playerRigitbody.velocity = new Vector2(_playerRigitbody.velocity.x, 0);
            }
        }

        if (!_isMovingX && !_dashRight && !_dashLeft)
        {
            _playerRigitbody.velocity = new Vector2(0, _playerRigitbody.velocity.y);
        }

        if (_isMovingX && !_isFlying) _vampireFormAnimator.SetBool("IsRunning", true);
    }

    private void MirrorCharacter()
    {
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void JumpUpdate()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            vampireForm.layer = 13;
            batForm.layer = 13;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            vampireForm.layer = 11;
            batForm.layer = 11;
        }

        if (_isFlying) return;

        _playerRigitbody.gravityScale = _defaultGravity;

        if (IsGrounded())
        {
            _coyoteCounter = coyoteTime;
            _doubleJumpPerformed = false;
        }

        if (Input.GetKey(KeyCode.S) && fastFallingEnabled)
        {
            _playerRigitbody.gravityScale *= fastFallingMultiplier;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded() || CoyoteJumpEnable())
            {
                _playerRigitbody.velocity = new Vector2(_playerRigitbody.velocity.x, jumpForce);
            } 
            else if (doubleJumpEnabled && !_doubleJumpPerformed)
            {
                _playerRigitbody.velocity = new Vector2(_playerRigitbody.velocity.x, jumpForce);
                _doubleJumpPerformed = true;
            }
        }

        if (_playerRigitbody.velocity.y < -maxFallingSpeed)
        {
            _playerRigitbody.velocity = new Vector2(_playerRigitbody.velocity.x, -maxFallingSpeed);
        }
    }

    private void AttackUpdate()
    {
        if (_isFlying) return;
        //if (dialogueManager.IsRunning()) return;
        if (Time.timeScale == 0) return;
        if (weaponEnabled == false) return;

        if (Input.GetMouseButtonDown(0) && _attackTimer <= 0)
        {
            weapon.SetActive(true);
            Invoke("DisableWeapon", weaponShowTime);
            swingSound.PlayOneShot(swingSound.clip);
            _attackTimer = attackCooldown;
        }
    }

    private void DisableWeapon()
    {
        weapon.SetActive(false);
    }

    private void ChangeFormUpdate()
    {
        if (IsGrounded()) _flyAbilityPerformed = false;

        if (Input.GetKeyDown(KeyCode.LeftShift) && !_flyAbilityPerformed)
        {
            _flyingTimer = flyingTime;
            weapon.SetActive(false);
            batForm.SetActive(true);
            vampireForm.SetActive(false);
            _playerRigitbody.gravityScale = 0;
            speed = flySpeed;
            _playerRigitbody.velocity = new Vector2(_playerRigitbody.velocity.x, 0);
            _isFlying = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || _flyingTimer <= 0 && _isFlying)
        {
            batForm.SetActive(false);
            vampireForm.SetActive(true);
            _playerRigitbody.gravityScale = 1;
            speed = _baseSpeed;
            _isFlying = false;
            _doubleJumpPerformed = true;
            _flyAbilityPerformed = true;
            vampireForm.layer = 11;

            if (additionalGroundChecker == null) return;
            if (Physics2D.OverlapCircle(additionalGroundChecker.position, 0.1f, groundLayer)) 
                transform.position += Vector3.up * (transform.localScale.y / 2);
        }
    }

    private void DashUpdate()
    {
        if (_isFlying) return;

        if (_dashRight)
        {
            _playerRigitbody.velocity = new Vector2(dashStrength, _playerRigitbody.velocity.y);
        } 
        else if (_dashLeft)
        {
            _playerRigitbody.velocity = new Vector2(-dashStrength, _playerRigitbody.velocity.y);
        }

        if (!dashEnabled) return;

        if (IsGrounded() || CoyoteJumpEnable()) _dashPerformed = false;

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (_aPressed)
            {
                if (IsGrounded() || CoyoteJumpEnable() || !_dashPerformed)
                {
                    _aPressed = false;
                    _dashPerformed = true;
                    _dashIntervalTimer = 0;
                    _dashLeft = true;
                    dashEnabled = false;
                    Invoke("DisableDashLeft", dashTime);
                    Invoke("ActivateDash", dashTime + dashCooldown);
                    _vampireFormAnimator.SetTrigger("Dash");
                }
            }
            _aPressed = true;
            _dPressed = false;
            _dashIntervalTimer = pressingInterval;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (_dPressed)
            {
                if (IsGrounded() || CoyoteJumpEnable() || !_dashPerformed)
                {
                    _dPressed = false;
                    _dashPerformed = true;
                    _dashIntervalTimer = 0;
                    _dashRight = true;
                    dashEnabled = false;
                    Invoke("DisableDashRight", dashTime);
                    Invoke("ActivateDash", dashTime + dashCooldown);
                    _vampireFormAnimator.SetTrigger("Dash");
                }
            }
            _dPressed = true;
            _aPressed = false;
            _dashIntervalTimer = pressingInterval;
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
        dashEnabled = true;
    }

    public void Knock(float KBForce, float KBTime, bool knockFromRight)
    {
        if (_KBCounter > 0) return;

        DisableDashLeft();
        DisableDashRight();
        _KBCounter = KBTime;
        
        if (knockFromRight) _playerRigitbody.velocity = new Vector2(-KBForce, KBForce);
        else _playerRigitbody.velocity = new Vector2(KBForce, KBForce);
    }

    public void DisableMovement()
    {
        _vampireFormAnimator.SetBool("IsRunning", false);
        _playerRigitbody.velocity = new Vector2(0, 0);
    }
}
