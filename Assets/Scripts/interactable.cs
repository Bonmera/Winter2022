using System.Collections;
using UnityEngine;

public class interactable : MonoBehaviour
{
    public float radius = 3f;
    bool isFocus = false;
    Transform player;
    bool hasInteracted = false;
    public Material highlightMaterial;
    public Material defaultMaterial;
    public item item;

    public void Interact()
    {
        bool wasPickedUp = Inventory.instance.Add(item);
        if (wasPickedUp)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("space") && isFocus && !hasInteracted)
        {
            Interact();
            hasInteracted = true;
        }
    }
    
    public void OnFocused(Transform playerTransform)
    {
        StartCoroutine(SetFocus());
        player = playerTransform;
        transform.gameObject.GetComponent<SpriteRenderer>().material = highlightMaterial;
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        transform.gameObject.GetComponent<SpriteRenderer>().material = defaultMaterial;
    }
 
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    IEnumerator SetFocus()
    {
        yield return 0;
        isFocus = true;
    }
}
