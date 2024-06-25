using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;

public class Customer : MonoBehaviour
{
    public ItemType desiredItem;
    public float patienceTime = 30.0f; // Müþterinin bekleme süresi
    private float timer;
    public TextMeshProUGUI itemText;
    public TextMeshProUGUI receivedItemsText; // Aldýðý ürünleri göstermek için
    private List<ItemType> receivedItems = new List<ItemType>(); // Aldýðý ürünlerin listesi

    private void Start()
    {
        desiredItem = GetRandomItem(); // Rastgele bir item belirle
        timer = patienceTime;
        UpdateItemText(); // UI'yi güncelle
        UpdateReceivedItemsText(); // Baþlangýçta UI öðelerini güncelle
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
            receivedItems.Add(item); // Aldýðý ürünü listeye ekle
            UpdateReceivedItemsText(); // Aldýðý ürünleri güncelle
            ExitFromArea(CustomerManager.Instance.exitPoint.position); // Ürünü aldýðýnda çýkýþ yap
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
