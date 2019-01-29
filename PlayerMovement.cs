using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class PlayerMovement : MonoBehaviour
{

    public Joystick joystick;// ON MOBILE
    public Joybutton joybutton;// ON MOBILE
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
        //horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        //ON MOBILE
        if (joystick.Horizontal >= .2f)
        {
            horizontalMove = runSpeed;
        }
        else if (joystick.Horizontal <= -.2f)
        {
            horizontalMove = -runSpeed;
        }
        else
        {
            horizontalMove = 0f;
        }
        float verticalMove = joystick.Vertical;

        scoreText.text = PlayerStats.Score.ToString();



        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        // ON MOBILE
        //if (Input.GetButtonDown("Jump"))
        //if (verticalMove >= .5f)
        if (joybutton.Pressed)
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }


    }



    public void OnLanding()
    {

        animator.SetBool("IsJumping", false);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Spikes")
        {
            jump = true;
            animator.SetBool("IsHurt", true);
            capbox.enabled = false;

            StartCoroutine(AwaitMenuScreen());
            PlayerStats.Score = 0;
        }

        if (col.gameObject.tag == "Enemy")
        {
            jump = true;
            animator.SetBool("IsHurt", true);
            capbox.enabled = false;

            StartCoroutine(AwaitMenuScreen());
            PlayerStats.Score = 0;
        }
        if (GameControlScript.health < 1)
        {
            jump = true;
            animator.SetBool("IsHurt", true);
            capbox.enabled = false;
            StartCoroutine(AwaitMenuScreen());
        }
        if (col.gameObject.tag == "Teleport")

        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (col.gameObject.tag == "Teleport2")

        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
        }

    }

    IEnumerator AwaitMenuScreen()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;

    }
}