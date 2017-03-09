using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class updown : MonoBehaviour
{
    Vector3 startPos;
    public bool onThis;

    // Use this for initialization
    void Start()
    {
        this.GetComponent<Rigidbody>().freezeRotation = true;
        this.GetComponent<Rigidbody>().useGravity = false;
        onThis = false;
        startPos = this.GetComponent<Rigidbody>().position;

    }

    // Update is called once per frame
    void Update()
    {
        if (!onThis && this.GetComponent<Rigidbody>().position.y > startPos.y)
        {
            this.GetComponent<Rigidbody>().velocity = Vector3.down * 2.0f;
            // transform.position -= Vector3.up * 0.03f;
        }


    }
    void OnCollisionStay(Collision other)
    {
        onThis = true;
        this.GetComponent<Rigidbody>().velocity = Vector3.up * 2.0f;
        // transform.position += Vector3.up * 0.05f;
    }
    void OnCollisionExit(Collision other)
    {
        onThis = false;
    }
}
