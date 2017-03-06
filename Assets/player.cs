using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class player : MonoBehaviour
{
    public bool IsGrounded;
    public int score;
    GameObject[] allBlock;
    // Use this for initialization
    void Start()
    {
        allBlock = GameObject.FindGameObjectsWithTag("block");
        IsGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        jumpThroughHead();

    }
    void jumpThroughHead()
    {
        if (!IsGrounded)
        {
            if (GetComponent<Rigidbody>().velocity.y > 0)
            {
                foreach (var i in allBlock)
                {
                    if (i.transform.position.y > transform.position.y)
                    {
                        Physics.IgnoreCollision(i.GetComponent<Collider>(), GetComponent<Collider>());
                    }
                }
            }
            else
            {
                foreach (var i in allBlock)
                {
                    if (i.transform.position.y < transform.position.y)
                    {
                        Physics.IgnoreCollision(i.GetComponent<Collider>(), GetComponent<Collider>(), false);
                    }
                }
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        //碰到star時
        if (collision.gameObject.tag == "star")
        {
            Destroy(collision.gameObject);
            score++;
            GameObject.Find("Score").GetComponent<UnityEngine.UI.Text>().text = score.ToString("F0");
        }

        //碰到block時
        if (collision.gameObject.tag == "block")
        {
            IsGrounded = true;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        //碰到block時
        if (collision.gameObject.tag == "block")
        {
            IsGrounded = true;
        }
    }


    void OnCollisionExit(Collision collision)
    {
        //當離開block時
        if (collision.gameObject.tag == "block")
        {
            IsGrounded = false;
        }
    }
}
