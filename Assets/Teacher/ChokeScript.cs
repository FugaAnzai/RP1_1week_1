using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChokeScript : MonoBehaviour
{
    public float moveSpeed;
    public GameObject powderPrefab;
    public Rigidbody2D ThisRigidbody2D;

    //�X���E��̑��x
    public float slowedMoveSpeed;

    public float kTurnSpeedMax = 20;
    public float kTurnAcc = 1;

    private Vector2 velocity = new Vector2(-1,0);

    private float turnSpeed;
    private bool powderSlow = false;
    private bool isTurn = false;
    
    private Vector3 theacherPos = Vector3.zero;
    

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
        Debug.Log("delete");
    }

    // Start is called before the first frame update

   
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
            Vector3 ThisPos = this.transform.position;

            Vector3 turnVector = theacherPos - ThisPos;
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

        Vector3 ThisPos = this.transform.position;
        Vector3 turnVector = theacherPos - ThisPos;
        float Rot = Mathf.Atan2(turnVector.y, turnVector.x) * Mathf.Rad2Deg;
        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Rot);
    }
}