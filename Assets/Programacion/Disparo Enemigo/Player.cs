using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed;
    public float jump;

    private float move;

    public Rigidbody2D rb;


    const float groundCheckRadius = 0.2F;

    [SerializeField] Transform groundCheckCollider;
    public bool isGrounded = false;
    [SerializeField] LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        GroundCheck();
        move = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(speed * move, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jump));

        }

    }

    void GroundCheck()
    {
        isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position,groundCheckRadius, groundLayer);
        if(colliders.Length > 0)
        {
            isGrounded = true;

        }
    }

}
