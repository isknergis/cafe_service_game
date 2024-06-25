using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomerQueue : MonoBehaviour
{
    public ItemType desiredItem;
    public float patienceTime = 30.0f; // M��terinin bekleme s�resi
    private float timer;
    private bool isExiting = false; // M��teri ��k��ta m� kontrol�
    public TextMeshProUGUI itemText;
    public TextMeshProUGUI receivedItemsText; // Ald��� �r�nleri g�stermek i�in

    private List<ItemType> receivedItems = new List<ItemType>(); // Ald��� �r�nlerin listesi

    private void Start()
    {
        
        timer = patienceTime;
      
        UpdateReceivedItemsText(); // Ba�lang��ta UI ��elerini g�ncelle
    }

    private void Update()
    {
        if (!isExiting)
        {
            // Bekleme s�resi azal�r
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
            Debug.Log("M��teri istedi�i �r�n� ald�.");
            receivedItems.Add(item); // Ald��� �r�n� listeye ekle
            UpdateReceivedItemsText(); // Ald��� �r�nleri g�ncelle
            // M��teri �r�n� ald���nda yap�lacak i�lemler
            StartCoroutine(MoveToPosition(CustomerManager.Instance.exitPoint.position, true));
            return true;
        }
        else
        {
            Debug.Log("M��teri yanl�� �r�n� ald�.");
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

    private IEnumerator MoveToPosition(Vector3 targetPosition, bool destroyOnArrival = false)
    {
        float elapsedTime = 0;
        float duration = 1.0f; // Hareket s�resi (sn)
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

   
}
