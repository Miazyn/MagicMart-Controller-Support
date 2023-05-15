using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CustomButton : Button, IPointerClickHandler
{
    Image img;
    [SerializeField] Sprite btnSprite;
    [SerializeField] AudioClip clickSound;
    [SerializeField] TMP_FontAsset tmpFont;

    Color color;
    AudioSource audioSource;

    Button btn;
    TextMeshProUGUI textUI;

    protected override void Start()
    {
        //NAME
        this.name = "CustomButton";
        //IMAGE
        if (GetComponent<Image>() == null)
        {
            img = gameObject.AddComponent<Image>();
        }
        img = GetComponent<Image>();
        if (btnSprite != null)
        {
            img.sprite = btnSprite;
        }

        base.Start();

        //AUDIO
        if (GetComponent<AudioSource>() == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = clickSound;

        //BUTTON
        btn = gameObject.GetComponent<Button>();
        if (GetComponent<Graphic>() != null)
        {
            btn.targetGraphic = GetComponent<Graphic>();
        }

        btn.targetGraphic = GetComponent<Graphic>();

        RectTransform thisRect = GetComponent<RectTransform>();
        //////////////////////////////////////////////
        Transform childText = transform.Find("Text");

        if (childText == null)
        {
            GameObject textMesh = new GameObject("Text");

            textMesh.transform.SetParent(transform);
            textMesh.transform.localPosition = Vector3.zero;

            if (GetComponent<TextMeshProUGUI>() == null)
            {
                textUI = textMesh.AddComponent<TextMeshProUGUI>();
            }

            textUI = textMesh.GetComponent<TextMeshProUGUI>();
            textUI.font = tmpFont;

            if (ColorUtility.TryParseHtmlString("#" + "FFDFBF", out color))
            {
                textUI.color = color;
            }

            textUI.text = "Text here...";
            textUI.fontSize = 45;
            textUI.alignment = TextAlignmentOptions.Center;

            RectTransform textTransform = textMesh.GetComponent<RectTransform>();
            textTransform.sizeDelta = new Vector2(thisRect.sizeDelta.x, thisRect.sizeDelta.y);
            textTransform.localScale = new Vector2(1, 1);

            textTransform.anchorMin = Vector2.zero;
            textTransform.anchorMax = Vector2.one;
            textTransform.offsetMin = Vector2.zero;
            textTransform.offsetMax = Vector2.one;
        }
        else if(childText.parent == transform)
        {
            GameObject textMesh = childText.gameObject;
        }
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        audioSource.Play();
    }

}
