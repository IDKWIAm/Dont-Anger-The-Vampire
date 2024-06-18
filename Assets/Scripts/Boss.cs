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
    [SerializeField] float extraFATime = 1f;
    [SerializeField] float extraSATime = 1f;
    [SerializeField] float extraTATime = 0f;

    [SerializeField] Transform ceiling;

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
    private int _secondAttacksStep = 1;
    private int _thirdAttacksStep = 1;
    private int _prevAttack = 0;

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
            var randomAttack = 0;

            if (_prevAttack == 1) randomAttack = Random.Range(2, 4);
            else randomAttack = Random.Range(1, 4);

            if (randomAttack < 1) randomAttack = 1;
            if (randomAttack > 3) randomAttack = 3;

            if (randomAttack == 1)
            {
                _prevAttack = 1;
                StartFirstAttack();
            } 
            else if (randomAttack == 2)
            {
                _prevAttack = 2;
                StartSecondAttack();
            }
            else if (randomAttack == 3)
            {
                _prevAttack = 3;
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
        yield return new WaitForSeconds(extraFATime);
        _attackInProcess = false;
    }

    IEnumerator SecondAttack()
    {
        for (int i = 0; i < _secondAttackPositions.Length; i += _secondAttacksStep)
        {
            Instantiate(AspenStakeAttack, _secondAttackPositions[i].transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnObjectsSACooldown);
        }
        yield return new WaitForSeconds(extraSATime);
        _attackInProcess = false;
    }

    IEnumerator ThirdAttack()
    {
        for (int i = 0; i < _thirdAttackPositions.Length; i += _thirdAttacksStep)
        {
            if (i >= _thirdAttackPositions.Length) i = _thirdAttackPositions.Length - 1;

            var rotationZ = Random.Range(0, 360);
            Transform parent = _thirdAttackPositions[0].transform.parent;
            parent.rotation = Quaternion.Euler(parent.rotation.x, parent.rotation.y, rotationZ);
            Instantiate(garlicAttack, _thirdAttackPositions[i].transform.position, parent.rotation);
            yield return new WaitForSeconds(spawnObjectsTACooldown);
        }
        yield return new WaitForSeconds(extraTATime);
        _attackInProcess = false;
    }

    private void ApplyDifficulty()
    {
        float difficultyMultiplyer = PlayerPrefs.GetFloat("DifficultyMultiplyer");

        extraFATime *= difficultyMultiplyer;
        extraSATime *= difficultyMultiplyer;
        extraTATime *= difficultyMultiplyer;

        if (difficultyMultiplyer == 1)
        {
            difficultyMultiplyer = 1;
            _secondAttacksStep = 2;
            _thirdAttacksStep = 2;
        }
        else if (difficultyMultiplyer == 0.75f)
        {
            difficultyMultiplyer = 1.25f;
            _secondAttacksStep = 2;
            _thirdAttacksStep = 3;
        }
        else if (difficultyMultiplyer == 1.25f)
        {
            difficultyMultiplyer = 0.75f;
            _secondAttacksStep = 1;
            _thirdAttacksStep = 1;
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
        if (ceiling == null) return;
        if (transform.position.y > ceiling.position.y) 
            transform.position = new Vector3(transform.position.x, ceiling.position.y - (transform.localScale.y / 3), transform.position.z);
    }
}
