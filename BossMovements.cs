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
    //bool phases = [false, false, false, false];


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Boss")
        {
            
            life--;
            //Destroy(col.gameObject);

        }
        //animator.SetBool("IsHurt", false);
    }

    void death()
    {

        if (life == 1)
        {
            animator.SetBool("IsHurt", true);
            //Destroy(gameObject);
        }
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
            //if(!phases[0])
            //{
            StartCoroutine("boss");
            //}
            

        }
        FollowPlayer();

    }

    void FollowPlayer()
    {
        if(!animator.GetBool("Running"))
        {
            transform.eulerAngles = OrientationToPosition(Player.transform.position);
        }
         
    }

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
    }


    IEnumerator boss()
    {
        //Pierwszy Atak
        {
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
            while (i < Random.Range(5,10))
            {
                animator.SetBool("IsShooting", true);
                GameObject go = (GameObject)Instantiate(projectile, (Vector2)transform.position + offset * (transform.rotation.y < 0 ? 1 : -1), Quaternion.identity);
                go.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed * (transform.rotation.y < 0 ? 1 : -1) , 0);
                i++;
                yield return new WaitForSeconds(Random.Range(0.1f, 1f));
            }
            animator.SetBool("IsShooting", false);
            MyCourtineRunnig = false;
            //phases[0] = true;
            yield return null;
        }




    }
}
