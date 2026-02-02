using System.Collections;
using UnityEngine;

public class PlayerItemCollector : MonoBehaviour
{
    private InventoryController inventoryController;
    private bool canPickUp = true; //cooldown flag
    [SerializeField] private float pickupCooldown = 0.1f;

    void Start()
    {
        inventoryController = FindObjectOfType<InventoryController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canPickUp) return; // ignore during cooldown

        if (collision.CompareTag("Item"))
        {
            Item item = collision.GetComponent<Item>();
            if (item != null)
            {
                bool itemAdded = inventoryController.AddItem(collision.gameObject);

                if (itemAdded)
                {
                    item.ShowPopUp();
                    Destroy(collision.gameObject);

                    StartCoroutine(PickupCooldownRoutine());
                }
            }
        }
    }

    private IEnumerator PickupCooldownRoutine()
    {
        canPickUp = false;
        yield return new WaitForSeconds(pickupCooldown);
        canPickUp = true;
    }
}
