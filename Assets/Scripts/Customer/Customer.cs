using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class Customer : MonoBehaviour
{
    public ItemType desiredItem;
    public float patienceTime = 30.0f; // Müþterinin bekleme süresi
    private float timer;


    public TextMeshProUGUI itemText;


    private void Start()
    {
   
        timer = patienceTime;
        UpdateItemText();

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

  /*  private ItemType GetRandomItem()
    {
        ItemType[] items = {
            ItemType.CHEESECAKESTRAW, ItemType.CUPCAKEREDVELVET,
            ItemType.LEMONADE, ItemType.ORANGEJUÝCE,
             ItemType.BURGER
        };
        int randomIndex = Random.Range(0, items.Length);
        return items[randomIndex];
    }*/



    public bool ReceiveItem(ItemType item)
    {
        if (item == desiredItem)
        {
            Debug.Log("Müþteri istediði ürünü aldý.");
            // Müþteri ürünü aldýðýnda yapýlacak iþlemler
            Destroy(gameObject, 10f);
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
        // Yeni pozisyona gitme iþlemleri burada yapýlýr
        transform.position = nextPosition;
    }
    public void ExitFromArea(Vector3 exitPosition)
    {
        // Çýkýþ pozisyonuna gitme iþlemleri burada yapýlýr
        transform.position = exitPosition;
        // Müþteriyi yok etme veya baþka bir iþlem yapma
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
