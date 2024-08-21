using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName ="Scrpitable Object/ItemData") ]
public class ItemData : ScriptableObject
{
    public enum ItemType { Character, Melee, Range, Glove, Shoe, Heal };

    [Header("#Main Info")]
    public ItemType itemType;
    public int itemId;
    public string ItemName;
    [TextArea]
    public string itemDesc;
    public Sprite itemIcon;

    [Header("LevelUp data")]
    public float baseDamage;
    public int baseCount;
    public float[] damages;
    public int[] counts;

    [Header("#Weapon")]
    public GameObject projectile;

}
