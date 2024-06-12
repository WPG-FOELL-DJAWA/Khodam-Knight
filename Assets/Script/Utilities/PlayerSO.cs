//////////////////////////////////////////////////////////////////////
//
//  Unity Source Code
//
//  File: PlayerSO.cs
//  Description: Script for player data base
//
//  History:
//  - November 18, 2023: Created by Bhekti 
//  - 
//
//////////////////////////////////////////////////////////////////////

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "Turn Base/PlayerSO", order = 0)]
public class PlayerSO : ScriptableObject
{
    public MapName lastScene;
    public Vector3 lastPosition;
    public StatisticSO statisticSO;

    public GameObject playerObject; // noted : harus fleksible tergantung karakter yang terakhir kali di pakai

    [System.Serializable]
    public struct TutorialData
    {
        public bool movementTutorialDone;
    }

    public TutorialData tutorialData;

    public void resetPosition()
    {
        lastScene = MapName.FaunvilleVillage;
        lastPosition = new Vector3(190, 10, 130);
    }

    public void SetScriptableObject(StatisticData statisticData)
    {
        statisticSO.level = statisticData.level;
        statisticSO.healthMod = statisticData.health;
        statisticSO.attackMod = statisticData.attack;
        statisticSO.manaMod = statisticData.mana;
        statisticSO.defenseMod = statisticData.defense;
        statisticSO.resistanceMod = statisticData.evasion;
        statisticSO.constantMod = statisticData.constant;
        statisticSO.critRateMod = statisticData.critRate;
        statisticSO.accuracyMod = statisticData.accuracy;
        statisticSO.critDMGMod = statisticData.critDMG;
    }


}
