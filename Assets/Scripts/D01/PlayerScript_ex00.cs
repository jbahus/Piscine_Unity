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

    public bool grounded;
    public int numberRaycast;
    
    //To not collide with himself
    public LayerMask layer;

    private Vector2 size;

    [HideInInspector]
    public bool active;
    public bool contactBottom;

    Animator anim;

    void Start(){
        anim = GetComponent<Animator>();
        size = GetComponent<SpriteRenderer>().size;
        layer = ~layer;
        grounded = true;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        betterJump();

        if (!active)
            return;
        
        if (grounded && Input.GetKeyDown("space"))
        {
            rb.AddForce(Vector2.up * jumpForce);
            anim.SetTrigger("Jump");
        }
        Vector3 velocity = (Vector2.right * Input.GetAxis("Horizontal")) * speed * Time.fixedDeltaTime;
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;
        
    }

    private void checkGround()
    {
        float step = (size.x * transform.localScale.x) / numberRaycast;
        Vector2 startOrigin = new Vector2(transform.position.x - (size.x * transform.localScale.x) / 2, transform.position.y - (size.y * transform.localScale.y) / 2);

        bool OnCastIsGround = false;

        for (int i = 0; i <= numberRaycast; i++)
        {
            Vector2 origin = startOrigin + new Vector2(step * i, 0);
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, 100, layer);
            Debug.DrawLine(origin, hit.point, Color.red);
            if ((hit.point.y - origin.y >= -0.015f) && (hit.collider.tag == "Ground" || hit.collider.tag == "Player"))
                OnCastIsGround = true;
        }
        if (OnCastIsGround)
            grounded = true;
        else
            grounded = false;
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

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.collider.gameObject.tag == "Ground" || coll.collider.gameObject.tag == "Player")
         {
            ContactPoint2D contact = coll.contacts[0];
            if (Vector2.Dot(contact.normal, Vector2.up) > 0.1)
            {
                contactBottom = true;
                grounded = true;
            }
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        checkGround();
    }
}
