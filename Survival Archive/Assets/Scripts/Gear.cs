using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.ItemType type;
    public float rate;

    public void Init(ItemData data)
    {
        name = "Gear" + data.ItemName;
        transform.parent = GameManager.instance.player.transform;
        transform.localPosition = Vector3.zero;

        type = data.itemType;
        rate = data.damages[0];
        ApplyGear();
    }

    public void ApplyGear()
    {
        switch (type) {
            case ItemData.ItemType.Glove:
                RateUp();
                break;
            case ItemData.ItemType.Shoe:
                SpeedUp();
                break;
        }
    }
    public void LevelUp(float rate) 
    {
        this.rate = rate;
        ApplyGear();
    }

    public void RateUp()
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        foreach(Weapon weapon in weapons){
            switch (weapon.id){
                case 0:weapon.speed = 150 + (150 * rate);
                    break;
            } 
        }
    }
    public void SpeedUp()
    {
        float speed = 3;
        GameManager.instance.player.speed = speed + (speed * rate);
    }
}
