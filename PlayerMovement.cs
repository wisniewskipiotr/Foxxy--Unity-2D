using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;
    public CapsuleCollider2D capbox;

    public float runSpeed = 40f;
    public Text scoreText;
    int score;

    float horizontalMove = 0f;
    bool jump = false;
    bool IsHurt = false;

    void Start()
    {

        
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController2D>();

        
        jump = false;
        IsHurt = false;

        score = 0;
    }

        
        void Update()
    {

        scoreText.text = score.ToString();

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }


    }

    public void OnLanding()
    {
        jump = false;
        animator.SetBool("IsJumping", false);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Spikes")
        {
            jump = true;
            animator.SetBool("IsHurt", true);
            capbox.enabled = false;
        }
        else if (col.gameObject.tag == "Enemy")
        {
            if (jump)
            {
                Destroy(col.gameObject);
                score++;
            }
                
            else
            {
                jump = true;
                animator.SetBool("IsHurt", true);
                capbox.enabled = false;

            }
        }
    }
            void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        
    }
}