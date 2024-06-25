using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private int amountOfItems;
    [SerializeField] private ItemData[] possibleItemDrop;
    private List<ItemData> dropList = new List<ItemData>();

    [SerializeField] private GameObject dropPrefab;

    public virtual void GenerateDrop()
    {
        for(int i= 0; i < possibleItemDrop.Length; i++) 
        {
            if(Random.Range(0,100) <= possibleItemDrop[i].dropRate)
                dropList.Add(possibleItemDrop[i]);
        }

        for(int i= 0; i < amountOfItems; i++)
        {
            ItemData randomItem = dropList[Random.Range(0, dropList.Count - 1)];
            dropList.Remove(randomItem);
            DropItem(randomItem);
        }
    }

    protected void DropItem(ItemData _itemData)
    {
        GameObject newDrop = Instantiate(dropPrefab, transform.position, Quaternion.identity);

        Vector2 randomVelocity = new  Vector2(Random.Range(-5,5), Random.Range(15,20));

        newDrop.GetComponent<ItemObjects>().SetupItem(_itemData, randomVelocity);
    }
}