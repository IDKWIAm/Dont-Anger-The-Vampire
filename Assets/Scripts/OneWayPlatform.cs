using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private PlatformEffector2D _platformEffector;
    void Start()
    {
        _platformEffector = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            _platformEffector.useColliderMask = true;
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            _platformEffector.useColliderMask = false;
        }
    }
}
