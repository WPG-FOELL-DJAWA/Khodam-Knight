using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerSO : MonoBehaviour
{
    [SerializeField] private PlayerSO playerSO;

    private void Start()
    {
        playerSO.resetPosition();
    }
}
