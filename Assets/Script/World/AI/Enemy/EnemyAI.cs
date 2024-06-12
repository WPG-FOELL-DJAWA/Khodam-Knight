//////////////////////////////////////////////////////////////////////
//
//  Unity Source Code
//
//  File: EnemyAI.cs
//  Description: Script for enemy AI in roaming world
//
//  History:
//  - October 22, 2023: Created by Bhekti
//
//
//////////////////////////////////////////////////////////////////////


using System.Collections;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AudioSource))]

public class EnemyAI : MonoBehaviour
{

    [Header("Steering")]
    [Tooltip("Rotation speed")]
    [SerializeField] private float _angularSpeed = 120f;
    [Tooltip("Patrol walk speed")]
    [SerializeField] private float _patrolSpeed = 1.8f;
    [Tooltip("Chasing run speed")]
    [SerializeField] private float _chaseSpeed = 3.6f;
    [Tooltip("Patrol walk speed")]
    [SerializeField] private float _maxTimeOnWaiting = 5;
    [Tooltip("Time for waiting in stand location")]
    [SerializeField] private float _maxTimeOnChasing = 5;
    [Tooltip("Delay after attack target")]
    [SerializeField] private float _maxDelayAttack = 3;
    [Tooltip("Time AI for waiting")]
    [SerializeField] private float _maxTimeToWaiting = 15;

    [Tooltip("Stop distance from target")]
    // [Range(1.4f, 2.2f)]
    [Range(1.2f, 5)]
    public float stopDistance = 1.6f;
    public enum MoveCondition
    {
        Patrol, Chase, Wait

    }
    private MoveCondition _moveCondition;

    [Header("Patrol")]
    [SerializeField] private Transform[] _patrolPoint;

    [Header("FOV")]
    [Tooltip("Position Field Of View atau posisi pandangan AI biasanya di tempatkan di bagian kepala atau mata (isi terlebih dahulu agar melihat GUInya)")]
    public Transform PFOV;
    [Tooltip("jarak pandangan AI")]
    public float range = 5;

    [Tooltip("luas pandangan AI")]
    [Range(0, 360)]
    public float angle = 135;
    [Tooltip("objek yang mempunyai layer tersebut yang akan dikejar oleh AI")]
    [SerializeField] private LayerMask _playerMask;
    [Tooltip("objek yang menghalangi pandangan AI")]
    public LayerMask _obstructionMask;

    // [Header("Don't Operate this")]
    [HideInInspector] public Transform target;
    public bool isSeePlayer;
    public NavMeshAgent agent;
    public AudioSource audioSource;
    private float _currentDistance;

    private Vector3 _newTargetFaced;
    [SerializeField] private Animator _animator;
    [SerializeField] private CapsuleCollider _capsuleCollider;
    private int _index;
    private float _currentTimeOnWaiting, _currentTimeOnChasing, _currentTimeAttack, _currentTimeToWaiting;

    private bool isFirstAttack = true;

    private void Start()
    {
        _capsuleCollider.height = agent.height;
        _capsuleCollider.center = new Vector3(_capsuleCollider.center.x, 1 - agent.baseOffset, _capsuleCollider.center.z);
        _capsuleCollider.radius = agent.radius / 2;
        agent.autoBraking = false;
        agent.angularSpeed = 60f;

        _index = Random.Range(0, _patrolPoint.Length); //random _index titik patrol, agar AI memulai patrol tidak dititik patroli yang sama 
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.3f);
        while (true)
        {
            yield return wait;
            FieldOfView();
        }
    }
    private void FieldOfView()
    {
        Collider[] atCloseRange = Physics.OverlapSphere(transform.position, agent.radius + 0.7f, _playerMask); //collider ini hanya berlaku untuk objek yang memiliki playerMask

        if (atCloseRange.Length > 0)
        { //Jika collider di atas terkena objek yang memiliki layer = PlayerMask
            target = atCloseRange[0].transform; //ubah target menjadi objek yang terkena
            //SwitchMoveCondition(MoveCondition.Chase); //_moveCondition berubah menjadi mengejar (chase)
        }

        Collider[] rangeChecks = Physics.OverlapSphere(PFOV.position, range, _playerMask); //collider ini hanya berlaku untuk objek yang memiliki playerMask
        if (rangeChecks.Length > 0)
        {
            target = rangeChecks[0].transform; //variable target di isi dengan objek yang terkena radiasi atau range bulat ini
            Vector3 directionToTarget = (target.position - PFOV.position).normalized;
            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(PFOV.position, target.position);
                if (!Physics.Raycast(PFOV.position, directionToTarget, distanceToTarget, _obstructionMask))
                { //ketika AI melihat ke objek yang tidak memiliki layer ObstructionMask maka dia akan mengejar objek tersebut (atau player)
                    SwitchMoveCondition(MoveCondition.Chase);
                    isSeePlayer = true;
                }
                else
                    isSeePlayer = false;
            }
            else
                isSeePlayer = false;
        }
        else
            isSeePlayer = false;
    }

    private void Update()
    {
        switch (_moveCondition)
        {
            case MoveCondition.Patrol:
                Patroling();
                break;
            case MoveCondition.Wait:
                Waiting();
                break;
            case MoveCondition.Chase:
                Chasing();
                break;
        }

        // _capsuleCollider.height = agent.height;
        // _capsuleCollider.center = new Vector3(_capsuleCollider.center.x, 1 - agent.baseOffset, _capsuleCollider.center.z);
        // _capsuleCollider.radius = agent.radius / 2;
        // agent.autoBraking = false;
        // agent.angularSpeed = 60f;

        if (agent.hasPath)
            agent.acceleration = (agent.remainingDistance < stopDistance) ? 25 : 60;

        AnimationSystem();
        Rotation();

        //arah pathfinding
        for (int i = 0; i < agent.path.corners.Length - 1; i++)
        {
            Debug.DrawLine(agent.path.corners[i], agent.path.corners[i + 1], Color.green);
        }
    }

    private void Rotation()
    {
        //rotation
        //rotasinya tidak menggunakan dari NavMeshAgentnya karena ada sedikit bug, di NavMeshAgent ketika target dengan AI ini berjarak dekat, AI tidak akan menghadap target
        Vector3 lookAtTarget = new Vector3(_newTargetFaced.x - transform.position.x, 0, _newTargetFaced.z - transform.position.z);
        if (lookAtTarget != Vector3.zero)
        {
            var rotation = Quaternion.LookRotation(lookAtTarget);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, _angularSpeed * Time.deltaTime);
        }
        agent.updateRotation = false; //menonaktifkan rotasi dari NavMeshAgent
    }

    private void Chasing()
    {
        agent.stoppingDistance = 0.2f;
        agent.speed = _chaseSpeed;
        agent.destination = CalculatePositionWithDistance(transform.position, target.position, stopDistance);

        _currentDistance = Vector3.Distance(transform.position, target.position);

        if (_currentTimeOnChasing > _maxTimeOnChasing)
        {
            SwitchMoveCondition(MoveCondition.Wait);
        }

        bool isAttackDistance = _currentDistance < stopDistance + 0.6f;

        if (isAttackDistance)
            _newTargetFaced = target.position;
        else
            _newTargetFaced = agent.steeringTarget;

        if (isAttackDistance && isSeePlayer && ((_currentTimeAttack > _maxDelayAttack) || isFirstAttack))
        {
            _animator.SetTrigger("Attack");
            _currentTimeAttack = 0;
            isFirstAttack = false;
            // TurnBaseManager.instance.enterTurnBaseBattle(GetComponentInParent<EnemyParty>().getEnemyPartySO(), SceneManager.GetActiveScene().name, target.position); // noted : perlu rubah saat player benar - benar terkena serangan
        }

        //ini adalah waktu berhenti setelah melakukan serangan ke target 
        if (_currentTimeAttack < _maxDelayAttack)
            _currentTimeAttack += Time.deltaTime;

        if (!isSeePlayer)
            _currentTimeOnChasing += Time.deltaTime;
        else
            if (_currentTimeOnChasing > 0) _currentTimeOnChasing = 0;
    }

    private void Patroling()
    {
        agent.stoppingDistance = 1f;
        agent.speed = _patrolSpeed;
        agent.destination = CalculatePositionWithDistance(transform.position, _patrolPoint[_index].position, agent.stoppingDistance / 1.5f);

        _currentDistance = Vector3.Distance(transform.position, _patrolPoint[_index].position);

        _newTargetFaced = agent.steeringTarget;

        if (_currentDistance <= agent.stoppingDistance)
        { //jika jarak AI kurang dari jarak yang telah ditentukan, maka ubah nilai moveCondition menjadi wait
            SwitchMoveCondition(MoveCondition.Wait);
        }

        if (_currentTimeToWaiting > Random.Range(_maxTimeToWaiting / 1.25f, _maxTimeToWaiting)) //AI tidak akan patroli terus menerus. mereka akan berpindah ke waiting jika waktu patrol habis
            SwitchMoveCondition(MoveCondition.Wait); // noted perlu modif jangan menunggu tapi melihat ke kiri kanan
        else
            _currentTimeToWaiting += Time.deltaTime;
    }

    private void Waiting()
    {
        agent.destination = transform.position;

        if (_currentTimeOnWaiting > _maxTimeOnWaiting)
            SwitchMoveCondition(MoveCondition.Patrol);
        else
            _currentTimeOnWaiting += Time.deltaTime;
    }

    private void SwitchMoveCondition(MoveCondition newMoveCondition)
    {
        if (_moveCondition == newMoveCondition) return;

        _moveCondition = newMoveCondition;
        // Debug.Log("nilai _moveCondition berubah menjadi " + newMoveCondition.ToString());

        _index = Random.Range(0, _patrolPoint.Length);
        _currentTimeOnWaiting = _currentTimeOnChasing = _currentTimeToWaiting = 0;

    }
    float newMagnitude;
    private void AnimationSystem()
    {
        newMagnitude = Mathf.Lerp(newMagnitude, agent.velocity.magnitude, 5 * Time.deltaTime);
        _animator.SetFloat("Walk", newMagnitude);
    }

    //Audio ini digunakan dalam animation Event
    public void AudioEvent(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    //DamageTarget digunakan dalam animation event, clip attack
    public void DamageTarget(float damageAmount)
    {
        Debug.Log("Attacked Player: " + damageAmount.ToString());
    }

    //dibawah ini untuk membuat posisi dari gabungan antara stop distance, target, dan AI ini
    private Vector3 CalculatePositionWithDistance(Vector3 start, Vector3 end, float m_stopDistance)
    {
        Vector3 dir = (start - end).normalized;
        return end + dir * Mathf.Clamp(Vector3.Distance(start, end), 0, m_stopDistance);
    }

}
