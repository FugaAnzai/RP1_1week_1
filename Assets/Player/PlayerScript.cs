using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

   
    public Vector2 pVelocity;
    public float moveSpeed;
    public float jumpSpeed;
    public float chokeJumpSpeed;
    public float gravity;
    public float jumpHeight;
    public int life = 3;
    public GroundCheck ground;
    public Rigidbody2D playerRigidBody;

    private bool isGround = false;
    private bool isJump = false;
    private bool isChokeJump = false;
    private bool isPreSpace;
    private bool isGameOver = false;
    public bool isSleep = true;

    private float jumpPos = 0.0f;
    private float sleepCount = 0.0f;

    private Vector3 prePlayerPos;

    public GameObject DebugPoint;
    public GameObject DebugPoint2;

    public bool GetIsGround()
    {
        return isGround;
    }

    public bool GetIsSleep()
    {
        return isSleep;
    }

    public bool GetIsGameOver()
    {
        return isGameOver;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();

        prePlayerPos = this.transform.position;

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //接地判定を得る
        isGround = ground.IsGround();

        //毎フレームx方向リセット
        pVelocity.x = 0;

        //地面に触れていない間重力加算
        if (!isGround)
        {
            pVelocity.y += -gravity;
        }

        //右移動
        if (Input.GetKey(KeyCode.D))
        {
            pVelocity.x = moveSpeed;
        }

        //左移動
        if (Input.GetKey(KeyCode.A))
        {
            pVelocity.x = -moveSpeed;
        }

        //地面に触れているかつチョークジャンプがオフの時
        if (isGround && !isChokeJump)
        {
            //yの速度をゼロに
            pVelocity.y = 0;

            //ジャンプ処理、長押しでは反応しない
            if (Input.GetKey(KeyCode.Space) && !isPreSpace)
            {
                //y方向の移動ベクトルに代入
                pVelocity.y = jumpSpeed;
                //ジャンプ時のy座標保存
                jumpPos = transform.position.y;
                //ジャンプフラグをtrueに
                isJump = true;
            }
            else
            {
                //押されていない間はジャンプフラグをfalseに
                isJump = false;
            }

        }
        else if (isChokeJump)//チョークジャンプがtrueの時
        {
            //入力がなくてもジャンプ
            if (jumpPos + jumpHeight > transform.position.y)
            {
                //y方向の移動ベクトルに代入
                pVelocity.y = jumpSpeed;
            }
            else
            {
                //ジャンプ上限を超えるとフラグをfalseに
                isChokeJump = false;
            }

        }
        else if (isJump)
        {
            //ジャンプ中にも入力受付、長押しで高く飛べる
            if (Input.GetKey(KeyCode.Space) && jumpPos + jumpHeight > transform.position.y)
            {
                //y方向の移動ベクトルに代入
                pVelocity.y = jumpSpeed;
            }
            else
            {
               
                isJump = false;
            }

        }

        //移動ベクトルが0の時
        if(pVelocity == new Vector2(0, 0))
        {
            sleepCount++;
            
            if(sleepCount > 30)
            {
                isSleep = true;
            }
        }
        else
        {
            sleepCount = 0;
            //動いてるときはスリープフラグをfalseに
            isSleep = false;
        }

        if(life < 0)
        {
            life = 0;
            isGameOver = false;
            Debug.Log("Game Over");
        }

        //rigidbodyの移動ベクトルに代入
        playerRigidBody.velocity = pVelocity;

        isPreSpace = Input.GetKey(KeyCode.Space);
        prePlayerPos = this.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Choke" || collision.tag == "Powder")
        {
            //ジャンプできるかどうか(敵を踏んだかどうか)
            bool canJump = false;
            //チョークなら判定の処理
            if (collision.tag == "Choke" )
            {

                if (collision.GetComponent<ChokeScript>().ReturnCanStep() == true) {
                    float playerBottom = prePlayerPos.y - this.gameObject.GetComponent<Renderer>().bounds.size.y / 2;
                    float tragetTop = collision.transform.position.y + collision.transform.GetChild(0).gameObject.GetComponent<Renderer>().bounds.size.y / 2;
                    tragetTop = collision.ClosestPoint(this.transform.position).y;

                    DebugPoint.transform.position = new Vector3(prePlayerPos.x, playerBottom, prePlayerPos.z);
                    DebugPoint2.transform.position = new Vector3(collision.transform.position.x, tragetTop, collision.transform.position.z);

                    //プレイヤーの位置(下面)がチョーク(上面)より上かで判定をとる
                    if (playerBottom >= tragetTop)
                    {
                        canJump = true;
                        jumpPos = transform.position.y;
                        collision.GetComponent<ChokeScript>().GeneratePowder();
                    }
                    else
                    {
                        life--;
                        Debug.Log("Hit");
                    }
                }
            }
            else
            {
                if (collision.GetComponent<PowderScript>().GetCanHitPlayer() == true)
                {
                    //canJump = true;
                    //Destroy(collision.gameObject);
                }
            }

            if (canJump == true)
            {

                pVelocity.y = jumpSpeed;
                jumpPos = transform.position.y;
                isChokeJump = true;

                if (collision.tag == "Choke")
                {
                    canJump = false;
                    collision.GetComponent<ChokeScript>().StartTurn();
                }
                else
                {
                    Destroy(collision.gameObject);
                }
                //
            }
        }

    }

    

}
