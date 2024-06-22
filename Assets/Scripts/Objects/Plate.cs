using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class Plate : MonoBehaviour
{

    private Item item;
    private ItemType type;
    private Inventory inventory;
    private bool isReady = false;
    private bool isDoneService = false;
    private bool isDestroyed = false;

    private ActionController controller;

    void Start()
    {
  
        GameObject controllerObject = GameObject.Find("Player"); 
        if (controllerObject != null)
        {
            controller = controllerObject.GetComponent<ActionController>();
            inventory = controller.inventory;
        }
    }

    void Update()
    {

      
        
    }

    

    private void ServiceTrigger()
    {
        if (inventory.CurrentType == ItemType.NONE) return;
        {
           
        }
    }


}



