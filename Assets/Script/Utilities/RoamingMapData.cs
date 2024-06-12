//////////////////////////////////////////////////////////////////////
//
//  Unity Source Code
//
//  File: RoamingMMapData.cs
//  Description: Script for handle all data for roaming scene
//
//  History:
//  - November 18, 2023: Created by Bhekti
//
//
//////////////////////////////////////////////////////////////////////
using System.Collections.Generic;
using UnityEngine;

public class RoamingMapData : MonoBehaviour
{
    public static RoamingMapData instance;
    [SerializeField] private BGMName _BGMName;
    [SerializeField] private MapName _sceneName;

    private List<EnemyStateMachine> enemyTargetingPlayer = new List<EnemyStateMachine>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        HUDHandler.instance.showAllUIGroup();

        HUDHandler.instance.combatUIHandler.refresh();
        AudioManager.instance.playBGM(_BGMName);
    }

    public MapName getMapName()
    {
        return _sceneName;
    }

    public void changeBGM()
    {
        if (enemyTargetingPlayer.Count > 0)
            AudioManager.instance.playBGM(BGMName.Combat1);
        else
            AudioManager.instance.playBGM(_BGMName);
    }

    public void addEnemy(EnemyStateMachine enemyStateMachine)
    {
        if (enemyTargetingPlayer.Contains(enemyStateMachine)) return;

        enemyTargetingPlayer.Add(enemyStateMachine);
        changeBGM();
    }

    public void removeEnemy(EnemyStateMachine enemyStateMachine)
    {
        enemyTargetingPlayer.Remove(enemyStateMachine);
        changeBGM();
    }
}
