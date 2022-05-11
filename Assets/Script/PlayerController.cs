using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]private Rigidbody2D rb;
    [SerializeField] private Animator anim;

    public Collider2D coll;
    public Collider2D Discoll;
    public float speed;
    public float jumpforce;
    public LayerMask ground;
    public Transform cellingCheck, grounfCheck;

    public Text cherryNum;
    private int cherry;

    private bool isHurt;
    private bool isGround;

    private int extraJump;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!isHurt)
        {
            Movement();
        }       
        SwitchAnim();
        isGround = Physics2D.OverlapCircle(grounfCheck.position, 0.2f, ground);
    }

    private void Update()
    {
        Jump();
        Crouch();
    }

    //����ƶ�����
    private void Movement()
    {
        float horizontalmove = Input.GetAxis("Horizontal");
        float facedirection = Input.GetAxisRaw("Horizontal");

        //��ɫ�ƶ�
        if (horizontalmove != 0)
        {
            rb.velocity = new Vector2(horizontalmove * speed * Time.fixedDeltaTime, rb.velocity.y);
            anim.SetFloat("running", Mathf.Abs(facedirection));
        }

        //��ɫת��
        if(facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
        }

        //��ɫ��Ծ
        Jump();

        //��ɫ�¶�
        Crouch();
    }

    //�����л�
    private void SwitchAnim()
    {        
        if(rb.velocity.y < 0.1f && !coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", true);
        }
        if (anim.GetBool("jumping"))
        {           
            if(rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }           
        }
        else if (isHurt)
        {
            anim.SetBool("hurt",true);
            if(Mathf.Abs(rb.velocity.x) < 0.1)
            {
                anim.SetBool("hurt", false);               
                isHurt = false;
            }
        }
        else if (coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", false);            
        }

    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�ռ���ײ���
        if (collision.tag == "Collection")
        {
            collision.GetComponent<Animator>().Play("getCherry");
        }
        //����
        if(collision.tag == "DeadLine")
        {
            Invoke("ReStart", 2f);
        }
    }


    //������ײ���
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();      
            if (anim.GetBool("falling"))
            {
                enemy.JumpOn();
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
                anim.SetBool("jumping", true);
                extraJump = 1;
            }
            else if(transform.position.x < collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-5, rb.velocity.y);
                isHurt = true;
            }
            else
            {
                rb.velocity = new Vector2(5, rb.velocity.y);
                isHurt = true;
            }
        }
    }

    //�¶�
    private void Crouch()
    {
        if (!Physics2D.OverlapCircle(cellingCheck.position, 0.2f, ground))
        {
            if (Input.GetButton("Crouch") && !isHurt)
            {
                anim.SetBool("crouching", true);
                Discoll.enabled = false;
            }
            else
            {
                anim.SetBool("crouching", false);
                Discoll.enabled = true;
            }
        }       
    }

    //��Ծ
    /*
    private void Jump()
    {
        if (Input.GetButton("Jump") && coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x * Time.deltaTime, jumpforce);
            anim.SetBool("jumping", true);
        }
    }
    */
    private void Jump()
    {
        if (isGround)
        {
            extraJump = 1;
        }
        if(Input.GetButtonDown("Jump") && extraJump > 0 && !isHurt)
        {
            rb.velocity = Vector2.up * jumpforce;
            extraJump--;
            anim.SetBool("jumping", true);
        }
    }

    //���¿�ʼ
    private void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CountCherry()
    {
        cherry += 1;
        cherryNum.text = cherry.ToString();
    }
}
