using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPSlider : MonoBehaviour
{
    private float maxHp;
    private float curHp;
    private Slider hpSlider;
    private GameObject boss;

    private void Awake()
    {
        hpSlider = GetComponent<Slider>();
    }
    public void Init(GameObject boss) 
    {
        this.boss = boss;
    }

    private void LateUpdate()
    {
        curHp = boss.GetComponent<Enemy>().health;
        maxHp = boss.GetComponent<Enemy>().maxHealth;
        hpSlider.value =curHp/ maxHp;
    }
}
