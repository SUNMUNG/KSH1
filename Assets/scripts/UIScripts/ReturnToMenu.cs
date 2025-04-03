using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
    public void Returntomenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
