using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIInventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        UIInventoryItem item = dropped.GetComponent<UIInventoryItem>();
        item.parentAfterDrag = transform;
    }
}
