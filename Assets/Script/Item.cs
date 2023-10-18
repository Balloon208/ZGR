using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public int type;
    public int id;
    public string name;
    public Sprite itemImage;
    public string description;
    public int amount;
}