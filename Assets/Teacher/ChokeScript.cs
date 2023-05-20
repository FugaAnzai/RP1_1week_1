using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChokeScript : MonoBehaviour
{
    public float moveSpeed;
    public GameObject powderPrefab;
    public Rigidbody2D ThisRigidbody2D;

    //ƒXƒƒEŒã‚Ì‘¬“x
    public float slowedMoveSpeed;

    public float kTurnSpeedMax = 20;
    public float kTurnAcc = 1;

    private Vector2 velocity = new Vector2(-1,0);

    private float turnSpeed;
    private bool powderSlow = false;
    private bool isTurn = false;
    private float turnStartTime = 0.0f;
    private float turnStartTimeStart = 0.5f;

    private bool canStepPlayer = true;

    private float Rot = 0.0f;

    private Vector3 theacherPos = Vector3.zero;
    

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
        Debug.Log("delete");
    }

    // Start is called before the first frame update

    public bool ReturnCanStep()
    {
        return canStepPlayer;
    }
   
    void Start()
    {

        velocity = velocity * moveSpeed;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isTurn == false)
        {
            if (powderSlow == true)
            {
                velocity = new Vector2(-1, 0) * slowedMoveSpeed;
            }

            //this.transform.position += new Vector3(velocity.x,velocity.y,0) * Time.deltaTime;      
           
        }
        else
        {
            if (turnStartTime >= turnStartTimeStart)
            {

                Vector3 ThisPos = this.transform.position;
                Vector3 turnVector = theacherPos - ThisPos;
                Rot = Mathf.Atan2(turnVector.y, turnVector.x) * Mathf.Rad2Deg;
                this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Rot);

                turnVector.Normalize();

                if (turnSpeed < kTurnSpeedMax)
                {
                    turnSpeed += kTurnAcc;
                }
                else
                {
                    turnSpeed = kTurnSpeedMax;
                }

                turnVector = turnVector * turnSpeed;

                velocity = turnVector;
            }
            else
            {
                velocity = new Vector3(0.0f,0.0f,0.0f);

                this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Rot);

                turnStartTime += Time.deltaTime;
                Rot -= 45.0f;
            }
        }

        ThisRigidbody2D.velocity = velocity;
    }

    public void GeneratePowder()
    {
        
        Instantiate(powderPrefab, this.transform.position, Quaternion.identity);
        
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (isTurn == false)
        {
            if (collision.tag == "Powder")
            {
                powderSlow = true;
                Destroy(collision.gameObject);


            }


        }
        else
        {
            if (collision.tag == "Teacher")
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void StartTurn()
    {
        GameObject theacher = GameObject.FindGameObjectWithTag("Teacher");

        theacherPos = theacher.transform.position;

        isTurn = true;
        canStepPlayer = false;

    }
}
