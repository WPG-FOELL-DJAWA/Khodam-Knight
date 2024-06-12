using UnityEngine;

public class CombatUIHandler : MonoBehaviour
{
    [SerializeField] private HealthMana _healthMana;
    [SerializeField] private TargetedEnemyUI _targetedEnemyUI;
    private GameObject _player;
    private Statistic _statistic;

    private void OnEnable()
    {
        setUI();
    }

    public void refresh() => setUI();

    private void setUI()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _statistic = _player.GetComponent<Statistic>();

        _healthMana.updateHealth(_statistic);
    }

    public void setTargetedEnemy(EnemyTarget enemyTarget) => _targetedEnemyUI.EnemyIsTargeted(enemyTarget.statistic);

    public void removeTargetedEnemy() => _targetedEnemyUI.removeEnemyTargeted();
}
