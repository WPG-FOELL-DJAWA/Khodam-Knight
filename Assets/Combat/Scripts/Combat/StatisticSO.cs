using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "StatisticSO", menuName = "Bhekti/StatisticSO")]
public class StatisticSO : ScriptableObject
{
    public int level;
    public int healthMod = 100;
    public int attackMod = 10;
    public int manaMod;
    public int defenseMod;
    public int critDMGMod;
    public int critRateMod;
    public int accuracyMod;
    public int resistanceMod;
    public float constantMod;

    public int damageMitigationMod = 0;
    public int defenseMitigationMod = 0;
    public int resistanceMitigationMod = 0;
}
