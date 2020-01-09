using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private float countdownTime = 50f;
    [SerializeField] private TextMeshProUGUI countdownText = null;

    public void ModifyTime(float amount) => countdownTime += amount;

    private void Update()
    {
        if (!GameManager.isRunning)
            return;

        if (countdownTime > 0f)
        {
            countdownTime -= Time.deltaTime;
            countdownText.text = $"Time: {countdownTime.ToString("F1")}";
        }
    }
}
