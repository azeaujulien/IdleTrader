using UnityEngine;

[CreateAssetMenu(fileName="New Upgrade", menuName="ScriptableObject/Upgrade")]
public class Upgrade : ScriptableObject
{
    public Sprite Img; // Image of the upgrade
    public string Name; // Name of the upgrade
    public string Effect; // Effect of the upgrade (for visual display only)
}
