using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    [SerializeField]
    private ItemsMeta.Item storedItem = ItemsMeta.Item.Null;

    public ItemsMeta.Item StoredItem
    {
        get => storedItem;
        set
        {
            _spriteRenderer.sprite = ItemsMeta.ItemsDesc[storedItem].Icon;
            storedItem = value;
        }
    }

    private Collider2D _collider2D;
    private SpriteRenderer _spriteRenderer;

    public void Start()
    {
        _collider2D = gameObject.GetComponent<Collider2D>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        
        // Apply sprite for item set in Inspector
        StoredItem = StoredItem;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        bool isPlayer = col.gameObject.CompareTag("Player");
        bool isEnemy = col.gameObject.CompareTag("Enemy");
        
        if (isEnemy)
        {
            Physics2D.IgnoreCollision(_collider2D, col.collider);
            return;
        }

        if (isPlayer)
        {
            Debug.Log($"Picking {StoredItem}");
            if (InventoryManager.Instance.Store(StoredItem))
            {
                Destroy(gameObject);
            }
            else
            {
                Physics2D.IgnoreCollision(_collider2D, col.collider);
            }
        }
    }
}