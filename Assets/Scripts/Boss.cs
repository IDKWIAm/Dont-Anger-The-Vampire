using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private float attacksDelay = 5f;

    [SerializeField] private GameObject boulder;
    [SerializeField] private GameObject spike;
    [SerializeField] private GameObject laserBeam;

    private float _timer;

    private GameObject[] _firstAttackPositions;
    private GameObject[] _secondAttackPositions;
    private GameObject[] _thirdAttackPositions;

    private void Start()
    {
        InitAttackPositions();
        _timer = attacksDelay;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0)
        {
            var randomAttack = Random.Range(1, 4);

            if (randomAttack == 1)
            {
                FirstAttack();
            } 
            else if (randomAttack == 2)
            {
                SecondAttack();
            }
            else if (randomAttack == 3)
            {
                ThirdAttack();
            }
            _timer = attacksDelay;
        }
    }

    private void InitAttackPositions()
    {
        _firstAttackPositions = GameObject.FindGameObjectsWithTag("First Attack Position");
        _secondAttackPositions = GameObject.FindGameObjectsWithTag("Second Attack Position");
        _thirdAttackPositions = GameObject.FindGameObjectsWithTag("Third Attack Position");
    }

    private void FirstAttack()
    {
        for (int i = 0; i < _firstAttackPositions.Length; i++)
        {
            Instantiate(boulder, _firstAttackPositions[i].transform.position, Quaternion.identity);
        }
    }

    private void SecondAttack()
    {
        for (int i = 0; i < _secondAttackPositions.Length; i++)
        {
            Instantiate(spike, _secondAttackPositions[i].transform.position, Quaternion.identity);
        }
    }

    private void ThirdAttack()
    {
        for (int i = 0; i < _thirdAttackPositions.Length; i++)
        {
            Instantiate(laserBeam, _thirdAttackPositions[i].transform.position, Quaternion.identity);
        }
    }
}
