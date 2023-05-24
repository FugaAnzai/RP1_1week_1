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

        //�ڒn����𓾂�
        isGround = ground.IsGround();

        //���t���[��x�������Z�b�g
        pVelocity.x = 0;

        //�n�ʂɐG��Ă��Ȃ��ԏd�͉��Z
        if (!isGround)
        {
            pVelocity.y += -gravity;
        }

        //�E�ړ�
        if (Input.GetKey(KeyCode.D))
        {
            pVelocity.x = moveSpeed;
        }

        //���ړ�
        if (Input.GetKey(KeyCode.A))
        {
            pVelocity.x = -moveSpeed;
        }

        //�n�ʂɐG��Ă��邩�`���[�N�W�����v���I�t�̎�
        if (isGround && !isChokeJump)
        {
            //y�̑��x���[����
            pVelocity.y = 0;

            //�W�����v�����A�������ł͔������Ȃ�
            if (Input.GetKey(KeyCode.Space) && !isPreSpace)
            {
                //y�����̈ړ��x�N�g���ɑ��
                pVelocity.y = jumpSpeed;
                //�W�����v����y���W�ۑ�
                jumpPos = transform.position.y;
                //�W�����v�t���O��true��
                isJump = true;
            }
            else
            {
                //������Ă��Ȃ��Ԃ̓W�����v�t���O��false��
                isJump = false;
            }

        }
        else if (isChokeJump)//�`���[�N�W�����v��true�̎�
        {
            //���͂��Ȃ��Ă��W�����v
            if (jumpPos + jumpHeight > transform.position.y)
            {
                //y�����̈ړ��x�N�g���ɑ��
                pVelocity.y = jumpSpeed;
            }
            else
            {
                //�W�����v����𒴂���ƃt���O��false��
                isChokeJump = false;
            }

        }
        else if (isJump)
        {
            //�W�����v���ɂ����͎�t�A�������ō�����ׂ�
            if (Input.GetKey(KeyCode.Space) && jumpPos + jumpHeight > transform.position.y)
            {
                //y�����̈ړ��x�N�g���ɑ��
                pVelocity.y = jumpSpeed;
            }
            else
            {
               
                isJump = false;
            }

        }

        //�ړ��x�N�g����0�̎�
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
            //�����Ă�Ƃ��̓X���[�v�t���O��false��
            isSleep = false;
        }

        if(life < 0)
        {
            life = 0;
            isGameOver = false;
            Debug.Log("Game Over");
        }

        //rigidbody�̈ړ��x�N�g���ɑ��
        playerRigidBody.velocity = pVelocity;

        isPreSpace = Input.GetKey(KeyCode.Space);
        prePlayerPos = this.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Choke" || collision.tag == "Powder")
        {
            //�W�����v�ł��邩�ǂ���(�G�𓥂񂾂��ǂ���)
            bool canJump = false;
            //�`���[�N�Ȃ画��̏���
            if (collision.tag == "Choke" )
            {

                if (collision.GetComponent<ChokeScript>().ReturnCanStep() == true) {
                    float playerBottom = prePlayerPos.y - this.gameObject.GetComponent<Renderer>().bounds.size.y / 2;
                    float tragetTop = collision.transform.position.y + collision.transform.GetChild(0).gameObject.GetComponent<Renderer>().bounds.size.y / 2;
                    tragetTop = collision.ClosestPoint(this.transform.position).y;

                    DebugPoint.transform.position = new Vector3(prePlayerPos.x, playerBottom, prePlayerPos.z);
                    DebugPoint2.transform.position = new Vector3(collision.transform.position.x, tragetTop, collision.transform.position.z);

                    //�v���C���[�̈ʒu(����)���`���[�N(���)���ォ�Ŕ�����Ƃ�
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
