using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TransitionMapping
{
    public GameObject entryRoom;
    public BoxCollider2D exitCollider;
    public Transform entryPoint;
}


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
    public GameObject firstFloorHallway;
    public GameObject player;
    public GameObject DialogueBox;

    [SerializeField]
    private TransitionMapping[] Transitions;

    private Dictionary<Collider2D, TransitionMapping> TransitionMap;

    private TransitionScript transition;
    private AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        TransitionMap = new Dictionary<Collider2D, TransitionMapping>();
        transition = transitionPanel.GetComponent<TransitionScript>();
        InventoryPanel.SetActive(false);
        DialogueBox.SetActive(false);
        audioManager = AudioManager.instance;
        if(audioManager == null)
        {
            Debug.LogError("audioManager is null");
        }

        for(int i = 0; i < Transitions.Length; i++)
        {
            TransitionMap.Add(Transitions[i].exitCollider, Transitions[i]);
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

    public void SwitchRoom(Collider2D door)
    {
        if(TransitionMap.ContainsKey(door))
        {
            ChangeRoom(TransitionMap[door]);
        }
    }

    public void ChangeRoom(TransitionMapping t)
    {
        transition.StartTransition();
        Vector2 startPosition = t.entryPoint.position;
        player.transform.position = new Vector3(startPosition.x, startPosition.y, player.transform.position.z);
        Camera.main.transform.position = new Vector3(startPosition.x, startPosition.y, Camera.main.transform.position.z);
        Camera.main.GetComponent<cameraMovement>().UpdateBounds(t.entryRoom.transform.Find("boundaries").gameObject.GetComponent<BoxCollider2D>());
    }

}
