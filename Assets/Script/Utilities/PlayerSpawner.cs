//////////////////////////////////////////////////////////////////////
//
//  Unity Source Code
//
//  File: PlayerSpawner.cs
//  Description: Script to handle spawn player in current scene
//
//  History:
//  - November 17, 2023: Created by Bhekti
//
//
//////////////////////////////////////////////////////////////////////

using Unity.Mathematics;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public static PlayerSpawner instance;
    [SerializeField] private PlayerSO _playerSO;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        instantiateCharacter(_playerSO.lastPosition, _playerSO.playerObject);
    }

    private void Start()
    {

    }

    private void setUpGame() // noted : masih perlu perbaikan untuk menentukan script apa saja yang harus di setup
    {

        // LoadScene.instance.loadScene(_defaultMapName);

        // GameObject turnBaseManager = new GameObject("Turn Base Manager");

        // turnBaseManager.transform.SetParent(transform);

        // turnBaseManager.AddComponent<TurnBaseManager>();
    }

    public void instantiateCharacter(Vector3 post, GameObject playerObject)
    {
        GameObject game = Instantiate(playerObject, post, quaternion.identity);
    }
}
