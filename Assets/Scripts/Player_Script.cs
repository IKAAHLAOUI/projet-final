using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Player_Script : MonoBehaviour
{
    public float moveSpeed = 15f;

    public Rigidbody2D rb2d;
    bool isFacingRight = true;
    public float health = 100f;
    public int steps=0;
    public float stepFatigue = 0.1f;
    Vector2 movement;
    Animator animator;
    public GameObject footstep;
    public Image healthBar;
    public TextMeshProUGUI stepsText;



    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (canmove == false)
            return;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator = GetComponent<Animator>();
        animate();
        flip();

    }

    void FixedUpdate()
    {
        if (canmove == false)
            return;
        rb2d.MovePosition(rb2d.position + movement * moveSpeed);
    }
    void animate()
    {
        if (movement.y == 0 && movement.x == 0)
        {
            animator.SetBool("isIdle", true);
        }
        else
        {
            animator.SetBool("isIdle", false);
            if (movement.x > 0)
            {
                animator.Play("Walk_Left");
                GetComponent<SpriteRenderer>().flipX = false;
            }
            if (movement.x < 0)
            {
                animator.Play("Walk_Left");
                GetComponent<SpriteRenderer>().flipX = true;
            }
            if (movement.y > 0)
            {
                animator.Play("Walk_Up");
            }
            if (movement.y < 0)
            {
                animator.Play("Walk_Down");
            }
        }


    }

    void flip()
    {
        if (movement.x > 0 && !isFacingRight)
        {
            Vector3 scale = transform.localScale;
            scale.x = scale.x * -1;
            transform.localScale = scale;
            isFacingRight = true;
           


        }
        if (movement.x < 0 && isFacingRight)
        {
            Vector3 scale = transform.localScale;
            scale.x = scale.x * -1;
            transform.localScale = scale;
            isFacingRight = false;
        }


    }

    bool isWalking = false;
    public void addSteps()
    {
        
        if(isWalking==false)
        {
            isWalking = true;
            spawnFootstep();
            AudioManager.instance.playWalk();
            steps++;
            health = health - steps * stepFatigue;
            if (health <= 0)
                Die();
            healthBar.fillAmount = health / 100f;
            stepsText.text = "Steps: " + steps.ToString();
        }
        isWalking = false;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="lake")
        {
            steps=0;
            health =100f;
            healthBar.fillAmount = health / 100f;
            stepsText.text = "Steps: " + steps.ToString();
        }
      


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {

            health -= 5f;

            if (health < 0)
            {
                health = 0;
                Die();
            }
            else
                collision.GetComponent<Enemy>().die();

            healthBar.fillAmount = health / 100f;
            GetComponent<Animator>().Play("hurt");
        }
    }
   public bool canmove = true;

     void Die()
    {
        canmove = false;
        GetComponent<Animator>().Play("die 1");
    }
    public void spawnFootstep()
    {
        GameObject stepf = Instantiate(footstep);
        stepf.transform.position = transform.position;
        if (movement.x > 0)
        {
            stepf.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (movement.x < 0)
        {
            stepf.transform.eulerAngles = new Vector3(0, 0, 180);
        }
        if (movement.y > 0)
        {
            stepf.transform.eulerAngles = new Vector3(0, 0, 90);
        }
        if (movement.y < 0)
        {
            stepf.transform.eulerAngles = new Vector3(0, 0, 270);
        }
        Destroy(stepf, 2);
    }
}
