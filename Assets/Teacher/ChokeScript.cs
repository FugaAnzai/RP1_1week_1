using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChokeScript : MonoBehaviour
{
    public GameObject ChokeGra;
    public float moveSpeed;
    public GameObject powderPrefab;
    public Rigidbody2D ThisRigidbody2D;
    private GameObject player;
    private PlayerScript playerScript;

    //ÉXÉçÉEå„ÇÃë¨ìx
    public float slowedMoveSpeed;

    public float kTurnSpeedMax = 20;
    public float kTurnAcc = 1;

    private Vector2 velocity = new Vector2(-1,0);

    private float turnSpeed;
    private bool powderSlow = false;
    private bool isTurn = false;
    private bool isTurnMoved = false;
    private float turnStartTime = 0.0f;
    private float turnStartTimeStart = 0.5f;

    private bool canStepPlayer = true;
    private bool isGround;
    private bool isPowderHit = false;

    private float Rot = 0.0f;

    private bool summonedPowder = false;

    private Vector3 teacherPos = Vector3.zero;

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
        Debug.Log("delete");
    }

    public bool GetIsTurn()
    {
        return isTurn;
    }

    // Start is called before the first frame update

    public bool ReturnCanStep()
    {
        return canStepPlayer;
    }
   
    void Start()
    {

        velocity = velocity * moveSpeed;
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerScript>();

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
            if (turnStartTime >= turnStartTimeStart && isTurnMoved == true)
            {

                Vector3 ThisPos = this.transform.position;
                Vector3 turnVector = teacherPos - ThisPos;
                Rot = Mathf.Atan2(turnVector.y, turnVector.x) * Mathf.Rad2Deg;
                ChokeGra.transform.localEulerAngles = new Vector3(0.0f, 0.0f, Rot);

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

                ChokeGra.transform.localEulerAngles = new Vector3(0.0f, 0.0f, Rot);

                turnStartTime += Time.deltaTime;
                Rot -= 45.0f;

                if (playerScript.GetIsGround())
                {
                    isTurnMoved = true;
                }
            }
        }

        ThisRigidbody2D.velocity = velocity;
    }

    public void GeneratePowder()
    {
        if (summonedPowder == false)
        {
            Instantiate(powderPrefab, new Vector3(this.transform.position.x + 2, this.transform.position.y, this.transform.position.z), Quaternion.identity);
            Instantiate(powderPrefab, new Vector3(this.transform.position.x - 2, this.transform.position.y, this.transform.position.z), Quaternion.identity);

            summonedPowder = true;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (isTurn == false)
        {
            if (collision.tag == "Powder" && !isPowderHit)
            {
                powderSlow = true;
                isPowderHit = true;
                Destroy(collision.gameObject);


            }


        }
        else
        {
            if (collision.tag == "Teacher")
            {
                //Destroy(this.gameObject);
            }
        }
    }

    public void StartTurn()
    {
        GameObject theacher = GameObject.FindGameObjectWithTag("Teacher");

        teacherPos = theacher.transform.position;

        isTurn = true;
        //canStepPlayer = false;

    }
}
