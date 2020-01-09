using UnityEngine;
using UnityEngine.UI;

public abstract class Powerup : ScriptableObject
{
    public new string name;
    [TextArea]
    public string description;
    public Image image;
    public abstract void Execute(PlayerController player);
}
