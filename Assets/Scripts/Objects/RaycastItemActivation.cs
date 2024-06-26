using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastItemActivation : MonoBehaviour
{
    public GameObject[] itemsToActivate;
 
    public GameObject objectToDetect;

    void Start()
    {
        objectToDetect.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        { 
            Debug.Log("StartProcessAction called.");
            Ray ray = new Ray(transform.position + Vector3.up / 2, transform.forward);
      
            if (Physics.Raycast(ray, out RaycastHit hit, 1))
            {
            
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