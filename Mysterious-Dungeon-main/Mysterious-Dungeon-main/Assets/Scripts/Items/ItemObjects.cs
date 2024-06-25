using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjects : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private ItemData itemData;
    [SerializeField] private Vector2 velocity;


    private void SetupVisuals()
    {
        if (itemData == null)
            return;
        GetComponent<SpriteRenderer>().sprite = itemData.icon;
        gameObject.name = "Item object -" + itemData.name;
    }

    private void Update()
    {
        if (itemData == null)
            return;
        if(Input.GetKeyDown(KeyCode.M))
            rb.velocity = velocity;
    }

    public void SetupItem(ItemData _itemData, Vector2 _velocity)
    {
        itemData = _itemData;
        rb.velocity = _velocity;
        SetupVisuals();
    }


    public void PickupItem()
    {
        Inventory.instance.AddItem(itemData);
        Destroy(gameObject);
    }
}
