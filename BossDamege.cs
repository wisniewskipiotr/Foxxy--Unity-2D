using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamege : MonoBehaviour
{

    // Use this for initialization
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameControlScript.health -= 1;
            Destroy(gameObject);
        }

    }
}
