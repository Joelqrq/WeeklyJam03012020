using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void ChangeScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void ExitApp()
    {
        Application.Quit();
    }
}
