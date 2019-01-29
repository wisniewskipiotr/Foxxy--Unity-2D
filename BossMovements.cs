using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovements : MonoBehaviour
{
    public int life;
    public float DetectionRange;
    public Animator animator;
    public Vector2 offset = new Vector2();
    public Transform[] spots;
    public float speed;
    public GameObject projectile;
    public GameObject Player;
    bool MyCourtineRunnig = false;
    public float projectileSpeed;


    Vector3 playerPosition;





    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag == "Projectile")
        {
            Destroy(col.gameObject);
            life -= 1;
            StartCoroutine(hurt());



        }
        if (col.gameObject.tag == "Player")
        {
            GameControlScript.health--;

        }

        //animator.SetBool("IsHurt", false);
    }
    IEnumerator hurt()
    {
        animator.SetBool("IsHurt", true);
        yield return new WaitForSeconds(0.4f);
        animator.SetBool("IsHurt", false);
    }


    void CheckForDeath()
    {
        if (life < 1)
        {
            animator.SetBool("IsHurt", false);
            animator.SetBool("IsShooting", false);
            StartCoroutine(dead());

        }
    }
    IEnumerator dead()
    {

        animator.SetBool("IsDead", true);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    Vector3 OrientationToPosition(Vector3 destination)
    {
        var xdiff = gameObject.transform.position.x - destination.x;
        return xdiff < 0 ? new Vector3(0, -180, 0) : new Vector3(0, 0, 0);

    }

    bool IsClose()
    {
        var distance = Vector2.Distance(gameObject.transform.position, Player.transform.position);
        return distance < DetectionRange;
    }

    void Run()
    {

        if (!MyCourtineRunnig && IsClose())
        {
            MyCourtineRunnig = true;

            StartCoroutine("boss");



        }
        FollowPlayer();

    }

    void FollowPlayer()
    {
        if (!animator.GetBool("Running"))
        {
            transform.eulerAngles = OrientationToPosition(Player.transform.position);
        }

    }

    // Use this for initialization
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        CheckForDeath();
    }


    IEnumerator boss()
    {
        //Pierwszy Atak

        animator.SetBool("Running", true);
        transform.eulerAngles = OrientationToPosition(spots[0].position);
        while (transform.position.x != spots[0].position.x)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(spots[0].position.x, spots[0].position.y), speed);

            yield return null;
        }
        animator.SetBool("Running", false);
        yield return new WaitForSeconds(1f);
        int i = 0;
        while (i < Random.Range(5, 10))
        {
            animator.SetBool("IsShooting", true);
            GameObject go = (GameObject)Instantiate(projectile, (Vector2)transform.position + offset * (transform.rotation.y < 0 ? 1 : -1), Quaternion.identity);
            go.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed * (transform.rotation.y < 0 ? 1 : -1), 0);
            i++;
            yield return new WaitForSeconds(Random.Range(0.1f, 1f));
        }
        animator.SetBool("IsShooting", false);

        yield return null;

        //SECOND ATTACK

        yield return new WaitForSeconds(1f);
        GetComponent<Rigidbody2D>().isKinematic = true;
        animator.SetBool("IsJumping", true);
        while (transform.position != spots[2].position)
        {

            transform.position = Vector2.MoveTowards(transform.position, spots[2].position, speed * 3);

            yield return null;
        }
        
        yield return new WaitForSeconds(.2f);
        playerPosition = Player.transform.position;

        while (Mathf.Abs(transform.position.x - playerPosition.x) > 0.1)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(playerPosition.x, gameObject.transform.position.y), speed * 10);

            yield return null;
        }
        Debug.Log("End");
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<Rigidbody2D>().gravityScale = 5;

        while (Mathf.Abs(transform.position.y - playerPosition.y) > 1)
        {
            yield return null;
        }
        animator.SetBool("IsJumping", false);
        GetComponent<Rigidbody2D>().gravityScale = 1;
        Debug.Log("End Caroutoiine");
        MyCourtineRunnig = false;
    }
}
