using UnityEngine;
using UnityEngine.UI;

public class StartGameAnimation : MonoBehaviour
{
    [SerializeField] private ButtonMainMenuAnimation _newGame;
    [SerializeField] private ButtonMainMenuAnimation _loadGame;

    private void Update()
    {
        if (_newGame.buttonState == ButtonMainMenuAnimation.ButtonState.PointerExit && _loadGame.buttonState == ButtonMainMenuAnimation.ButtonState.PointerExit)
        {
            _newGame.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            _loadGame.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            return;
        }


        if (_newGame.buttonState == ButtonMainMenuAnimation.ButtonState.pointerEnter || _newGame.buttonState == ButtonMainMenuAnimation.ButtonState.PointerClick)
        {
            _newGame.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            _loadGame.GetComponent<Image>().color = new Color32(150, 150, 150, 255);
        }
        else
        {
            _loadGame.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            _newGame.GetComponent<Image>().color = new Color32(150, 150, 150, 255);

        }
    }

}