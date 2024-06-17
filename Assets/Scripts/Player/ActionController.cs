using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    private Inventory inventory;
    private Functionality currentFunction;
    private WaitForSeconds takeCooldown;
    private bool isWorking = false;
    private bool isProcessing = false;
    private bool canPut = true;

    private void Awake()
    {
        canPut = true;
        inventory = GetComponent<Inventory>();
        takeCooldown = new WaitForSeconds(1);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            isWorking = true;
            Debug.Log("Mouse button held down.");
            if (!isProcessing)
            {
                StartProcessAction();
            }
            else
            {
                DoProcess();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            isWorking = false;
            Debug.Log("Mouse button released.");
            if (isProcessing)
            {
                currentFunction?.ResetTimer();
                isProcessing = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("DoTakeAction called from Update.");
            DoTakeAction();
        }
    }

    private void StartProcessAction()
    {
        Debug.Log("StartProcessAction called.");
        Ray ray = new Ray(transform.position + Vector3.up / 2, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 1))
        {
            Debug.Log("Raycast hit: " + hit.collider.name);
            if (hit.collider.TryGetComponent<Functionality>(out Functionality itemProcess))
            {
                isProcessing = true;
                currentFunction = itemProcess;
            }
        }
        else
        {
            Debug.Log("Raycast did not hit anything.");
        }
    }

    private void DoProcess()
    {
        if (!isProcessing) return;
        if (!isWorking) return;

        ItemType item = currentFunction.Process();
        if (item != ItemType.NONE)
        {
            currentFunction.ClearObject();
            inventory.TakeItem(item);
            isWorking = false;
        }
    }

    public void DoTakeAction()
    {
        Debug.Log("DoTakeAction called");
        Ray ray = new Ray(transform.position + Vector3.up / 2, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 1))
        {
            Debug.Log("Raycast hit: " + hit.collider.name);
            if (hit.collider.TryGetComponent<IPutItemFull>(out IPutItemFull itemPutBox))
            {
                if (canPut)
                {
                    bool status = itemPutBox.PutItem(inventory.GetItem());
                    if (status)
                    {
                        inventory.PutItem();
                        inventory.ClearHand();
                    }
                }
            }
            if (hit.collider.TryGetComponent<ItemBox>(out ItemBox itemBox))
            {
                Debug.Log("Hit ItemBox: " + itemBox.name);
                inventory.TakeItem(itemBox.GetItem());
                StartCoroutine(canPutCoolDown());
            }
        }
        else
        {
            Debug.Log("Raycast did not hit anything in DoTakeAction.");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (inventory.CurrentType != ItemType.PIZZA) return;
        if (other.gameObject.CompareTag("SellArea"))
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Selling item to customer.");
                //CustomerManager.Instance.SellToCustomer();
                inventory.ClearHand();
            }
        }
    }

    private IEnumerator canPutCoolDown()
    {
        canPut = false;
        yield return takeCooldown;
        canPut = true;
    }
} 
