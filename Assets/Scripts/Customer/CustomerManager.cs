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
    [SerializeField] public Transform exitPoint; // ExitPoint'i public yap�n
    [SerializeField] private float customerSpacing = 2.0f; // M��teriler aras�ndaki mesafe
    private float currentTime = 0f;
    private float spawnTime;
    private List<Customer> customerList = new List<Customer>();

    private void Awake()
    {
        Instance = this;
        spawnTime = Random.Range(spawnRateMin, spawnRateMax); // �lk spawn zaman� belirleme
    }

    private void Start()
    {
        // Prefab'lar�n do�ru �ekilde atand���n� kontrol et
        if (customerPrefabs == null || customerPrefabs.Count == 0)
        {
            Debug.LogError("Customer prefabs list is empty or null. Please assign prefabs in the inspector.");
            return;
        }
    }

    private void Update()
    {
        if (customerPrefabs == null || customerPrefabs.Count == 0) return; // Prefab listesi bo�sa g�ncellemeyi durdur

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
        Vector3 spawnPos = spawnPoint.position + (spawnPoint.forward * -1 * customerList.Count * customerSpacing);
        Customer temp = Instantiate(customerPrefabs[Random.Range(0, customerPrefabs.Count)], spawnPos, spawnPoint.rotation);
        customerList.Add(temp);
    }

    public void SellToCustomer()
    {
        if (customerList.Count == 0) return;

        // ��k�� yapan m��teriyi belirle
        Customer firstCustomer = customerList[0]; // �lk m��teriyi al
        if (firstCustomer != null)
        {
            firstCustomer.ExitFromArea(exitPoint.position);
        }

        // Listeden ��k�� yapan m��teriyi ��kar
        customerList.RemoveAt(0);

        // Geri kalan m��terilerin pozisyonlar�n� g�ncelle
        UpdateCustomerPositions();

        // Yeni bir m��teri olu�tur
        SpawnCustomer();
    }

    private void UpdateCustomerPositions()
    {
        for (int i = 0; i < customerList.Count; i++)
        {
            if (customerList[i] != null)
            {
                Vector3 nextPos = spawnPoint.position + (spawnPoint.forward * -1 * i * customerSpacing);
                customerList[i].MoveNext(nextPos);
            }
        }
    }
}
