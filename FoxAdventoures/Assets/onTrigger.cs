using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag == "Enemy")
        {
            Destroy(col.gameObject);
            PlayerStats.Score++;

        }


    }
}