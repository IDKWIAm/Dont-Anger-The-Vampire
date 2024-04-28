using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float jumpDistance = 1f;
    [SerializeField] private Transform player;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallChecker;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Transform abyssChecker;

    private bool _faceRight = true;

    private Rigidbody2D _rigitbody;

    private void Start()
    {
        _rigitbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MoveUpdate();
        JumpUpdate();
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
        return !Physics2D.OverlapArea(abyssChecker.position, abyssChecker.position - new Vector3(0.2f, 1.5f, 0));
    }

    private bool IsPlayerAbove()
    {
        return player.position.y > transform.position.y && Mathf.Abs(player.position.x - transform.position.x) < jumpDistance && Mathf.Abs(player.position.y - transform.position.y) > transform.localScale.y / 2;
    }


    private void MoveUpdate()
    {
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
        if (IsGrounded())
        {
            if (IsAbyss() || IsObstructed() || IsPlayerAbove())
            {
                _rigitbody.velocity = new Vector2(_rigitbody.velocity.x, jumpForce);
            }
        }
    }
}
