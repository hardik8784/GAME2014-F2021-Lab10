using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Touch Input")]
    public Joystick Joystick;

    [Header("Movement")] 
    public float horizontalForce;
    public float verticalForce;
    public bool isGrounded;
    public Transform groundOrigin;
    public float groundRadius;
    public LayerMask groundLayerMask;

    [Range(0.1f,0.9f)]
    public float AirControlFactor;

    [Header("Animation")]
    public PlayerAnimationState State;

    private Rigidbody2D rigidbody;
    private Animator AnimatorController;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        AnimatorController = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        CheckIfGrounded();
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal") + Joystick.Horizontal;
        if (isGrounded)
        {
            //float deltaTime = Time.deltaTime;

            // Keyboard Input
           
            float y = Input.GetAxisRaw("Vertical") + Joystick.Vertical;
            float jump = Input.GetAxisRaw("Jump") + ((UIController.JumpButtonDown) ? 1.0f : 0.0f);

            // Check for Flip

            if (x != 0)
            {
                x = FlipAnimation(x);
                AnimatorController.SetInteger("AnimationState", (int) PlayerAnimationState.RUN);     //RUN State
                State = PlayerAnimationState.RUN;
            } 
            else
            {
                AnimatorController.SetInteger("AnimationState", (int)PlayerAnimationState.IDLE);     //IDLE State
                State = PlayerAnimationState.IDLE;
            }
            
            //// Touch Input
            //Vector2 worldTouch = new Vector2();
            //foreach (var touch in Input.touches)
            //{
            //    worldTouch = Camera.main.ScreenToWorldPoint(touch.position);
            //}

            float horizontalMoveForce = x * horizontalForce;// * deltaTime;
            float jumpMoveForce = jump * verticalForce; // * deltaTime;

            float mass = rigidbody.mass * rigidbody.gravityScale;


            rigidbody.AddForce(new Vector2(horizontalMoveForce, jumpMoveForce) * mass);
            rigidbody.velocity *= 0.99f; // scaling / stopping hack
        }
        else //Air Control
        {
            AnimatorController.SetInteger("AnimationState", (int) PlayerAnimationState.JUMP);     //JUMP State
            State = PlayerAnimationState.JUMP;

            if (x != 0)
            {
                x = FlipAnimation(x);

                float horizontalMoveForce = x * horizontalForce * AirControlFactor;        // * deltaTime;
                

                float mass = rigidbody.mass * rigidbody.gravityScale;


                rigidbody.AddForce(new Vector2(horizontalMoveForce, 0.0f) * mass);

            } 
        }

    }

    private void CheckIfGrounded()
    {
        RaycastHit2D hit = Physics2D.CircleCast(groundOrigin.position, groundRadius, Vector2.down, groundRadius, groundLayerMask);

        isGrounded = (hit) ? true : false;
    }

    private float FlipAnimation(float x)
    {
        // depending on direction scale across the x-axis either 1 or -1
        x = (x > 0) ? 1 : -1;

        transform.localScale = new Vector3(x, 1.0f);
        return x;
    }


    // UTILITIES

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundOrigin.position, groundRadius);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(collision.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(null);
        }
    }
}
