using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float speed = 4f;
    [SerializeField] float jumpForce = 8f;
    [SerializeField] float jumpDistance = 1f;
    [SerializeField] float visionRange = 20f;
    [SerializeField] float KBResistacnce = 30f; //In percents
    [SerializeField] Transform groundChecker;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform wallChecker;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] Transform abyssChecker;

    private bool _faceRight;
    private float _KBCounter;

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
        MoveUpdate();
        JumpUpdate();
    }

    private void ApplyDifficulty()
    {
        float difficultyMultiplyer = PlayerPrefs.GetFloat("DifficultyMultiplyer");

        if (difficultyMultiplyer > 1) speed *= difficultyMultiplyer - 0.15f;
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


    private void MoveUpdate()
    {
        if (Vector3.Distance(transform.position, _player.position) > visionRange) return;

        if (IsPlayerBelow()) gameObject.layer = 13;
        else gameObject.layer = 10;

        if (_KBCounter > 0)
        {
            _KBCounter -= Time.deltaTime;
            return;
        }

        if (_player.position.x < transform.position.x)
        {
            _rigitbody.velocity = new Vector2(-speed, _rigitbody.velocity.y);
            if (_faceRight)
            {
                MirrorCharacter();
                _faceRight = false;
            }
        }
        if (_player.position.x > transform.position.x)
        {
            _rigitbody.velocity = new Vector2(speed, _rigitbody.velocity.y);
            if (!_faceRight)
            {
                MirrorCharacter();
                _faceRight = true;
            }
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
        if (Vector3.Distance(transform.position, _player.position) > visionRange) return;

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
