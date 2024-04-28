using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private GameObject falseWall; //Can be null

    private Animator _animator;
    private BoxCollider2D _collider;

    private void Start()
    {
        _animator = transform.parent.GetComponent<Animator>();
        _collider = transform.parent.GetComponent<BoxCollider2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _animator.SetTrigger("Triggered");
            _collider.enabled = false;

            if (falseWall != null)
            {
                falseWall.GetComponent<Animator>().SetTrigger("Triggered");
            }
        }
    }
}
