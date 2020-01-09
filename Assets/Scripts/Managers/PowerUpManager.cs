using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] private PlayerController player = null;
    [SerializeField] private PowerupList powerupList = null;
    [SerializeField] private GameObject powerCanvas = null;
    [SerializeField] private PUButton[] pUButtons = new PUButton[0];

    private static PowerUpManager SingletonManager;
    public static PowerUpManager Instance { get { return SingletonManager; } }

    private void Awake()
    {
        if (SingletonManager && SingletonManager != this)
            Destroy(gameObject);
        else
            SingletonManager = this;

        powerCanvas.SetActive(false);
    }

    public void ChoosePUPhase()
    {
        GameManager.isRunning = false;
        powerCanvas.SetActive(true);
        RandomizePU(pUButtons[0]);
        RandomizePU(pUButtons[1]);
        player.PlayerControls.Disable();
    }

    private void RandomizePU(PUButton pUButton)
    {
        int id = Random.Range(0, powerupList.Powerups.Length);
        Powerup pu = powerupList.Powerups[id];
        pUButton.name.text = pu.name;
        pUButton.description.text = pu.description;
        pUButton.image.sprite = pu.image;
        //Debug.Log(id);
        pUButton.id = id;
    }

    public void ChoosePU(int index)
    {
        //Debug.Log(pUButtons[index].id);
        powerupList.Powerups[pUButtons[index].id].Execute(player);
        RemovePUCanvas();
    }

    public void RemovePUCanvas()
    {
        GameManager.isRunning = true;
        powerCanvas.SetActive(false);
        player.PlayerControls.Enable();
    }

    [System.Serializable]
    public class PUButton
    {
        public TextMeshProUGUI name;
        public TextMeshProUGUI description;
        public Image image;
        [HideInInspector]
        public int id;
    }
}
