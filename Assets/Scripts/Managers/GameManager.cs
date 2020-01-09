using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static bool isRunning = true;

    [SerializeField] private GameObject scoreCanvas = null;
    [SerializeField] private TextMeshProUGUI scoreText = null;
    [SerializeField] private GameObject pauseCanvas = null;
    [SerializeField] private TimeManager timeManager = null;

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        isRunning = true;
        pauseCanvas.SetActive(false);
        scoreCanvas.SetActive(false);
        RaceManager.Instance.StartRace();
    }

    public void StopGame()
    {
        isRunning = false;
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

    public IEnumerator DisplayScore()
    {
        scoreCanvas.SetActive(true);
        scoreText.text = $"Time taken\n {timeManager.timeTaken.ToString("F1")}";
        yield return new WaitForSeconds(2f);
        ChangeScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
    }
}
