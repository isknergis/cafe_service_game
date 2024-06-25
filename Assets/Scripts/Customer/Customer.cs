using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Customer : MonoBehaviour
{
    public ItemType desiredItem;
    public float patienceTime = 30.0f; // Müþterinin bekleme süresi
    private float timer;
    private bool isExiting = false; // Müþteri çýkýþta mý kontrolü
    public TextMeshProUGUI itemText;
    public TextMeshProUGUI receivedItemsText; // Aldýðý ürünleri göstermek için

    private List<ItemType> receivedItems = new List<ItemType>(); // Aldýðý ürünlerin listesi

    private void Start()
    {
        desiredItem = GetRandomItem(); // Rastgele bir item belirle
        timer = patienceTime;
        UpdateItemText();
        UpdateReceivedItemsText(); // Baþlangýçta UI öðelerini güncelle
    }

    private void Update()
    {
        if (!isExiting)
        {
            // Bekleme süresi azalýr
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                StartCoroutine(MoveToPosition(CustomerManager.Instance.exitPoint.position, true));
            }
        }
    }

    public bool ReceiveItem(ItemType item)
    {
        if (item == desiredItem)
        {
            Debug.Log("Müþteri istediði ürünü aldý.");
            receivedItems.Add(item); // Aldýðý ürünü listeye ekle
            UpdateReceivedItemsText(); // Aldýðý ürünleri güncelle
            // Müþteri ürünü aldýðýnda yapýlacak iþlemler
            StartCoroutine(MoveToPosition(CustomerManager.Instance.exitPoint.position, true));
            return true;
        }
        else
        {
            Debug.Log("Müþteri yanlýþ ürünü aldý.");
            return false;
        }
    }

    public void MoveNext(Vector3 nextPosition)
    {
        StartCoroutine(MoveToPosition(nextPosition));
    }

    public void ExitFromArea(Vector3 exitPosition)
    {
        isExiting = true;
        StartCoroutine(MoveToPosition(exitPosition, true));
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

    private IEnumerator MoveToPosition(Vector3 targetPosition, bool destroyOnArrival = false)
    {
        float elapsedTime = 0;
        float duration = 1.0f; // Hareket süresi (sn)
        Vector3 startingPosition = transform.position;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startingPosition, targetPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;

        if (destroyOnArrival)
        {
            Destroy(gameObject);
        }
    }

    private ItemType GetRandomItem()
    {
        ItemType[] items = {
            ItemType.CHEESECAKESTRAW, ItemType.CUPCAKEREDVELVET,
            ItemType.LEMONADE, ItemType.ORANGEJUÝCE,
            ItemType.BURGER
        };
        int randomIndex = Random.Range(0, items.Length);
        return items[randomIndex];
    }
}
