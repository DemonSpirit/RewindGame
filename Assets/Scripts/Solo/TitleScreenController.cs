using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleScreenController : MonoBehaviour
{

    public void GoToGame()
    {
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }
    public void ExitGame()
    {
        Application.Quit();
        print("Sorry, you can't leave!!!");
    }
}
