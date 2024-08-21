using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;
public class Item : MonoBehaviour
{
    public ItemData data;
    public ItemData[] cData;
    ItemData useData;
    public int level;
    public Weapon weapon;
    public Gear gear;
    

    Image icon;
    Text textLevel;
    Text textName;
    Text textDesc;
    private void Awake()
    {
        useData = data;
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = useData.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel= texts[0];
        textName = texts[1];
        textDesc = texts[2];
        textName.text = useData.ItemName;
    }

    public void CharInit(int id)
    {
        useData = cData[id];
        icon.sprite = useData.itemIcon;
        textName.text = useData.ItemName;
    }
    private void OnEnable()
    {
        textLevel.text = "Lv." + (level + 1);
        switch (useData.itemType) {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                textDesc.text = string.Format(useData.itemDesc, useData.damages[level] * 100, useData.counts[level]);
                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                textDesc.text = string.Format(useData.itemDesc, useData.damages[level]* 100);
                break;
            case ItemData.ItemType.Heal:
                textDesc.text = string.Format(useData.itemDesc);
                break;
        }
    
    }
    
    public void OnClick()
    {
        switch (data.itemType) {
            case ItemData.ItemType.Melee: 
            case ItemData.ItemType.Range: 
                if(level == 0) {
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<Weapon>();
                    weapon.Init(useData);
                }
                else {
                    float nextDamage = useData.baseDamage;
                    int nextCount = 0;
                    nextDamage += useData.baseDamage * useData.damages[level];
                    nextCount += useData.counts[level];

                    weapon.LevelUp(nextDamage, nextCount);
                }
                level++;
                break;
            case ItemData.ItemType.Glove: 
            case ItemData.ItemType.Shoe: 
                if(level == 0) {
                    GameObject newGear = new GameObject();
                    gear = newGear.AddComponent<Gear>();
                    gear.Init(useData);
                }
                else {
                    float nextRate = useData.damages[level];
                    gear.LevelUp(nextRate);
                }
                level++;
                break;
            case ItemData.ItemType.Heal:
                GameManager.instance.health = GameManager.instance.health + 50;
                break;
        }
        if(level == useData.damages.Length) {
            GetComponent<Button>().interactable = false;
        }
    }
}
