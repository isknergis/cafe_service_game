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
    [SerializeField] public Transform exitPoint; // ExitPoint'i public yapýn
    [SerializeField] private float customerSpacing = 2.0f; // Müþteriler arasýndaki mesafe
    private float currentTime = 0f;
    private float spawnTime;
    private List<Customer> customerList = new List<Customer>();

    private void Awake()
    {
        Instance = this;
        spawnTime = Random.Range(spawnRateMin, spawnRateMax); // Ýlk spawn zamaný belirleme
    }

    private void Start()
    {
        // Prefab'larýn doðru þekilde atandýðýný kontrol et
        if (customerPrefabs == null || customerPrefabs.Count == 0)
        {
            Debug.LogError("Customer prefabs list is empty or null. Please assign prefabs in the inspector.");
            return;
        }
    }

    private void Update()
    {
        if (customerPrefabs == null || customerPrefabs.Count == 0) return; // Prefab listesi boþsa güncellemeyi durdur

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
        Vector3 spawnPos = spawnPoint.position + (spawnPoint.forward * -1 * customerList.Count * customerSpacing);
        Customer temp = Instantiate(customerPrefabs[Random.Range(0, customerPrefabs.Count)], spawnPos, spawnPoint.rotation);
        customerList.Add(temp);
    }

    public void SellToCustomer()
    {
        if (customerList.Count == 0) return;

        // Çýkýþ yapan müþteriyi belirle
        Customer firstCustomer = customerList[0]; // Ýlk müþteriyi al
        if (firstCustomer != null)
        {
            firstCustomer.ExitFromArea(exitPoint.position);
        }

        // Listeden çýkýþ yapan müþteriyi çýkar
        customerList.RemoveAt(0);

        // Geri kalan müþterilerin pozisyonlarýný güncelle
        UpdateCustomerPositions();

        // Yeni bir müþteri oluþtur
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
