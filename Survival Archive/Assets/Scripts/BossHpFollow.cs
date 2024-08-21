using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHpFollow : MonoBehaviour
{
    private Vector3 distance = Vector3.down * 250.0f;
    private Transform targetTransform;
    private RectTransform rectTransform;

    public void SetUp(Transform target)
    {
        targetTransform = target;
        rectTransform = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        if(!targetTransform) {
            Destroy(gameObject);
            return;
        }
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetTransform.position);
        rectTransform.position = screenPosition + distance;
    }
}
