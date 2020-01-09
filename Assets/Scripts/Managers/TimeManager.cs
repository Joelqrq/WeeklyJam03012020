using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText = null;
    [HideInInspector] public float timeTaken = 0f;

    private void Update()
    {
        if (!GameManager.isRunning)
            return;

        timeTaken += Time.deltaTime;
        countdownText.text = $"Time: {timeTaken.ToString("F1")}";
    }
}
