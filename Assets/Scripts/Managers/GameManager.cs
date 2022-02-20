using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject transitionPanel;
    public GameObject InventoryPanel;
    public GameObject protagRoom;
    public GameObject basicRoom;
    public GameObject kitchenRoom;
    public GameObject janitorClosetRoom;
    public GameObject storageRoom;
    public GameObject grooveRoom;
    public GameObject player;
    public GameObject DialogueBox;

    private TransitionScript transition;
    private AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        transition = transitionPanel.GetComponent<TransitionScript>();
        InventoryPanel.SetActive(false);
        DialogueBox.SetActive(false);
        audioManager = AudioManager.instance;
        if(audioManager == null)
        {
            Debug.LogError("audioManager is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            InventoryPanel.SetActive(!InventoryPanel.activeSelf);
        }
    }

    public void SwitchRoom(string originRoom)
    {
        switch (originRoom)
        {
            case "protagRoom":
                //Entering Kitchen
                // ChangeRoom(janitorClosetRoom);
                //Entering Janitor Closet
                // ChangeRoom(janitorClosetRoom);
                //Entering Storage Room
                // ChangeRoom(storageRoom);
                //Entering Groove
                ChangeRoom(grooveRoom);

                //Entering BasicRoom
                /*transition.StartTransition();
                Vector2 startPosition = basicRoom.transform.Find("startPoint").gameObject.transform.position;
                player.transform.position = new Vector3(startPosition.x,startPosition.y,player.transform.position.z) ;
                Camera.main.transform.position = new Vector3(startPosition.x,startPosition.y,Camera.main.transform.position.z);
                Camera.main.GetComponent<cameraMovement>().UpdateBounds(basicRoom.transform.Find("boundaries").gameObject.GetComponent<BoxCollider2D>());
*/
                break;
        }
    }

    public void ChangeRoom(GameObject room)
    {
        transition.StartTransition();
        Vector2 startPosition = room.transform.Find("startPoint").gameObject.transform.position;
        player.transform.position = new Vector3(startPosition.x, startPosition.y, player.transform.position.z);
        Camera.main.transform.position = new Vector3(startPosition.x, startPosition.y, Camera.main.transform.position.z);
        Camera.main.GetComponent<cameraMovement>().UpdateBounds(room.transform.Find("boundaries").gameObject.GetComponent<BoxCollider2D>());
    }

}
