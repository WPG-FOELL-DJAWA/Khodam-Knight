using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public PlayerSO playerSO { get; private set; }
    [field: SerializeField] public GameObject Khodam { get; private set; }
    [field: SerializeField] public Collider trigger { get; private set; }
    [field: SerializeField] public CharacterInput InputReader { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Animator KhodamAnimator { get; private set; }
    [field: SerializeField] public Targeter Targeter { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public WeaponDamage Weapon { get; private set; }
    [field: SerializeField] public WeaponDamage Skill { get; private set; }
    [field: SerializeField] public Statistic Statistic { get; private set; }
    [field: SerializeField] public Ragdoll Ragdoll { get; private set; }
    [field: SerializeField] public LedgeDetector LedgeDetector { get; private set; }
    [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }
    [field: SerializeField] public float TargetingMovementSpeed { get; private set; }
    [field: SerializeField] public float RotationDamping { get; private set; }
    [field: SerializeField] public float DodgeDuration { get; private set; }
    [field: SerializeField] public float DodgeLength { get; private set; }
    [field: SerializeField] public float JumpForce { get; private set; }
    [field: SerializeField] public Attack[] Attacks { get; private set; }
    [field: SerializeField] public Skill Skill1 { get; private set; }

    public float PreviousDodgeTime { get; private set; } = Mathf.NegativeInfinity;
    public Transform MainCameraTransform { get; private set; }

    private void Start()
    {
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;

        MainCameraTransform = Camera.main.transform;

        SwitchState(new PlayerFreeLookState(this));
        playerSO.SetScriptableObject(Statistic.statisticData);
    }

    private void OnEnable()
    {
        Statistic.OnTakeDamage += HandleTakeDamage;
        Statistic.OnDie += HandleDie;
    }

    private void OnDisable()
    {
        Statistic.OnTakeDamage -= HandleTakeDamage;
        Statistic.OnDie -= HandleDie;
    }

    private void HandleTakeDamage()
    {
        SwitchState(new PlayerImpactState(this));
    }

    private void HandleDie()
    {
        SwitchState(new PlayerDeadState(this));
    }
}
