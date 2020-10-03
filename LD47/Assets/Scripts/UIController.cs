using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Image dialMask;
    public Image needMask;
    public float initialPercent = 0;


    float originalDialSize;
    float originalNeedSize;

    // Start is called before the first frame update
    void Start()
    {
        originalDialSize = dialMask.rectTransform.rect.width;
        originalNeedSize = needMask.rectTransform.rect.width;

        SetUITime(initialPercent);
    }

    public void SetUITime(float percent)
    {
        dialMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalDialSize * percent);
    }

    public void SetUINeed(float percent)
    {
        needMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalNeedSize * percent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
