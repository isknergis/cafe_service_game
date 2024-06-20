using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class Plate : MonoBehaviour
{
    private GameObject plate;
    private Item item;
    private ItemType type;
    private Inventory inventory;
    private bool isReady = false;
    private bool isDoneService = false;
    private bool isDestroyed = false;

    private ActionController controller;

    void Start()
    {
        plate = gameObject; // gameObject doðrudan bu scriptin bulunduðu GameObject'i iþaret eder.
        GameObject controllerObject = GameObject.Find("Player"); // Buraya ActionController'ýn bulunduðu GameObject'in adýný yazýn
        if (controllerObject != null)
        {
            controller = controllerObject.GetComponent<ActionController>();
            inventory = controller.inventory;
        }
    }

    void Update()
    {
        PlatePreparation();
        DestroyPlate();
        
    }

    public void PlatePreparation()
    {


        if (controller.itemTaken)
        {
            if (!isReady && !isDoneService && !isDestroyed)
            {
                if ((inventory.CurrentType == ItemType.LEMONADE && inventory.CurrentType == ItemType.ORANGEJUÝCE && inventory.CurrentType == ItemType.BURGER)&& controller == null && controller.itemTaken)
                {
                    plate.SetActive(false);
                    isReady = true;
                }
                else if (inventory.CurrentType != ItemType.CUPCAKEREDVELVET && inventory.CurrentType != ItemType.CHEESECAKESTRAW && inventory.CurrentType != ItemType.BREAD)
                {
                    plate.SetActive(true);
                    isReady = true;
                }
            }
        }
        else if(!controller.itemTaken)
        {
            plate.SetActive(false);
        }
    }

    private void DestroyPlate()
    {
        if ((inventory.CurrentType != ItemType.CUPCAKEREDVELVET && inventory.CurrentType != ItemType.CHEESECAKESTRAW && inventory.CurrentType != ItemType.BREAD) && isDoneService)
        {
            plate.SetActive(false);
            isDestroyed = true;
        }
    }


}
