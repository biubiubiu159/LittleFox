using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator anim;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Death()
    {
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject);
    }

    public void JumpOn()
    {
        anim.SetTrigger("death");
    }
}
