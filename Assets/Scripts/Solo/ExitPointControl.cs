using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ExitPointControl : MonoBehaviour
{
    [SerializeField] string goesTo = "";
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(goesTo, LoadSceneMode.Single);
        }
    }
}
