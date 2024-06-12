using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAnimation : MonoBehaviour
{
    [SerializeField] private GameObject _logo;
    [SerializeField] private GameObject _anyKey;
    public void ShowLogo()
    {
        _logo.SetActive(true);
    }

    public void showAnyKey()
    {
        _anyKey.SetActive(true);
    }
}
