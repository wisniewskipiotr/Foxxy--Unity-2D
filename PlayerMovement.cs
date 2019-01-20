using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;
    public CapsuleCollider2D capbox;

    public float runSpeed = 40f;
    public Text scoreText;


    float horizontalMove = 0f;
    bool jump = false;
    bool IsHurt = false;

    void Start()
    {

        // Grab the components in case they weren't loaded properly
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController2D>();

        // initialize the start values
        jump = false;
        IsHurt = false;

        
    }

        // Update is called once per frame
        void Update()
    {

        scoreText.text = PlayerStats.Score.ToString();

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
                PlayerStats.Score++;
            }

            else
            {

                jump = true;

                animator.SetBool("IsHurt", true);
                capbox.enabled = false;
                StartCoroutine(AwaitMenuScreen());

            }
        }
        if (col.gameObject.tag == "Teleport")

        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        }

    }

    IEnumerator AwaitMenuScreen()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
    }
    void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        
    }
}