using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float speed = 1.0f;
    public int health = 1;
    Vector2 knockback = new Vector2(5.0f,5.0f);
    bool direction;
    public Collider2D g1,g2;
    void Start()
    {
        if (transform.localScale.x < 0)
        {
            direction = true;
        }
        else
            direction = false;
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), g1);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), g2);

    }


    void Update()
    {
        if (health == 0)
        {
            Destroy(gameObject);
        }
        if (direction)
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        else
            transform.Translate(Vector2.left * speed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.name == "Blade" && Input.GetKey(KeyCode.LeftControl))
        {

            health--;
            GetComponent<Rigidbody2D>().AddForce(knockback, ForceMode2D.Impulse);
        }
    }

}
