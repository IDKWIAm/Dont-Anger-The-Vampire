using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    [SerializeField] Exit _exit;

    private int _enemiesKilledCount;
    private int _enemiesAmount;

    private void Update()
    {
        if (_enemiesKilledCount == _enemiesAmount)
        {
            _exit.canExit = true;
        }
    }

    public void AddCount()
    {
        _enemiesKilledCount += 1;
    }

    public void AddEnemy()
    {
        _enemiesAmount += 1;
    }
}
