using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 4f;
    public float jumpForce = 8f;
    public float jumpDistance = 1f;
    public float damage = 5f;
    public float attackRange = 0.5f;
    public Transform player;
    public Transform groundChecker;
    public LayerMask groundLayer;

    private Rigidbody2D _rigitbody;
    private PlayerHealth _playerHealth;

    private void Start()
    {
        _rigitbody = GetComponent<Rigidbody2D>();
        _playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        MoveUpdate();
        JumpUpdate();
        AttackUpdate();
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

    private void AttackUpdate()
    {
        if (Mathf.Abs(player.position.x - transform.position.x) < attackRange && Mathf.Abs(player.position.y - transform.position.y) < transform.localScale.y / 2)
        {
            _playerHealth.DealDamage(damage * Time.deltaTime);
        }
    }
}
