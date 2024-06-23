using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class Customer : MonoBehaviour
{
    public ItemType desiredItem;
    public float patienceTime = 30.0f; // M��terinin bekleme s�resi
    private float timer;


    public TextMeshProUGUI itemText;


    private void Start()
    {
   
        timer = patienceTime;
        UpdateItemText();

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

  /*  private ItemType GetRandomItem()
    {
        ItemType[] items = {
            ItemType.CHEESECAKESTRAW, ItemType.CUPCAKEREDVELVET,
            ItemType.LEMONADE, ItemType.ORANGEJU�CE,
             ItemType.BURGER
        };
        int randomIndex = Random.Range(0, items.Length);
        return items[randomIndex];
    }*/



    public bool ReceiveItem(ItemType item)
    {
        if (item == desiredItem)
        {
            Debug.Log("M��teri istedi�i �r�n� ald�.");
            // M��teri �r�n� ald���nda yap�lacak i�lemler
            Destroy(gameObject, 10f);
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
        // Yeni pozisyona gitme i�lemleri burada yap�l�r
        transform.position = nextPosition;
    }
    public void ExitFromArea(Vector3 exitPosition)
    {
        // ��k�� pozisyonuna gitme i�lemleri burada yap�l�r
        transform.position = exitPosition;
        // M��teriyi yok etme veya ba�ka bir i�lem yapma
        Destroy(gameObject);
    }
    private void UpdateItemText()
    {
        if (itemText != null)
        {
            itemText.text = desiredItem.ToString();
        }
    }
}
