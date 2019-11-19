using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerScript_ex00 : MonoBehaviour{

    [SerializeField][Range(1, 1000)]
    private float speed;

    private Rigidbody2D rb;

    [SerializeField][Range(1, 1000)]
    private float jumpForce;
    [SerializeField][Range(1, 10)]
    private float fallMultiplier;
    [SerializeField][Range(1, 10)]
    private float lowJumpMultiplier;

    private bool grounded;
    [HideInInspector]
    public bool active;

    void Start(){
        grounded = true;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

	void Update () {
        if (!active)
            return;
    }

    private void FixedUpdate()
    {
        betterJump();

        if (!active)
            return;
        
        if (grounded && Input.GetKeyDown("space"))
        {
            rb.AddForce(Vector2.up * jumpForce);
            grounded = false;
        }
        Vector3 velocity = (Vector2.right * Input.GetAxis("Horizontal")) * speed * Time.fixedDeltaTime;
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;
        
    }

    void betterJump()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey("space"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.gameObject.tag == "Ground" || col.collider.gameObject.tag == "Player")
        {
            ContactPoint2D contact = col.contacts[0];
            if (Vector2.Dot(contact.normal, Vector2.up) > 0.1)
                grounded = true;
        }
    }
}
