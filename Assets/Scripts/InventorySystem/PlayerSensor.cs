using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSensor : MonoBehaviour
{
    private interactable focus = null;
    private HashSet<Collider2D> inventoryInstances = new HashSet<Collider2D>();


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && focus!=null)
        {
            focus.Interact();
        }
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "InventoryItem")
        {
            if (focus == null)
            {
                inventoryInstances.Add(collision);
                focus = collision.gameObject.GetComponent<interactable>();
                focus.OnFocused(transform);
            }
            else
            {
                if (collision.gameObject.GetComponent<interactable>() != focus)
                {
                    if (Vector2.Distance(transform.position, collision.gameObject.transform.position) < Vector2.Distance(transform.position, focus.transform.position))
                    {
                        focus.OnDefocused();
                        focus = collision.gameObject.GetComponent<interactable>();
                        focus.OnFocused(transform);
                    }
                    else
                    {
                        inventoryInstances.Add(collision);
                    }
                }
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "InventoryItem")
        {

            if (focus != null)
            {
                inventoryInstances.Remove(collision);
                focus.OnDefocused();
                focus = null;
            }

            if (inventoryInstances.Count > 0)
            {
                float minDist = float.MaxValue;
                GameObject closestObject = null;
                foreach (Collider2D collider in inventoryInstances)
                {
                    if (Vector2.Distance(transform.position, collider.transform.position) < minDist)
                    {
                        minDist = Vector2.Distance(transform.position, collider.transform.position);
                        closestObject = collider.gameObject;
                    }
                }

                focus = closestObject.GetComponent<interactable>();
                focus.OnFocused(transform);

            }

        }
    }
}
