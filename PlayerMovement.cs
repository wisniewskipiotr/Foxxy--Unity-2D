using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
<<<<<<< HEAD
using UnityEngine.SceneManagement;



=======

>>>>>>> c37178f8e869fda9489d20ce8e3d12792f441648
public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;
    public CapsuleCollider2D capbox;

    public float runSpeed = 40f;
    public Text scoreText;
<<<<<<< HEAD

=======
    int score;
>>>>>>> c37178f8e869fda9489d20ce8e3d12792f441648

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

<<<<<<< HEAD
        
=======
        score = 0;
>>>>>>> c37178f8e869fda9489d20ce8e3d12792f441648
    }

        // Update is called once per frame
        void Update()
    {

<<<<<<< HEAD
        scoreText.text = PlayerStats.Score.ToString();
=======
        scoreText.text = score.ToString();
>>>>>>> c37178f8e869fda9489d20ce8e3d12792f441648

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }


    }

<<<<<<< HEAD


=======
>>>>>>> c37178f8e869fda9489d20ce8e3d12792f441648
    public void OnLanding()
    {
        jump = false;
        animator.SetBool("IsJumping", false);
    }
<<<<<<< HEAD

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
=======

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
>>>>>>> c37178f8e869fda9489d20ce8e3d12792f441648
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
<<<<<<< HEAD
=======
            void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        
    }
>>>>>>> c37178f8e869fda9489d20ce8e3d12792f441648
}