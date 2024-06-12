using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyKey : MonoBehaviour
{

    private bool _oneBeat;
    private void Update()
    {
        if (UserInputMainMenu.instance.anyKey && !_oneBeat)
        {
            MainMenuHandler.instance.startMenu();
            _oneBeat = true;
        }
    }
}
