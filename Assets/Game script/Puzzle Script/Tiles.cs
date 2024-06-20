using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tiles : MonoBehaviour
{
    public Vector2 targetPosition; // Ubah ke Vector2 untuk RectTransform

    private Vector2 correctPosition;
    
    private Image image; // Ganti SpriteRenderer dengan Image

    public int number;

    public bool inRightPlace;
    
    // Start is called before the first frame update
    void Awake()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        targetPosition = rectTransform.anchoredPosition;
        correctPosition = rectTransform.anchoredPosition;
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, targetPosition, 0.05f);
        if (targetPosition == correctPosition)
        {
            image.color = Color.green;
            inRightPlace = true;
        }
        else
        {
            image.color = Color.white;
            inRightPlace = false;
        }
    }
}