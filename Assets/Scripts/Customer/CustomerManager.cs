using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public static CustomerManager Instance;
    [SerializeField] private float spawnRateMin = 5f;
    [SerializeField] private float spawnRateMax = 10f;
    [SerializeField] private int maxCustomers = 5; // Maksimum m��teri say�s�
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
        spawnTime = Random.Range(spawnRateMin, spawnRateMax); // �lk spawn zaman� belirleme
    }

    private void Update()
    {
        if (customerList.Count < maxCustomers) // Maksimum m��teri say�s�n� kontrol et
        {
            currentTime += Time.deltaTime;
            if (currentTime >= spawnTime)
            {
                SpawnCustomer();
                currentTime = 0f;
                spawnTime = Random.Range(spawnRateMin, spawnRateMax); // Yeni spawn zaman� belirleme
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

        // ��k�� yapan m��teriyi belirle
        Customer firstCustomer = customerList.Dequeue(); // M��teriyi kuyruktan ��kar
        firstCustomer.ExitFromArea(exitPoint.position);

        // Geri kalan m��terilerin pozisyonlar�n� g�ncelle
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
