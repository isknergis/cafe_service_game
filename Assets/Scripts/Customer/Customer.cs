using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public ItemType desiredItem;
    public float patienceTime = 30.0f; // M��terinin bekleme s�resi
    private float timer;

    private void Start()
    {
        // M��terinin bekleme s�resi ba�lat�l�r
        timer = patienceTime;
    }

    private void Update()
    {
        // Bekleme s�resi azal�r
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
            Debug.Log("M��teri istedi�i �r�n� ald�.");
            // M��teri �r�n� ald���nda yap�lacak i�lemler
            return true;
        }
        else
        {
            Debug.Log("M��teri yanl�� �r�n� ald�.");
            return false;
        }
    }
}
