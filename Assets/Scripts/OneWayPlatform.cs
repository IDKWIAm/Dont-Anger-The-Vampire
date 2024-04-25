using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    [SerializeField] Transform player;

    private BoxCollider2D _platformCollider;

    private void Start()
    {
        _platformCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (player.position.y > transform.position.y + transform.localScale.y / 2 + player.localScale.y / 2) _platformCollider.enabled = true;
        else _platformCollider.enabled = false;
    }
}
