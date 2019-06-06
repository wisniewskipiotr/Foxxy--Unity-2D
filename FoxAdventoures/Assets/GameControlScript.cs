using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControlScript : MonoBehaviour
{


    public GameObject health1, health2, health3;
    public static int health;

    // Use this for initialization
    void Start()
    {
        health = 3;
        //health1.gameObject.SetActive(true);
        //health2.gameObject.SetActive(true);
        //health3.gameObject.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        switch (health)
        {
            case 3:
                health1.gameObject.SetActive(true);
                health2.gameObject.SetActive(true);
                health3.gameObject.SetActive(true);
                break;
            case 2:
                health1.gameObject.SetActive(true);
                health2.gameObject.SetActive(true);
                health3.gameObject.SetActive(false);
                break;
            case 1:
                health1.gameObject.SetActive(true);
                health2.gameObject.SetActive(false);
                health3.gameObject.SetActive(false);
                break;
            case 0:
                health1.gameObject.SetActive(false);
                health2.gameObject.SetActive(false);
                health3.gameObject.SetActive(false);


                break;
        }

    }
}
