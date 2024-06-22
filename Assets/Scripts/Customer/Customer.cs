using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public ItemType desiredItem;
    public float patienceTime = 30.0f; // Müþterinin bekleme süresi
    private float timer;

    private void Start()
    {
        // Müþterinin bekleme süresi baþlatýlýr
        timer = patienceTime;
    }

    private void Update()
    {
        // Bekleme süresi azalýr
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    public bool ReceiveItem(ItemType item)
    {
        if (item == desiredItem)
        {
            Debug.Log("Müþteri istediði ürünü aldý.");
            // Müþteri ürünü aldýðýnda yapýlacak iþlemler
            return true;
        }
        else
        {
            Debug.Log("Müþteri yanlýþ ürünü aldý.");
            return false;
        }
    }
}
