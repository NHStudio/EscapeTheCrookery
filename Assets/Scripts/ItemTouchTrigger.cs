using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTouchTrigger : MonoBehaviour
{
    private Collider2D _collider2D;
    private DroppedItem _parentItem;

    public void Start()
    {
        _collider2D = gameObject.GetComponent<BoxCollider2D>();
        _parentItem = gameObject.GetComponentInParent<DroppedItem>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        BaseActorController actorController =
            col.gameObject.GetComponent(typeof(BaseActorController)) as BaseActorController;
        if (actorController is null)
        {
            return;
        }

        if (actorController.TakeItem(_parentItem.StoredItem))
        {
            Debug.Log($"Picked {_parentItem.StoredItem} by {col.gameObject}");
            Destroy(_parentItem.gameObject);
        }
    }
}
