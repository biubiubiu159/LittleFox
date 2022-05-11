using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Eagle : Enemy
{
    private Rigidbody2D rb;
    private Collider2D coll;
    public float speed;
    public Transform top;
    public Transform bottom;
    private float topy, bottomy;

    private bool isUP = true;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        topy = top.position.y;
        bottomy = bottom.position.y;
        Destroy(top.gameObject);
        Destroy(bottom.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (isUP)
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
            if(transform.position.y > topy)
            {
                isUP = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, -speed);
            if(transform.position.y < bottomy)
            {
                isUP = true;
            }
        }
    }
}
