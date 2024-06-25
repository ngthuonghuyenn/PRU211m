using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjectTrigger : MonoBehaviour
{
    private ItemObjects myItemObject => GetComponentInParent<ItemObjects>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            if(collision.GetComponent <CharacterStats>().isDead)
            {
                return;
            }
            Debug.Log("nhat item");
            myItemObject.PickupItem();
        }
    }
}
