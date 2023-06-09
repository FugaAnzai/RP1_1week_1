using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public float KyokaiSenn = 7.8f;
   
    public Vector2 pVelocity;
    public float moveSpeed;
    public float jumpSpeed;
    public float chokeJumpSpeed;
    public float gravity;
    public float jumpHeight;
    public float life = 0;
    public GroundCheck ground;
    public Rigidbody2D playerRigidBody;
    public GameObject gameOver;

    private bool isGround = false;
    private bool isJump = false;
    private bool isChokeJump = false;
    private bool isPreSpace;
    private bool isGameOver = false;
    public bool isSleep = true;

    private float jumpPos = 0.0f;
    private float sleepCount = 0.0f;
    private int damageCount = 0;
    private bool isDamage = false;

    private Vector3 prePlayerPos;

    public GameObject DebugPoint;
    public GameObject DebugPoint2;

    private Animator anime = null;

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

    public float GetLife()
    {
        return life;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();

        prePlayerPos = this.transform.position;
        anime = GetComponent<Animator>();
        gameOver.SetActive(false);
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
        if (Input.GetKey(KeyCode.D) && isGameOver == false)
        {
            pVelocity.x = moveSpeed;
        }

        //左移動
        if (Input.GetKey(KeyCode.A) && isGameOver == false)
        {
            pVelocity.x = -moveSpeed;
        }

        if(isDamage)
        {
            damageCount++;

            if(damageCount > 60)
            {
                damageCount = 0;
                isDamage = false;
            }
        }

        //地面に触れているかつチョークジャンプがオフの時
        if (isGround && !isChokeJump)
        {
            //yの速度をゼロに
            pVelocity.y = 0;

            //ジャンプ処理、長押しでは反応しない
            if (Input.GetKey(KeyCode.Space) && !isPreSpace && isGameOver == false)
            {
                //y方向の移動ベクトルに代入
                pVelocity.y = jumpSpeed;
                //ジャンプ時のy座標保存
                jumpPos = transform.position.y;
                //ジャンプフラグをtrueに
                isJump = true;
                anime.SetTrigger("StepTrigger");
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
            if (Input.GetKey(KeyCode.Space) && jumpPos + jumpHeight > transform.position.y && isGameOver == false)
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

        if(life > 10)
        {
            life = 10;
            isGameOver = true;
            gameOver.SetActive(true);
            Debug.Log("Game Over");
        }

        //rigidbodyの移動ベクトルに代入
        playerRigidBody.velocity = pVelocity;

        if (pVelocity.y > 0.0f)
        {
            anime.SetBool("isJump", true);
            anime.SetBool("isFall", false);
        }
        else if (pVelocity.y < 0.0f)
        {
            anime.SetBool("isJump", false);
            anime.SetBool("isFall", true);
        }

        anime.SetBool("isWalk", false);
        if (isGround == true)
        {
            anime.SetBool("isJump", false);
            anime.SetBool("isFall", false);

            if (pVelocity.x != 0.0f)
            {
                anime.SetBool("isWalk", true);
            }
        }

        if (pVelocity.x > 0.0f)
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (pVelocity.x < 0.0f)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }

        float A = Mathf.Clamp( this.transform.position.x,-KyokaiSenn, KyokaiSenn);
        this.transform.position = new Vector3( A , this.transform.position.y, this.transform.position.z);

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
                    else if(collision.GetComponent<ChokeScript>().GetIsTurn() == false && isDamage == false)
                    {
                        life++;
                        isDamage = true;
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
