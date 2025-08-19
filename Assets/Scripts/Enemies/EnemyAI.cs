using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float speed = 4f;
    [SerializeField] float jumpForce = 8f;
    [SerializeField] float jumpDistance = 1f;
    [SerializeField] float visionRange = 20f;
    [SerializeField] float KBResistacnce = 30f; //In percents
    [SerializeField] float turnDelayMax = 1.5f;
    [SerializeField] Transform groundChecker;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform wallChecker;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] Transform abyssChecker;
    [SerializeField] Animator animator;

    private bool _faceRight;
    private float _KBCounter;
    private float _turnDelay;
    private bool _movementAllowed;

    private Rigidbody2D _rigitbody;
    private Transform _player;

    private void Start()
    {
        _rigitbody = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        ApplyDifficulty();

        KBResistacnce = 1f - Mathf.Clamp(KBResistacnce / 100, 0, 1);
        if (transform.localScale.x > 0) _faceRight = true;
    }

    private void Update()
    {
        if (!OnSight() && !_movementAllowed) return;

        if (!_movementAllowed)
        {
            _movementAllowed = true;
            animator.SetBool("Spotted", true);
        }

        MoveUpdate();
        JumpUpdate();
    }

    private void ApplyDifficulty()
    {
        float difficultyMultiplyer = PlayerPrefs.GetFloat("DifficultyMultiplyer");

        if (difficultyMultiplyer == 1.25f) speed *= difficultyMultiplyer - 0.15f;
        if (difficultyMultiplyer == 0.75f) speed *= difficultyMultiplyer + 0.25f;
        else speed *= difficultyMultiplyer;
        KBResistacnce *= difficultyMultiplyer;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundChecker.position, 0.2f, groundLayer);
    }

    private bool IsObstructed()
    {
        return Physics2D.OverlapCircle(wallChecker.position, 0.2f, wallLayer);
    }

    private bool IsAbyss()
    {
        return !Physics2D.OverlapArea(abyssChecker.position, abyssChecker.position - new Vector3(0.2f, 1.5f, 0)) && !NeedToFall();
    }

    private bool IsPlayerAbove()
    {
        return _player.position.y > transform.position.y && Mathf.Abs(_player.position.x - transform.position.x) < jumpDistance && Mathf.Abs(_player.position.y - transform.position.y) > transform.localScale.y / 2;
    }

    private bool IsPlayerBelow()
    {
        return _player.position.y < transform.position.y && Mathf.Abs(_player.position.x - transform.position.x) < jumpDistance && Mathf.Abs(_player.position.y - transform.position.y) > transform.localScale.y / 2;
    }

    private bool NeedToFall()
    {
        return _player.position.y < transform.position.y && Mathf.Abs(_player.position.x - transform.position.x) < Mathf.Abs(_player.position.y - transform.position.y);
    }

    private bool OnSight()
    {
        LayerMask mask = ~0;
        mask &= ~(1 << 10);
        mask &= ~(1 << 12);
        mask &= ~(1 << 13);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, _player.position - transform.position, visionRange, mask);
        
        if (hit == true && (hit.collider.gameObject.tag == "Player Form"))
            return true;
        else
            return false;
    }


    private void MoveUpdate()
    {
        if (_KBCounter > 0)
        {
            _KBCounter -= Time.deltaTime;
            return;
        }

        //if (!OnSight())
        //{
        //    _rigitbody.velocity = new Vector2(0, _rigitbody.velocity.y);
        //    return;
        //}

        if (IsPlayerBelow()) gameObject.layer = 13;
        else gameObject.layer = 10;

        if (_player.position.x < transform.position.x)
        {
            if (_faceRight)
            {
                _turnDelay = Random.Range(0, turnDelayMax);
                Invoke("MirrorCharacter", _turnDelay);
                _faceRight = false;
            }
            if (_turnDelay > 0) _turnDelay -= Time.deltaTime;
            else _rigitbody.velocity = new Vector2(-speed, _rigitbody.velocity.y);
        }

        if (_player.position.x > transform.position.x)
        {
            if (!_faceRight)
            {
                _turnDelay = Random.Range(0, turnDelayMax);
                Invoke("MirrorCharacter", _turnDelay);
                _faceRight = true;
            }
            if (_turnDelay > 0) _turnDelay -= Time.deltaTime;
            else _rigitbody.velocity = new Vector2(speed, _rigitbody.velocity.y);
        }
    }

    private void MirrorCharacter()
    {
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void JumpUpdate()
    {
        //if (!OnSight()) return;

        if (IsGrounded())
        {
            if (IsAbyss() || IsObstructed() || IsPlayerAbove())
            {
                _rigitbody.velocity = new Vector2(_rigitbody.velocity.x, jumpForce);
            }
        }
    }

    public void Knock(float KBForce, float KBTime, bool knockFromRight)
    {
        _KBCounter = KBTime * KBResistacnce;

        if (knockFromRight) _rigitbody.velocity = new Vector2(-KBForce, KBForce) * KBResistacnce;
        else _rigitbody.velocity = new Vector2(KBForce, KBForce) * KBResistacnce;
    }
}
