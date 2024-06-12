using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing;
using UnityEngine.UI;


public class GenericItemStatus : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private GenericText _statusName;
    [SerializeField] private GenericText _statusValue;

    public void setup(string statusName, string statusValue)
    {
        _statusName.SetText(statusName);
        _statusValue.SetText(statusValue);
    }
}
