using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour
{

    GameObject player;
    GameObject joyStick;
    GameObject Camera;
    Vector3 cameraRELtarget;

    // Use this for initialization
    void Start()
    {
        joyStick = GameObject.Find("joyStick");
        player = GameObject.Find("player");
        Camera = GameObject.Find("Main Camera");
        cameraRELtarget = Camera.transform.position - player.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        cameraFellow();
        playerControl();
        joyStickControl();
    }
    void joyStickControl()
    {
        if (joyStick.GetComponent<joyStick>().touch)
        {
            Vector2 joyStickVec = joyStick.GetComponent<joyStick>().joyStickVec;
            if (joyStickVec.x < 0)
            {
                player.GetComponent<player>().left(joyStickVec.x);
            }
            else
            {
                player.GetComponent<player>().right(joyStickVec.x);
            }
        }
    }
    void cameraFellow()
    {
        Camera.transform.position = cameraRELtarget + player.transform.position;
    }

    void playerControl()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            player.GetComponent<player>().pressA();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            player.GetComponent<player>().releaseA();
        }
        if (Input.GetKey("a"))
        {
            player.GetComponent<player>().left(1);
        }
        if (Input.GetKey("d"))
        {
            player.GetComponent<player>().right(1);
        }



        if (Input.GetKeyDown("space"))
        {
            player.GetComponent<player>().jump();
        }
        if (Input.GetKeyUp("space"))
        {
            player.GetComponent<player>().fall();
        }

    }


}