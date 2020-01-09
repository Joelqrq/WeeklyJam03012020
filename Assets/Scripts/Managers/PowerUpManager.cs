using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] private PlayerController player = null;
    [SerializeField] private PowerupList powerupList = null;
    [SerializeField] private PUButton[] pUButtons = new PUButton[0];

    private static PowerUpManager SingletonManager;
    public static PowerUpManager Instance { get { return SingletonManager; } }

    private void Awake()
    {
        if (SingletonManager && SingletonManager != this)
            Destroy(gameObject);
        else
            SingletonManager = this;
    }

    public void ChoosePUPhase()
    {
        GameManager.isRunning = false;
    }

    private void RandomizePU()
    {
        powerupList.Powerups
    }

    public void ChoosePU(int index)
    {

    }

    public void RejectPU()
    {

    }

    [System.Serializable]
    public struct PUButton
    {
        public TextMeshProUGUI name;
        public TextMeshProUGUI description;
        public Image image;
    }
}
