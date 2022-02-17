using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject InventoryPanel;
    
    // Start is called before the first frame update
    void Start()
    {
        InventoryPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            InventoryPanel.SetActive(!InventoryPanel.activeSelf);
        }
    }
}
