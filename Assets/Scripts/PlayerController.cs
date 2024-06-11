using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float m_velocity = 1;
    [SerializeField] Transform m_overlapCirclePosition;
    [SerializeField] float m_overlapRadius;
    [SerializeField] LayerMask m_groundLayer;
    [SerializeField] bool isGrounded;

    Animator m_animator;
    private SpriteRenderer m_spriteRender;
    private Rigidbody2D m_body;
    private bool m_wasGroundedOnPreviousFrame;

    // Start is called before the first frame update
    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_spriteRender = GetComponent<SpriteRenderer>();
        m_body = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(m_overlapCirclePosition.position,m_overlapRadius,m_groundLayer);
       
        if (isGrounded )
        {
            m_animator.SetTrigger("onGroundTrigger");
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            m_animator.SetTrigger("JumpTrigger");
            m_body.AddForce(Vector2.up * 8f, ForceMode2D.Impulse);
        }
        float InputX = Input.GetAxis("Horizontal");
        bool isMoving = Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f;

        m_spriteRender.flipX = Input.GetAxis("Horizontal") < 0;
        m_animator.SetBool("RunTrigger", isMoving);
        m_body.velocity = new Vector2(InputX * m_velocity, m_body.velocity.y);
        m_wasGroundedOnPreviousFrame = isGrounded;
        m_animator.SetFloat("VerticalVelocity", m_body.velocity.y);
    }
}