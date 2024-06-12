using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchObjectByTag : MonoBehaviour
{
    public GameObject[] gameObjects;
    public string tags;
    void Start()
    {
        gameObjects = GameObject.FindGameObjectsWithTag(tags);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
