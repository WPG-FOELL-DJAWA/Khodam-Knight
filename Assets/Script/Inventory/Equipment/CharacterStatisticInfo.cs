using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterStatisticInfo : MonoBehaviour
{
    [SerializeField] private StatisticSO statisticSO;
    [SerializeField] private TextMeshProUGUI level;
    [SerializeField] private TextMeshProUGUI damage;
    [SerializeField] private TextMeshProUGUI defense;
    [SerializeField] private TextMeshProUGUI healthPoint;
    [SerializeField] private TextMeshProUGUI manaPoint;
    [SerializeField] private TextMeshProUGUI evasion;
    [SerializeField] private TextMeshProUGUI accuracy;
    [SerializeField] private TextMeshProUGUI criticalRate;
    [SerializeField] private TextMeshProUGUI criticalDamage;

    private void Start()
    {
        SetAllStat();
    }

    public void SetAllStat()
    {
        level.text = statisticSO.level.ToString();
        damage.text = statisticSO.attackMod.ToString();
        defense.text = statisticSO.defenseMod.ToString();
        healthPoint.text = statisticSO.healthMod.ToString();
        manaPoint.text = statisticSO.manaMod.ToString();
        evasion.text = statisticSO.resistanceMod.ToString();
        accuracy.text = statisticSO.accuracyMod.ToString();
        criticalRate.text = statisticSO.critRateMod.ToString();
        criticalDamage.text = statisticSO.critDMGMod.ToString();
    }
}
