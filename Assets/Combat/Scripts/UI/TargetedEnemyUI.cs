using UnityEngine;
using UnityEngine.UI;

public class TargetedEnemyUI : MonoBehaviour
{
    [SerializeField] private GenericText _enemyName;
    [SerializeField] private Slider _enemyHealthSlider;

    private Statistic _enemyStatistic;

    public void EnemyIsTargeted(Statistic enemyStatistic)
    {
        _enemyStatistic = enemyStatistic;
        _enemyName.SetText(_enemyStatistic.statisticData.name);
        _enemyHealthSlider.maxValue = _enemyStatistic.statisticData.health;
        _enemyHealthSlider.value = _enemyStatistic.currentHealth;
        gameObject.SetActive(true);
    }

    private void LateUpdate()
    {
        if (_enemyStatistic)
        {
            _enemyHealthSlider.value = _enemyStatistic.currentHealth;
            if (_enemyStatistic.IsDead) removeEnemyTargeted();
        }
    }

    public void removeEnemyTargeted()
    {
        _enemyStatistic = null;
        gameObject.SetActive(false);
    }

}
