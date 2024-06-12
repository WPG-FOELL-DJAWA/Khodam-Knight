using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void NewGame()
    {
        Debug.Log("NewGame button clicked");
        SceneManager.LoadScene("Cutscene");
    }
}
