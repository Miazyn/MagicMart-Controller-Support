using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Image image;

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = Color.grey;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = Color.white;
    }

    void Start()
    {
        image = GetComponent<Image>();
    }
}
