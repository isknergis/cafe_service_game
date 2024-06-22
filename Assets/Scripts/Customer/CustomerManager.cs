using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public static CustomerManager Instance;
    [SerializeField] private float spawnRateMin = 5f;
    [SerializeField] private float spawnRateMax = 10f;
    [SerializeField] private int maxCustomers = 5; // Maksimum müþteri sayýsý
    [SerializeField] private List<Customer> customerPrefabs = new List<Customer>();
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform exitPoint;
    private float currentTime = 0f;
    private float spawnTime;
    private float lrRandom = 0.75f;
    private Queue<Customer> customerList = new Queue<Customer>();

    private void Awake()
    {
        Instance = this;
        spawnTime = Random.Range(spawnRateMin, spawnRateMax); // Ýlk spawn zamaný belirleme
    }

    private void Update()
    {
        if (customerList.Count < maxCustomers) // Maksimum müþteri sayýsýný kontrol et
        {
            currentTime += Time.deltaTime;
            if (currentTime >= spawnTime)
            {
                SpawnCustomer();
                currentTime = 0f;
                spawnTime = Random.Range(spawnRateMin, spawnRateMax); // Yeni spawn zamaný belirleme
            }
        }
    }

    private void SpawnCustomer()
    {
        Vector3 spawnPos = spawnPoint.position + (spawnPoint.forward * -1 * customerList.Count) + spawnPoint.right * Random.Range(-lrRandom, lrRandom);
        Customer temp = Instantiate(customerPrefabs[Random.Range(0, customerPrefabs.Count)], spawnPos, spawnPoint.rotation);
        customerList.Enqueue(temp);
    }

    public void SellToCustomer()
    {
        if (customerList.Count == 0) return;

        // Çýkýþ yapan müþteriyi belirle
        Customer firstCustomer = customerList.Dequeue(); // Müþteriyi kuyruktan çýkar
        firstCustomer.ExitFromArea(exitPoint.position);

        // Geri kalan müþterilerin pozisyonlarýný güncelle
        UpdateCustomerPositions();
    }

    private void UpdateCustomerPositions()
    {
        Customer[] customers = customerList.ToArray();
        for (int i = 0; i < customers.Length; i++)
        {
            Vector3 nextPos = spawnPoint.position + (spawnPoint.forward * -1 * i) + spawnPoint.right * Random.Range(-lrRandom, lrRandom);
            customers[i].MoveNext(nextPos);
        }
    }
}
