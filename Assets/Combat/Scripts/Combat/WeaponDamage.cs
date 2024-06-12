using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    enum Character
    {
        player,
        enemy
    }
    [SerializeField] private Character character;
    [SerializeField] private Statistic statistic;
    [SerializeField] private Collider myCollider;

    private int level;
    private int damageOutput;
    private int accuracy;
    private float knockback;

    private List<Collider> alreadyCollidedWith = new List<Collider>();

    private void OnEnable()
    {
        alreadyCollidedWith.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == myCollider) { return; }

        if (character == Character.enemy && other.CompareTag("Enemy")) return;

        if (alreadyCollidedWith.Contains(other)) { return; }

        alreadyCollidedWith.Add(other);

        if (other.TryGetComponent<Statistic>(out Statistic statistic))
        {
            statistic.TakeDamage(level, damageOutput, accuracy);
        }

        if (other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver))
        {
            Vector3 direction = (other.transform.position - myCollider.transform.position).normalized;
            forceReceiver.AddForce(direction * knockback);
        }
    }

    public void SetAttack(int level, int damage, int accuracy, float knockback)
    {
        damageOutput = DamageDealFormula(damage);
        this.level = level;
        this.accuracy = accuracy;
        this.knockback = knockback;
    }

    private int DamageDealFormula(int damagePercent)
    {
        int basedamage = statistic.statisticData.attack * damagePercent * (1 + statistic.statisticData.damageMitigation);
        return basedamage * (1 + statistic.statisticData.critRate / 100 * statistic.statisticData.critDMG);
    }
}
