using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIDamageIndicator : MonoBehaviour
{
    public TMP_Text damageText;
    public float moveSpeed, lifetime = 3f;

    public RectTransform damageTextRect;

    void Start()
    {
        Destroy(gameObject, lifetime);

        damageTextRect = GetComponent<RectTransform>();
    }

    void Update()
    {
        damageTextRect.anchoredPosition += new Vector2(0, -moveSpeed * Time.fixedDeltaTime);
    }
}
