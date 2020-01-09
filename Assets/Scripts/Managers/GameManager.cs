using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isRunning = true;

    [SerializeField] private GameObject scoreCanvas = null;
    [SerializeField] private GameObject pauseCanvas = null;

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        isRunning = true;
        pauseCanvas.SetActive(false);
        RaceManager.Instance.StartRace();
    }

    public void StopGame()
    {
        isRunning = false;
        scoreCanvas.SetActive(true);
        pauseCanvas.SetActive(false);
    }

    public void PauseGame()
    {
        isRunning = false;
        pauseCanvas.SetActive(true);
    }

    public void ResumeGame()
    {
        isRunning = true;
        pauseCanvas.SetActive(false);
    }

    public void ChangeScene(int sceneIndex)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
    }
}
