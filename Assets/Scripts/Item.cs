using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    material,
    consumable,
    armor,
    weapon,
}
public class Item : MonoBehaviour
{
    public string itemName;
    public float xpToGainOnGather;
}
