using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Image mask;
    public float initialPercent = 0;


    float originalSize;

    // Start is called before the first frame update
    void Start()
    {
        originalSize = mask.rectTransform.rect.width;

        SetUITime(initialPercent);
    }

    public void SetUITime(float percent)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * percent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
