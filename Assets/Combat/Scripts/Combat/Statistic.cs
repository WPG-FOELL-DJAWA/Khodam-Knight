using System;
using UnityEngine;

public class Statistic : MonoBehaviour
{
    public StatisticData statisticData;
    public int currentHealth => health;

    private int health;
    private bool isInvulnerable;

    public event Action OnTakeDamage;
    public event Action OnDie;

    public bool IsDead => health == 0;

    private void Start()
    {
        health = statisticData.health;
    }

    public void SetInvulnerable(bool isInvulnerable)
    {
        this.isInvulnerable = isInvulnerable;
    }

    public void TakeDamage(int enemyLevel, int damage, int enemyAccuracy)
    {
        if (health == 0) { return; }

        if (isInvulnerable) { return; }

        if (Evasion(enemyLevel, enemyAccuracy)) return;

        int damageInput = DamageTakenFormula(damage);
        health = Mathf.Max(health - damageInput, 0);

        OnTakeDamage?.Invoke();

        if (health == 0)
        {
            OnDie?.Invoke();
        }
    }

    private bool Evasion(int enemyLevel, int enemyAccuracy)
    {
        float baseEvasion = statisticData.evasion / statisticData.constant * (1 + statisticData.resistanceMitigation);
        float evasionChange = baseEvasion * (statisticData.level / enemyLevel) / (1 + enemyAccuracy / baseEvasion + 100);

        int randomValue = UnityEngine.Random.Range(0, 101);

        if (randomValue < evasionChange) return true;
        return false;
    }
    private int DamageTakenFormula(int damageOutput)
    {
        return damageOutput - (statisticData.defense * (1 + statisticData.defenseMitigation));
    }
}
