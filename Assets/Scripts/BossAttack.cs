using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    [SerializeField] float lifeTime = 10f;
    [Space]
    [SerializeField] LookAt2D lookAt2D; //Can be null

    private bool _throw;

    void Start()
    {
        ApplyDifficulty();
        Invoke("destroy", lifeTime);
    }

    private void Update()
    {
        if (_throw) transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + Time.deltaTime * speed, transform.localPosition.z);
    }

    private void ApplyDifficulty()
    {
        float difficultyMultiplyer = PlayerPrefs.GetFloat("DifficultyMultiplyer");
        if (difficultyMultiplyer == 1.25f) speed *= difficultyMultiplyer;
    }

    private void destroy()
    {
        Destroy(gameObject);
    }

    public void Throw()
    {
        _throw = true;
    }

    public void StopLooking()
    {
        lookAt2D?.StopLooking();
    }
}
