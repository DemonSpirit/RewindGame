using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleScreenController : MonoBehaviour
{

    public void GoToGame()
    {
        SceneManager.LoadScene("Hub", LoadSceneMode.Single);
    }
    public void GoToVersus()
    {
        SceneManager.LoadScene("level select", LoadSceneMode.Single);
    }
    public void ExitGame()
    {
        Application.Quit();
        print("Sorry, you can't leave!!!");
    }
    public void GoToStadium()
    {
        SceneManager.LoadScene("Arena", LoadSceneMode.Single);
    }
    public void GoToDistrict()
    {

        SceneManager.LoadScene("doruk edt 1", LoadSceneMode.Single);
    }
}
