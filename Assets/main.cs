using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour
{

    GameObject player;
    GameObject Camera;
    Vector3 cameraRELtarget;

    // Use this for initialization
    void Start()
    {

        player = GameObject.Find("player");
        Camera = GameObject.Find("Main Camera");
        cameraRELtarget = Camera.transform.position - player.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        cameraFellow();
        playerControl();

    }
    void cameraFellow()
    {
        Camera.transform.position = cameraRELtarget + player.transform.position;
    }

    void playerControl()
    {
        if (Input.GetKeyDown("space")
            && player.GetComponent<player>().IsGrounded)
        {
            player.GetComponent<Rigidbody>().AddForce(Vector3.up * 500);
        }
        if (Input.GetKey("a"))
        {
            if (player.GetComponent<player>().IsGrounded)
            {
                player.GetComponent<Animator>().SetInteger("state", 1);
                player.GetComponent<SpriteRenderer>().flipX = true;
            }

            player.GetComponent<Rigidbody>().position += Vector3.left * 0.15f;
        }
        if (Input.GetKeyUp("a"))
        {
            if (player.GetComponent<player>().IsGrounded)
            {
                player.GetComponent<Animator>().SetInteger("state", 4);
                player.GetComponent<SpriteRenderer>().flipX = true;
            }

            player.GetComponent<Rigidbody>().position += Vector3.left * 0.15f;
        }
        if (Input.GetKey("d"))
        {
            if (player.GetComponent<player>().IsGrounded)
            {
                player.GetComponent<Animator>().SetInteger("state", 1);
                player.GetComponent<SpriteRenderer>().flipX = false;
            }
            player.GetComponent<Rigidbody>().position += Vector3.right * 0.15f;
        }

        if (Input.GetKeyUp("space")
            && !player.GetComponent<player>().IsGrounded
            && player.GetComponent<Rigidbody>().velocity.y > 0)
        {
            player.GetComponent<Rigidbody>().velocity *= 0.5f;
        }

    }


}