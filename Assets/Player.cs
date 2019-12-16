using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {
    public float WalkSpeed;
    public float JumpForce;
    public Transform Blade, GroundCast;
    public Camera cam;

    private bool mirror;

    private bool canJump, canWalk;
    private bool isWalk, isJump;
    private float startScale;
    private Rigidbody2D rig;
    private Vector2 inputAxis;
    private RaycastHit2D hit;

    public Collider2D g1;
    public int health = 5;

    void Start ()
    {
        rig = gameObject.GetComponent<Rigidbody2D>();
        startScale = transform.localScale.x;
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), g1);   
    }

    void Update()
    {
        
        Vector3 myScale = transform.localScale;
        bool rotate = false;

        if (health == 0)
        {
            Destroy(gameObject);
        }

        if (cam.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x + 0.2f)
            mirror = false;
        if (cam.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x - 0.2f)
            mirror = true;

        if (Input.GetAxis("Horizontal") < 0 && myScale.x > 0)
        {
            rotate = true;

        }
        else if (Input.GetAxis("Horizontal") > 0 && myScale.x < 0)
        {
            rotate = true;
        }
        if (rotate)
        {
            myScale.x = myScale.x * -1;
            transform.localScale = myScale;
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            GetComponent<Animation>().Play();
        }

        if (hit = Physics2D.Linecast(new Vector2(GroundCast.position.x, GroundCast.position.y + 0.2f), GroundCast.position))
        {
            if (!hit.transform.CompareTag("Player"))
            {
                canJump = true;
                canWalk = true;
            }
        }
        else canJump = false;

        inputAxis = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (inputAxis.y > 0 && canJump)
        {
            canWalk = false;
            isJump = true;
        }
    }

    void FixedUpdate()
    {
        if (cam.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x + 0.2f)
            mirror = false;
        if (cam.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x - 0.2f)
            mirror = true;

    
        if (inputAxis.x != 0)
            rig.velocity = new Vector2(inputAxis.x * WalkSpeed * Time.deltaTime, rig.velocity.y);
        else
             rig.velocity = new Vector2(0, rig.velocity.y);
        
        if (isJump)
        {
            rig.AddForce(new Vector2(0, JumpForce));
            canJump = false;
            isJump = false;
        }
    }

    public bool IsMirror()
    {
        return mirror;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "enemy")
        {
            Vector2 knockback = new Vector2(-10, 7.0f);

            health--;
            rig.AddForce(knockback, ForceMode2D.Impulse);
            

        }
    }


}

