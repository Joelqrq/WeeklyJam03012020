using UnityEngine;

public abstract class Powerup : ScriptableObject
{
    public new string name;
    [TextArea]
    public string description;
    public Sprite image;
    public abstract void Execute(PlayerController player);
}
