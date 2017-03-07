using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class player : MonoBehaviour
{
    public bool IsGrounded;
    public int score;
    public float speedUP;
    GameObject[] allBlock;
    // Use this for initialization
    void Start()
    {
        speedUP = 1;
        allBlock = GameObject.FindGameObjectsWithTag("block");
        IsGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        jumpThroughHead();
        animatorState();
    }
    public void jump()
    {
        if (IsGrounded)
            this.GetComponent<Rigidbody>().AddForce(Vector3.up * 500);
    }

    public void fall()
    {
        this.GetComponent<Rigidbody>().velocity *= 0.5f;

    }
    public void right()
    {
        this.GetComponent<Rigidbody>().position += Vector3.right * 0.15f * speedUP;
        if (IsGrounded)
        {
            this.GetComponent<Animator>().SetInteger("state", 1);
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    public void pressA()
    {
        if (IsGrounded)
        {
            speedUP = 2f;
        }

    }
    public void releaseA()
    {
        if (IsGrounded)
        {
            speedUP = 1f;
        }

    }
    public void left()
    {
        this.GetComponent<Rigidbody>().position += Vector3.left * 0.15f * speedUP;
        if (this.GetComponent<player>().IsGrounded)
        {
            this.GetComponent<Animator>().SetInteger("state", 1);
            this.GetComponent<SpriteRenderer>().flipX = true;
        }

    }
    //animatorState: 依據player的各種狀態，判斷現在對應的動畫狀態
    void animatorState()
    {
        if (!IsGrounded)
        {
            //如果在空中，且正在往上升:2-jump
            if (GetComponent<Rigidbody>().velocity.y > 0)
            {
                GetComponent<Animator>().SetInteger("state", 2);
            }
            else
            //如果在空中，且正在往下降:3-fall
            {
                GetComponent<Animator>().SetInteger("state", 3);
            }
        }

        if (IsGrounded)
        {
            //如果在地面:0-idel
            GetComponent<Animator>().SetInteger("state", 0);
        }
    }

    //讓player可以從下往上穿過block的判斷式
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

    //OnCollisionEnter:當player碰到其他collision時(一次性)...
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

    //OnCollisionEnter:當player持續碰到其他collision時(重複性)...
    void OnCollisionStay(Collision collision)
    {
        //碰到block時
        if (collision.gameObject.tag == "block")
        {
            IsGrounded = true;
        }
    }

    //OnCollisionEnter:當player離開碰到的collision時(一次性)...
    void OnCollisionExit(Collision collision)
    {
        //當離開block時
        if (collision.gameObject.tag == "block")
        {
            IsGrounded = false;
        }
    }
}
