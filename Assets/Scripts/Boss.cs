using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] float attacksDelay = 5f;
    [SerializeField] float speed = 3f;
    [SerializeField] Vector3 bossPositionOffset = new Vector3(0, 4, 0);
    [SerializeField] float stunTime = 3f;
    [SerializeField] int stunRate = 3;
    [Space]
    [SerializeField] float spawnObjectsFACooldown;
    [SerializeField] float spawnObjectsSACooldown;
    [SerializeField] float spawnObjectsTACooldown;

    [SerializeField] GameObject enemySummoner;
    [SerializeField] GameObject AspenStakeAttack;
    [SerializeField] GameObject garlicAttack;

    [SerializeField] GameObject[] _firstAttackPositions;
    [SerializeField] GameObject[] _secondAttackPositions;
    [SerializeField] GameObject[] _thirdAttackPositions;

    private bool _stunned;
    private bool _attackInProcess;
    private int _attacksCounter;
    private float _stunTimer;
    private float _cooldownTimer;
    private int secondAttacksStep = 1;
    private int thirdAttacksStep = 1;

    private Rigidbody2D _rigitbody;
    private Animator _animator;

    private Transform _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _rigitbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        ApplyDifficulty();
        _cooldownTimer = attacksDelay;
    }

    private void Update()
    {
        if (_stunTimer > 0) _stunTimer -= Time.deltaTime;
        else
        {
            _stunned = false;
            _animator.SetBool("Stunned", false);
            _rigitbody.bodyType = RigidbodyType2D.Static;
        }

        if (_stunned) return;

        _cooldownTimer -= Time.deltaTime;

        ChasePlayer();

        if (_cooldownTimer <= 0)
        {
            _attackInProcess = true;
            var randomAttack = Random.Range(1, 4);

            if (randomAttack == 1)
            {
                StartFirstAttack();
            } 
            else if (randomAttack == 2)
            {
                StartSecondAttack();
            }
            else if (randomAttack == 3)
            {
                StartThirdAttack();
            }
            _cooldownTimer = attacksDelay;
            _attacksCounter++;
        }

        if (_attacksCounter >= stunRate && !_attackInProcess)
        {
            Stun();
            _animator.SetBool("Stunned", true);
            _attacksCounter = 0;
        }
    }

    IEnumerator FirstAttack()
    {
        for (int i = 0; i < _firstAttackPositions.Length; i++)
        {
            Instantiate(enemySummoner, _firstAttackPositions[i].transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnObjectsFACooldown);
        }
        _attackInProcess = false;
    }

    IEnumerator SecondAttack()
    {
        for (int i = 0; i < _secondAttackPositions.Length; i += secondAttacksStep)
        {
            Instantiate(AspenStakeAttack, _secondAttackPositions[i].transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnObjectsSACooldown);
        }
        _attackInProcess = false;
    }

    IEnumerator ThirdAttack()
    {
        for (int i = 0; i < _thirdAttackPositions.Length; i += thirdAttacksStep)
        {
            if (i >= _thirdAttackPositions.Length) i = _thirdAttackPositions.Length - 1;

            var rotationZ = Random.Range(0, 360);
            Transform parent = _thirdAttackPositions[0].transform.parent;
            parent.rotation = Quaternion.Euler(parent.rotation.x, parent.rotation.y, rotationZ);
            Instantiate(garlicAttack, _thirdAttackPositions[i].transform.position, parent.rotation);
            yield return new WaitForSeconds(spawnObjectsTACooldown);
        }
        _attackInProcess = false;
    }

    private void ApplyDifficulty()
    {
        float difficultyMultiplyer = PlayerPrefs.GetFloat("DifficultyMultiplyer");

        if (difficultyMultiplyer == 1)
        {
            difficultyMultiplyer = 1;
            secondAttacksStep = 2;
            thirdAttacksStep = 2;
        }
        else if (difficultyMultiplyer == 0.75f)
        {
            difficultyMultiplyer = 1.25f;
            secondAttacksStep = 2;
            thirdAttacksStep = 3;
        }
        else if (difficultyMultiplyer == 1.25f)
        {
            difficultyMultiplyer = 0.75f;
            secondAttacksStep = 1;
            thirdAttacksStep = 1;
        }

        attacksDelay *= difficultyMultiplyer;
        stunTime *= difficultyMultiplyer;
    }

    private void ChasePlayer()
    {
        transform.position = Vector3.Lerp(transform.position, _player.position + bossPositionOffset, Time.deltaTime * speed);
    }

    private void StartFirstAttack()
    {
        StartCoroutine(FirstAttack());
    }

    private void StartSecondAttack()
    {
        StartCoroutine(SecondAttack());
    }

    private void StartThirdAttack()
    {
        StartCoroutine(ThirdAttack());
    }

    public void Stun()
    {
        _rigitbody.bodyType = RigidbodyType2D.Dynamic;
        _stunned = true;
        _stunTimer = stunTime;
    }
}
