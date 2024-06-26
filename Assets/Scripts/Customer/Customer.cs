using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;

public class Customer : MonoBehaviour
{
    public ItemType desiredItem;
    public float patienceTime = 30.0f;
    private float timer;
    public TextMeshProUGUI itemText;
    public TextMeshProUGUI receivedItemsText;
    public TextMeshProUGUI totalCoin; 

    private List<ItemType> receivedItems = new List<ItemType>();

    private static Dictionary<ItemType, float> itemPrices = new Dictionary<ItemType, float>()
    {
        { ItemType.CHEESECAKESTRAW, 5.0f },
        { ItemType.CUPCAKEREDVELVET, 4.0f },
        { ItemType.LEMONADE, 2.5f },
        { ItemType.ORANGEJUÝCE, 3.0f },
        { ItemType.BURGER, 6.0f }
    };

    public static float totalEarnings = 0.0f;

    private void Start()
    {
        desiredItem = GetRandomItem();
        timer = patienceTime;
        UpdateItemText();
        UpdateReceivedItemsText();
        UpdateTotalEarningsText(); 
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            ExitFromArea(CustomerManager.Instance.exitPoint.position);
        }
    }

    public bool ReceiveItem(ItemType item)
    {
        if (item == desiredItem)
        {
            Debug.Log("Müþteri istediði ürünü aldý.");
            receivedItems.Add(item);
            UpdateReceivedItemsText();

            if (itemPrices.ContainsKey(item))
            {
                totalEarnings += itemPrices[item];
                Debug.Log("Toplam Kazanç: " + totalEarnings);
                UpdateTotalEarningsText(); 
            }

            ExitFromArea(CustomerManager.Instance.exitPoint.position);
            return true;
        }
        else
        {
            Debug.Log("Müþteri yanlýþ ürünü aldý.");
            return false;
        }
    }

    public void MoveNext(Vector3 pos)
    {
        transform.DOMove(pos, 2);
    }

    public void ExitFromArea(Vector3 pos)
    {
        transform.DOMove(pos, 3).OnComplete(() => { Destroy(this.gameObject); });
    }

    private void UpdateItemText()
    {
        if (itemText != null)
        {
            itemText.text = "Ýstediði Ürün: " + desiredItem.ToString();
        }
    }

    private void UpdateReceivedItemsText()
    {
        if (receivedItemsText != null)
        {
            receivedItemsText.text = "Aldýðý Ürünler:\n";
            foreach (var item in receivedItems)
            {
                receivedItemsText.text += item.ToString() + "\n";
            }
        }
    }

    private void UpdateTotalEarningsText()
    {
        if (totalCoin != null)
        {
            totalCoin.text = "Toplam Kazanç: " + totalEarnings.ToString("F2") + " TL"; 
        }
    }

    private ItemType GetRandomItem()
    {
        ItemType[] items = {
            ItemType.CHEESECAKESTRAW, ItemType.CUPCAKEREDVELVET,
            ItemType.LEMONADE, ItemType.ORANGEJUÝCE,
            ItemType.BURGER
        };
        int randomIndex = UnityEngine.Random.Range(0, items.Length);
        return items[randomIndex];
    }
}
