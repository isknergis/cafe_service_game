using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;

public class Customer : MonoBehaviour
{
    public ItemType desiredItem;
    public float patienceTime = 30.0f; // M��terinin bekleme s�resi
    private float timer;
    public TextMeshProUGUI itemText;
    public TextMeshProUGUI receivedItemsText; // Ald��� �r�nleri g�stermek i�in
    private List<ItemType> receivedItems = new List<ItemType>(); // Ald��� �r�nlerin listesi

    private void Start()
    {
        desiredItem = GetRandomItem(); // Rastgele bir item belirle
        timer = patienceTime;
        UpdateItemText(); // UI'yi g�ncelle
        UpdateReceivedItemsText(); // Ba�lang��ta UI ��elerini g�ncelle
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
            Debug.Log("M��teri istedi�i �r�n� ald�.");
            receivedItems.Add(item); // Ald��� �r�n� listeye ekle
            UpdateReceivedItemsText(); // Ald��� �r�nleri g�ncelle
            ExitFromArea(CustomerManager.Instance.exitPoint.position); // �r�n� ald���nda ��k�� yap
            return true;
        }
        else
        {
            Debug.Log("M��teri yanl�� �r�n� ald�.");
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
            itemText.text = "�stedi�i �r�n: " + desiredItem.ToString();
        }
    }

    private void UpdateReceivedItemsText()
    {
        if (receivedItemsText != null)
        {
            receivedItemsText.text = "Ald��� �r�nler:\n";
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
            ItemType.LEMONADE, ItemType.ORANGEJU�CE,
            ItemType.BURGER
        };
        int randomIndex = UnityEngine.Random.Range(0, items.Length);
        return items[randomIndex];
    }
}
