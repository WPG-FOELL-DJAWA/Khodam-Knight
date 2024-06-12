//////////////////////////////////////////////////////////////////////
//
//  Unity Source Code
//
//  File: NPCAI.cs
//  Description: Script for NPC AI in roaming world
//
//  History:
//  - November 14, 2023: Created by Bhekti
//
//
//////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;


public class NPCAI : NPCBehavior
{
    enum State
    {
        Playing,
        Completed
    }
    private State _state;
    [SerializeField] private List<Transform> _transform = new List<Transform>();

}


