using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastItemActivation : MonoBehaviour
{
    public GameObject[] itemsToActivate;
 
    public GameObject objectToDetect; // I��n�n �arpt��� nesne

    void Start()
    {
        objectToDetect.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        { // I��n olu�turma
            Debug.Log("StartProcessAction called.");
            Ray ray = new Ray(transform.position + Vector3.up / 2, transform.forward);
            // I��n �arp��ma kontrol�
            if (Physics.Raycast(ray, out RaycastHit hit, 1))
            {
                // E�er ���n objectToDetect objesine �arparsa
                if (hit.collider.gameObject == itemsToActivate[0] ||
                    hit.collider.gameObject == itemsToActivate[1] 
                )
                    
                {
                    
                        objectToDetect.SetActive(true);
                    return;
                }
                objectToDetect.SetActive(false);
            }
        }
       
    }
}