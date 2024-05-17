using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private float attacksDelay = 5f;

    [SerializeField] GameObject boulder;
    [SerializeField] GameObject spike;
    [SerializeField] GameObject laserBeam;

    [SerializeField] GameObject[] _firstAttackPositions;
    [SerializeField] GameObject[] _secondAttackPositions;
    [SerializeField] GameObject[] _thirdAttackPositions;

    private float _timer;

    private void Start()
    {
        ApplyDifficulty();
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

    private void ApplyDifficulty()
    {
        float difficultyMultiplyer = PlayerPrefs.GetFloat("DifficultyMultiplyer");
        if (difficultyMultiplyer == 0) difficultyMultiplyer = 1;
        if (difficultyMultiplyer == 0.75f) difficultyMultiplyer = 1.25f;
        if (difficultyMultiplyer == 1.25f) difficultyMultiplyer = 0.75f;

        attacksDelay *= difficultyMultiplyer;
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
