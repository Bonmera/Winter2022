using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DialogueEditor;

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
    public GameObject player;

    [SerializeField]
    private TransitionMapping[] Transitions;
    [SerializeField]
    private NPCConversation controlIntro;
    [SerializeField]
    private NPCConversation[] exitConversations;
    [SerializeField]
    private GameObject exitPanel;
    
    const int exitConversationIndex = 0;
    const int ropeMissingConversationIndex = 1;

    private Dictionary<Collider2D, TransitionMapping> TransitionMap;

    private TransitionScript transition;
    private AudioManager audioManager;
    // Start is called before the first frame update

    void Start()
    {
        TransitionMap = new Dictionary<Collider2D, TransitionMapping>();
        transition = transitionPanel.GetComponent<TransitionScript>();
        InventoryPanel.SetActive(false);
        audioManager = AudioManager.instance;
        if(audioManager == null)
        {
            Debug.LogError("audioManager is null");
        }

        for(int i = 0; i < Transitions.Length; i++)
        {
            TransitionMap.Add(Transitions[i].exitCollider, Transitions[i]);
        }

        ConversationManager.Instance.StartConversation(controlIntro);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            InventoryPanel.SetActive(!InventoryPanel.activeSelf);
        }

        if(ConversationManager.Instance != null && ConversationManager.Instance.IsConversationActive)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                ConversationManager.Instance.PressSelectedOption();
            }
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

    public void TriggerExitDialog()
    {
        bool hasRope = Inventory.instance.CheckIfRopeExists();
        if (hasRope)
        {
            ConversationManager.Instance.StartConversation(exitConversations[exitConversationIndex]);
        }
        else
        {
            ConversationManager.Instance.StartConversation(exitConversations[ropeMissingConversationIndex]);
        }
    }

    public void TriggerGameExit()
    {
        CharacterController2D playerController = GameObject.Find("Player").GetComponent<CharacterController2D>();
        playerController.StopMovement();
        playerController.enabled = false;
        ConversationManager.Instance.EndConversation();
        exitPanel.SetActive(true);
        StartCoroutine(FadeInExit());
    }

    IEnumerator FadeInExit()
    {
        Image image = exitPanel.GetComponent<Image>();
        float time = 2f;
        for (float i = 0; i <= time; i += Time.deltaTime)
        {
            float normalizedVal = i / time;
            // set color with i as alpha
            image.color = new Color(normalizedVal, normalizedVal, normalizedVal, normalizedVal);
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        Transform tf = exitPanel.transform.Find("EndText");
        tf.gameObject.SetActive(true);
        TextMeshProUGUI text = tf.GetComponent<TextMeshProUGUI>();
        Color textColor = text.color;
        time = 2f;
        for (float i = 0; i <= time; i += Time.deltaTime)
        {
            float normalizedVal = i / time;
            // set color with i as alpha
            text.color = new Color(textColor.r, textColor.g,textColor.b, normalizedVal);
            yield return null;
        }
    }

}
