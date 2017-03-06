using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updown : MonoBehaviour
{
    Vector3 startPos;
    bool onThis;

    // Use this for initialization
    void Start()
    {
        onThis = false;
        startPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (!onThis && transform.position.y > startPos.y)
        {
            transform.position -= Vector3.up * 0.03f;
        }


    }
    void OnCollisionStay(Collision other)
    {
        onThis = true;
        transform.position += Vector3.up * 0.05f;
    }
    void OnCollisionExit(Collision other)
    {
        onThis = false;
    }
}
