using UnityEngine;

public class LimitedLifeTime : MonoBehaviour
{
    [SerializeField] float lifeTime;

    private void Start()
    {
        Invoke("destroy", lifeTime);
    }

    private void destroy()
    {
        Destroy(gameObject);
    }
}
