//////////////////////////////////////////////////////////////////////
//
//  Unity Source Code
//
//  File: LoadScene.cs
//  Description: Script for transition
//
//  History:
//  - September 29, 2023: Created by Bhekti
//  - 
//
//////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections.Generic;

public class LoadScene : MonoBehaviour
{
    public static LoadScene instance;
    [SerializeField] private List<string> transitionID;
    [SerializeField] private float loadDelay;
    [SerializeField] private EasyTransition.TransitionManager transitionManager;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void loadScene(MapName sceneName)
    {
        transitionManager.LoadScene(SplitCamelCase(sceneName.ToString()), transitionID[Random.Range(0, transitionID.Count)], loadDelay);
    }

    public static string SplitCamelCase(string input)
    {
        return System.Text.RegularExpressions.Regex.Replace(input, "(\\B[A-Z])", " $1");
    }
}



