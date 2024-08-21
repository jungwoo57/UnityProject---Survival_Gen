using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor.UIElements;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    Item[] items;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
    }

    public void Show()
    {
        Next();
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
    }

    public void Hide() 
    {
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
    }

    public void Select(int index)
    {
        items[index].OnClick();
    }

    public void Next()
    {
        foreach (Item item in items) {
            item.gameObject.SetActive(false);
        }
        // 랜덤아이템 활성화
        int[] ran = new int[3];
        while (true) {
            ran[0] = Random.Range(0, items.Length);
            ran[1] = Random.Range(0, items.Length);
            ran[2] = Random.Range(0, items.Length);
            if (ran[0] != ran[1] && ran[0] != ran[2] && ran[1] != ran[2]) break;
        }
        //무기레벨 max시 아이템으로 변경
        for(int index = 0; index < ran.Length; index++) {
            Item ranItem = items[ran[index]];
            if (ranItem.level == ranItem.data.damages.Length){
                items[3].gameObject.SetActive(true);
            }
            else ranItem.gameObject.SetActive(true);
        }
    }
}
