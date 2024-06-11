using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float m_velocity = 1;
    [SerializeField] float m_jumpForce = 8;
    [SerializeField] float m_gravityScale = 4f;
    [SerializeField] float m_gravityScaleFalling = 8f;
    [SerializeField] Transform m_overlapCirclePosition;
    [SerializeField] float m_overlapRadius;
    [SerializeField] LayerMask m_groundLayer;
    [SerializeField] bool isGrounded;
    [SerializeField] private float m_jumpMaxCharge;
    public STATE currentState;
    Animator m_animator;
    private SpriteRenderer m_spriteRender;
    private Rigidbody2D m_body;
    private bool m_wasGroundedOnPreviousFrame;
    private bool isJumping;
    private float jumpTimer;
    
    public enum STATE
    {
        idle,
        jumping,
        falling,
        ground,
        running
    }
    // Start is called before the first frame update
    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_spriteRender = GetComponent<SpriteRenderer>();
        m_body = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        currentState = STATE.idle;
    }

    // Update is called once per frame
    void Update()
    {
        m_body.gravityScale = m_gravityScale;

        isGrounded = Physics2D.OverlapCircle(m_overlapCirclePosition.position, m_overlapRadius, m_groundLayer);

        switch (currentState)
        {
            case STATE.idle: 
                

                
                break;
            case STATE.jumping: break;
            case STATE.falling: break;
            case STATE.ground: break;
            case STATE.running: break;
        }




        
        
        // Comprends pas le previous frame.. 
        if (isGrounded && m_wasGroundedOnPreviousFrame == false)
        {
            m_animator.SetTrigger("onGroundTrigger");
        }
        if (m_body.velocity.y < 0)
        {
            m_body.gravityScale = m_gravityScaleFalling;
        }


        // Classic Jump 
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    m_animator.SetTrigger("JumpTrigger");
        //    m_body.AddForce(Vector2.up * m_jumpForce, ForceMode2D.Impulse);
        //}


        // Variable Jump based on timer 
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            isJumping = true;
            jumpTimer = 0f;
        }
        if (isJumping)
        {
            m_body.velocity = new Vector2(m_body.velocity.x,m_jumpForce);
            jumpTimer += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.Space) || jumpTimer >= m_jumpMaxCharge)
        {
            isJumping = false;
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
