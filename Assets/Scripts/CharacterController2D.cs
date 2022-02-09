using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{

    [SerializeField] private float k_BaseSpeed = 40f;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;

    private Rigidbody2D m_Rigidbody2D;
    private float k_HorizontalSpeed = 0f;
    private float k_VerticalSpeed = 0f;
    private Vector3 m_CurrVelocity = Vector3.zero;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
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
}
