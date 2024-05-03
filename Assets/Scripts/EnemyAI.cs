using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float jumpDistance = 1f;
    [SerializeField] private float visionRange = 20f;
    [SerializeField] private float KBResistacnce = 30f; //In percents
    [SerializeField] private Transform player;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallChecker;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Transform abyssChecker;

    private bool _faceRight = true;
    private float _KBCounter;

    private Rigidbody2D _rigitbody;

    private void Start()
    {
        _rigitbody = GetComponent<Rigidbody2D>();

        ApplyDifficulty();

        KBResistacnce = 1f - Mathf.Clamp(KBResistacnce / 100, 0, 1);
    }

    private void Update()
    {
        MoveUpdate();
        JumpUpdate();
    }

    private void ApplyDifficulty()
    {
        float difficultyMultiplyer = PlayerPrefs.GetFloat("DifficultyMultiplyer");
        if (difficultyMultiplyer == 0) difficultyMultiplyer = 1;

        speed *= difficultyMultiplyer;
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
        return player.position.y > transform.position.y && Mathf.Abs(player.position.x - transform.position.x) < jumpDistance && Mathf.Abs(player.position.y - transform.position.y) > transform.localScale.y / 2;
    }

    private bool NeedToFall()
    {
        return player.position.y < transform.position.y && Mathf.Abs(player.position.x - transform.position.x) < Mathf.Abs(player.position.y - transform.position.y);
    }


    private void MoveUpdate()
    {
        if (Vector3.Distance(transform.position, player.position) > visionRange) return;

        if (_KBCounter > 0)
        {
            _KBCounter -= Time.deltaTime;
            return;
        }

        if (player.position.x < transform.position.x)
        {
            _rigitbody.velocity = new Vector2(-speed, _rigitbody.velocity.y);
            if (_faceRight)
            {
                MirrorCharacter();
                _faceRight = false;
            }
        }
        if (player.position.x > transform.position.x)
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
        if (Vector3.Distance(transform.position, player.position) > visionRange) return;

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
