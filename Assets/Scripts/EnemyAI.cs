using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float jumpDistance = 1f;
    [SerializeField] private Transform player;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private LayerMask groundLayer;

    private bool _knockFromRight;

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


    private void MoveUpdate()
    {
        if (player.position.x < transform.position.x)
        {
            _rigitbody.velocity = new Vector2(-speed, _rigitbody.velocity.y);
        }
        if (player.position.x > transform.position.x)
        {
            _rigitbody.velocity = new Vector2(speed, _rigitbody.velocity.y);
        }
    }

    private void JumpUpdate()
    {
        if (IsGrounded() && player.position.y > transform.position.y && Mathf.Abs(player.position.x - transform.position.x) < jumpDistance && Mathf.Abs(player.position.y - transform.position.y) > transform.localScale.y / 2)
        {
            _rigitbody.velocity = new Vector2(_rigitbody.velocity.x, jumpForce);
        }
    }
}
