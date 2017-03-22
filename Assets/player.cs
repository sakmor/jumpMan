using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class player : MonoBehaviour
{
    public bool IsGrounded;
    public int score;
    public float speedUP;
    public bool isTake;
    GameObject[] allBlock;
    GameObject[] allTakeable;
    GameObject targetObj;
    GameObject nTargetObj;
    Transform lastTargetObj;
    Vector3 lastTargetObjScale;
    // Use this for initialization
    void Start()
    {
        isTake = false;
        speedUP = 1;
        allBlock = GameObject.FindGameObjectsWithTag("block"); ;
        allTakeable = GameObject.FindGameObjectsWithTag("blockTake");
        IsGrounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        jumpThroughHead();
        animatorState();
        velocityText();
        targetSerach();

    }

    void targetSerach()
    {
        if (isTake == false)
        {

            float dist = Mathf.Infinity;
            foreach (var t in allTakeable)
            {
                if (t.GetComponent<Renderer>().isVisible)
                {
                    float temp = Vector3.Distance(t.transform.position, transform.position);
                    if (temp < dist)
                    {
                        dist = temp;
                        targetObj = t;
                    }
                }
            }
            if (targetObj)
            {
                lastTargetObj = targetObj.transform.parent;
                lastTargetObjScale = targetObj.transform.localScale;

            }

        }

    }
    void velocityText()
    {
        GameObject.Find("velocityText").GetComponent<UnityEngine.UI.Text>().text = this.GetComponent<Rigidbody>().velocity.ToString("F1");

    }
    public void jump()
    {
        if (IsGrounded)
            this.GetComponent<Rigidbody>().AddForce(Vector3.up * 160);
    }

    public void fall()
    {
        Vector3 temp3 = this.GetComponent<Rigidbody>().velocity;
        if (temp3.y > 0)
            this.GetComponent<Rigidbody>().velocity = new Vector3(temp3.x, temp3.y * 0.5f, temp3.z);

    }
    public void take()
    {
        if (isTake)
        {
            //throw
            this.GetComponent<Animator>().Play("idel", -1, 0f);
            isTake = false;
            nTargetObj.transform.parent = lastTargetObj;
            nTargetObj.transform.tag = "blockTake";
            nTargetObj.transform.localScale = lastTargetObjScale;
            nTargetObj.AddComponent<Rigidbody>();
            nTargetObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;
            nTargetObj.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
            nTargetObj.GetComponent<Rigidbody>().AddForce(Vector3.up * 200);

            if (nTargetObj.GetComponent<m101>())
            {
                nTargetObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
                nTargetObj.GetComponent<m101>().enabled = true;
                nTargetObj.GetComponent<Animator>().enabled = true;
            }

            if (this.GetComponent<SpriteRenderer>().flipX)
            {
                nTargetObj.GetComponent<Rigidbody>().AddForce(Vector3.left * 100);
            }
            else
            {
                nTargetObj.GetComponent<Rigidbody>().AddForce(Vector3.right * 100);
            }
            //加入空心球機制
            nTargetObj.AddComponent<ball>();

            //更新清單
            allTakeable = GameObject.FindGameObjectsWithTag("blockTake");

        }
        else
        {
            //take
            if (targetObj
            && Vector3.Distance(transform.position, targetObj.transform.position) < 1.5f)
            {

                // Reset 
                targetObj.GetComponent<Rigidbody>().velocity = Vector3.zero;
                targetObj.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                targetObj.GetComponent<Rigidbody>().isKinematic = true;
                targetObj.transform.rotation = Quaternion.identity;
                targetObj.GetComponent<Rigidbody>().isKinematic = false;

                //disable m101
                if (targetObj.GetComponent<m101>())
                {
                    targetObj.GetComponent<m101>().enabled = false;
                    targetObj.GetComponent<Animator>().enabled = false;
                }


                // Copy
                nTargetObj = Instantiate(targetObj);
                nTargetObj.transform.tag = "Untagged";
                Destroy(nTargetObj.GetComponent<Rigidbody>());
                nTargetObj.transform.parent = transform;
                if (targetObj.name == "Sphere")
                {
                    nTargetObj.name = "Sphere";
                    nTargetObj.transform.localPosition = Vector3.zero + Vector3.up * 0.35f;
                }
                else
                {
                    nTargetObj.transform.localPosition = Vector3.zero + Vector3.up * 0.25f;
                }
                Destroy(targetObj);

                // this.GetComponent<Animator>().Play("take", -1, 0f);
                isTake = true;
            }
        }
    }
    public void left(float n)
    {
        n = Mathf.Abs(n);
        Vector3 temp3 = this.GetComponent<Rigidbody>().velocity;
        this.GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Clamp(temp3.x, -4, 4) * n, temp3.y, temp3.z);

        if (this.GetComponent<player>().IsGrounded)
        {
            this.GetComponent<Rigidbody>().AddForce(Vector3.left * 5);
            this.GetComponent<Animator>().SetInteger("state", 1);
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            this.GetComponent<Rigidbody>().AddForce(Vector3.left * 3F);
        }
        if (this.GetComponent<Rigidbody>().velocity.x > 0)
        {
            this.GetComponent<Animator>().SetInteger("state", 4);
        }

    }
    public void right(float n)
    {
        n = Mathf.Abs(n);
        Vector3 temp3 = this.GetComponent<Rigidbody>().velocity;
        this.GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Clamp(temp3.x, -4, 4) * n, temp3.y, temp3.z);

        if (IsGrounded)
        {
            this.GetComponent<Rigidbody>().AddForce(Vector3.right * 5);
            this.GetComponent<Animator>().SetInteger("state", 1);
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            this.GetComponent<Rigidbody>().AddForce(Vector3.right * 3f);
        }
        if (this.GetComponent<Rigidbody>().velocity.x < 0)
        {
            this.GetComponent<Animator>().SetInteger("state", 4);
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
    public void brakes()
    {
        // Debug.Log("brakes");
        // this.GetComponent<Rigidbody>().velocity *= 0.5f;
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
            if (GetComponent<Rigidbody>().velocity.x > -0.5f
            && GetComponent<Rigidbody>().velocity.x < 0.5f
            && GetComponent<Animator>().GetInteger("state") != 5)
            {
                //如果在地面:0-idel
                GetComponent<Animator>().SetInteger("state", 0);
            }
            else
            {
                // GetComponent<Animator>().SetInteger("state", 1);
                GetComponent<Animator>().speed = 0.5f + Mathf.Abs(GetComponent<Rigidbody>().velocity.x) / 5;
            }


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

        //碰到blockTake時
        if (collision.gameObject.tag == "blockTake")
        {
            IsGrounded = true;
        }
    }

    //OnCollisionEnter:當player離開碰到的collision時(一次性)...
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == targetObj)
        {
            targetObj = null;
        }
        //當離開block時
        if (collision.gameObject.tag == "block")
        {
            IsGrounded = false;
        }
    }
}
