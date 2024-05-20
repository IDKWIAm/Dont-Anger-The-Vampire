using UnityEngine;
public class LookAt2D : MonoBehaviour
{
    [SerializeField] bool onStart = true;

    private Transform target;

    private bool stopLooking;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        if (onStart && !stopLooking)
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90);
    }

    void Update()
    {
        if (!onStart && !stopLooking)
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90);
    }

    public void StopLooking()
    {
        stopLooking = true;
    }
}
