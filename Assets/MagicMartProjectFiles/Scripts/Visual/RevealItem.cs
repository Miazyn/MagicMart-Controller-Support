using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RevealItem : MonoBehaviour
{
    [Header("Reveal Images")]
    [SerializeField] Image coloredImage;
    [SerializeField] Image overlayImage;

    [Header("Sizes")]
    [SerializeField] float upScale = 2f;
    [SerializeField] float growthSpeedSecs = 0.001f;
    [SerializeField] Vector2 shrinkPerTimer = new Vector2(1f,1f);
    Vector2 ogScale;

    RectTransform coloredRect, overlayRect;
    

    void Start()
    {
        coloredRect = coloredImage.GetComponent<RectTransform>();
        overlayRect = overlayImage.GetComponent<RectTransform>();

        ogScale = coloredRect.sizeDelta;

    }


    public void RevealSize()
    {
        coloredRect.sizeDelta = ogScale * upScale;
        overlayRect.sizeDelta = ogScale * upScale;

        StartCoroutine(SizeToOriginalSize());
    }

    IEnumerator SizeToOriginalSize()
    {
        while(coloredRect.sizeDelta != ogScale)
        {
            overlayRect.sizeDelta -= shrinkPerTimer;
            coloredRect.sizeDelta -= shrinkPerTimer;
            yield return new WaitForSeconds(growthSpeedSecs);
        }
    }
}
