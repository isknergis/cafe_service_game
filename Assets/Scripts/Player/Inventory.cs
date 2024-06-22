using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[System.Serializable]

public class ObjectnType
{
    public GameObject item;
    public ItemType type;
}


public class Inventory : MonoBehaviour
{
    private ItemType currentItem = ItemType.NONE;


    [SerializeField] private List<ObjectnType> itemsToHold = new List<ObjectnType>();
    private ItemType currentType;
    public ItemType CurrentType { get { return currentType; } }

    private void Start()
    {
        currentType = ItemType.NONE;
        Debug.Log("Inventory initialized. Current item type: " + currentType);
    }

    public void TakeItem(ItemType type)
    {
        if (currentType != ItemType.NONE)
        {
            Debug.LogWarning("Cannot take item. Already holding: " + currentType);
            return;
        }

        currentType = type;
        Debug.Log("Took item of type: " + type);

        foreach (ObjectnType itemHold in itemsToHold)
        {
            if (itemHold.type != type)
            {
                itemHold.item.SetActive(false);
                Debug.Log("Deactivated item: " + itemHold.type);
            }
            else
            {
                itemHold.item.SetActive(true);
                Debug.Log("Activated item: " + itemHold.type);
            }
        }
    }

    public ItemType PutItem()
    {
        if (currentType == ItemType.NONE)
        {
            Debug.LogWarning("No item to put.");
            return ItemType.NONE;
        }

        ItemType tempType = currentType;
        currentType = ItemType.NONE;
        Debug.Log("Put item of type: " + tempType);
        return tempType;
    }

    public void ClearHand()
    {
        Debug.Log("Clearing hand. Current item: " + currentType);
        currentType = ItemType.NONE;
        itemsToHold.ForEach(obj => obj.item.SetActive(false));
    }

    public ItemType GetItem()
    {
        Debug.Log("Getting current item: " + currentType);
        return currentType;
    }
    public void SetItem(ItemType item)
    {
        currentItem = item;
    }

}
