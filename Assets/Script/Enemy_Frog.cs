using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Frog : Enemy
{
    private Rigidbody2D rb;
    //private Animator anim;
    public Transform leftPoint, rightPoint;
    public Collider2D coll;
    public LayerMask ground;
    private float leftx, rightx;
    public float speed,jumpForce;

    private bool isFaceLeft = true;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        leftx = leftPoint.position.x;
        rightx = rightPoint.position.x;
        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);
    }

    private void Update()
    {
        SwitchAnim();
    }

    //¿ØÖÆÒÆ¶¯
    private void Movement()
    {
        if (isFaceLeft)
        {
            if (transform.position.x < leftx)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                isFaceLeft = false;
            }
            else if (coll.IsTouchingLayers(ground))
            {
                rb.velocity = new Vector2(-speed, jumpForce);
                anim.SetBool("jumping", true);                
            }           
            
        }
        else
        {
            if (transform.position.x > rightx)
            {
                transform.localScale = new Vector3(1, 1, 1);
                isFaceLeft = true;
            }
            else if (coll.IsTouchingLayers(ground))
            {
                rb.velocity = new Vector2(speed, jumpForce);
                anim.SetBool("jumping", true);              
            }            
        }
    }

    private void SwitchAnim()
    {
        if(anim.GetBool("jumping") && rb.velocity.y < 0.1)
        {
            anim.SetBool("jumping", false);
            anim.SetBool("falling", true);
        }
        if (coll.IsTouchingLayers(ground) && anim.GetBool("falling"))
        {
            anim.SetBool("falling", false);
        }
    }


}
