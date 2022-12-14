
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorableButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
    // Color of the button in idle (373755)
    public Color IdleColor = new(55.0f / 255.0f, 55 / 255.0f, 85 / 255.0f);

    // Color of the button in hover (4F4F6F)
    public Color HoverColor = new(79.0f / 255.0f, 79 / 255.0f, 111 / 255.0f);

    // Color of the button in pressed (6F6F8F)
    public Color PressedColor = new(111.0f / 255.0f, 111 / 255.0f, 143 / 255.0f);

    bool isHovering = false;
    bool isPressed = false;
    
    private void Start()
    {
        GetComponent<Image>().color = IdleColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        UpdateColor();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        isPressed = false;
        UpdateColor();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        UpdateColor();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        UpdateColor();
    }
    
    private void UpdateColor()
    {
        if (isPressed)
        {
            GetComponent<Image>().color = PressedColor;
        }
        else if (isHovering)
        {
            GetComponent<Image>().color = HoverColor;
        }
        else
        {
            GetComponent<Image>().color = IdleColor;
        }
    }
}