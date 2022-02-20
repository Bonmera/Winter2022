using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{

    [SerializeField] private float k_BaseSpeed = 40f;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    [SerializeField] private GameManager gameManager;

    private Rigidbody2D m_Rigidbody2D;
    private float k_HorizontalSpeed = 0f;
    private float k_VerticalSpeed = 0f;
    private Vector3 m_CurrVelocity = Vector3.zero;
    private Vector2 screenBounds;
    private Vector2 playerOffset;
    private Vector2 cameraOffset;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        playerOffset.x = transform.GetComponent<SpriteRenderer>().bounds.size.x/2;
        playerOffset.y = transform.GetComponent<SpriteRenderer>().bounds.size.y/2;
        Vector2 cameraOffset = Camera.main.transform.position;
        screenBounds.x =Mathf.Abs(  cameraOffset.x - screenBounds.x);
        screenBounds.y =Mathf.Abs( cameraOffset.y - screenBounds.y);
    }

    // Update is called once per frame
    void Update()
    {
        CheckMoveInput();
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    private void CheckMoveInput()
    {
        k_HorizontalSpeed = Input.GetAxisRaw("Horizontal") * k_BaseSpeed;
        k_VerticalSpeed = Input.GetAxisRaw("Vertical") * k_BaseSpeed;
    }

    private void MoveCharacter()
    {
        Vector3 targetVelocity = new Vector2(k_HorizontalSpeed , k_VerticalSpeed );
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_CurrVelocity, m_MovementSmoothing);
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(
                                        Mathf.Clamp(transform.position.x, Camera.main.transform.position.x  + -1*screenBounds.x + playerOffset.x , Camera.main.transform.position.x + screenBounds.x - playerOffset.x ), 
                                        Mathf.Clamp(transform.position.y, Camera.main.transform.position.y  - screenBounds.y + playerOffset.y , Camera.main.transform.position.y + screenBounds.y - playerOffset.y ), 
                                        transform.position.z);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Exit"))
        {
            gameManager.SwitchRoom(collision.collider);
        }
    }

}
