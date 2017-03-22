using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwIn : MonoBehaviour
{
    main main;
    public int score;
    int best;
    // Use this for initialization
    void Start()
    {
        main = GameObject.Find("main").GetComponent<main>();
        best = 0;

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Rigidbody>().velocity.y < 0)
        {
            main.throwIn();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("ballIn");


    }
}
