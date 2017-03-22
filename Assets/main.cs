using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour
{
    int score;
    int best;
    float gameTime;
    float gameStartTime;
    GameObject countDownTime;
    GameObject player;
    GameObject joyStick;
    GameObject Camera;
    Vector3 cameraRELtarget;

    // Use this for initialization
    void Start()
    {
        score = 0;
        best = 0;
        gameStartTime = Time.time;
        gameTime = 30;
        countDownTime = GameObject.Find("countDownTime");
        joyStick = GameObject.Find("joyStick");
        player = GameObject.Find("player");
        Camera = GameObject.Find("Main Camera");
        cameraRELtarget = Camera.transform.position - player.transform.position;

    }
    public void throwIn()
    {
        score++;
        if (score > best)
        {
            best = score;
            GameObject.Find("text").GetComponent<UnityEngine.UI.Text>().text = "Best record: " + (best).ToString("F1");
        }
        GameObject.Find("Score").GetComponent<UnityEngine.UI.Text>().text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        countDown();
        cameraFellow();
        playerControl();
        joyStickControl();
    }
    void countDown()
    {
        float gameOverTime = gameTime - (Time.time - gameStartTime);
        countDownTime.GetComponent<UnityEngine.UI.Text>().text = (gameOverTime).ToString("F1");
        if (gameOverTime <= 0)
        {
            score = 0;
            GameObject.Find("Score").GetComponent<UnityEngine.UI.Text>().text = score.ToString();
            gameStartTime = Time.time;
        }

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
        if (Input.GetKeyDown("b"))
        {
            player.GetComponent<player>().take();
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