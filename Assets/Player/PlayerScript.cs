using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public Vector2 pVelocity;
    public float moveSpeed;
    public float jumpSpeed;
    public float gravity;
    public float jumpHeight;
    public GroundCheck ground;
    public Rigidbody2D playerRigidBody;

    private bool isGround = false;
    private bool isJump = false;
    private bool isCokeJump = false;
    private float jumpPos = 0.0f;
    private bool isPreSpace;

    private Vector3 prePlayerPos;

    public GameObject DebugPoint;
    public GameObject DebugPoint2;

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

        pVelocity.x = 0;

        if (!isGround)
        {
            pVelocity.y += -gravity;
        }

        if (Input.GetKey(KeyCode.D))
        {
            pVelocity.x = moveSpeed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            pVelocity.x = -moveSpeed;
        }

        if (isGround)
        {
            pVelocity.y = 0;

            if (Input.GetKey(KeyCode.Space) && !isPreSpace)
            {
                pVelocity.y = jumpSpeed;
                jumpPos = transform.position.y;
                isJump = true;
            }
            else
            {
                isJump = false;
            }

        }
        else if (isCokeJump)
        {
            if (Input.GetKey(KeyCode.Space) && jumpPos + jumpHeight > transform.position.y)
            {
                pVelocity.y = jumpSpeed;
            }
            else
            {
                isCokeJump = false;
            }

        }
        else if (isJump)
        {
            if (Input.GetKey(KeyCode.Space) && jumpPos + jumpHeight > transform.position.y)
            {
                pVelocity.y = jumpSpeed;
            }
            else
            {
                isJump = false;
            }

        }

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
            if (collision.tag == "Choke")
            {
                float playerBottom = prePlayerPos.y - this.gameObject.GetComponent<Renderer>().bounds.size.y / 2;
                float tragetTop = collision.transform.position.y + collision.gameObject.GetComponent<Renderer>().bounds.size.y / 2;

                DebugPoint.transform.position = new Vector3 (prePlayerPos.x, playerBottom, prePlayerPos.z);
                DebugPoint2.transform.position = new Vector3(collision.transform.position.x, tragetTop, collision.transform.position.z);

                //�v���C���[�̈ʒu(����)���`���[�N(���)���ォ�Ŕ�����Ƃ�
                if (playerBottom >= tragetTop)
                {
                    canJump = true;
                    collision.GetComponent<ChokeScript>().GeneratePowder();
                }
                else
                {
                    //�_���[�W����
                }
            }
            else
            {
                if (collision.GetComponent<PowderScript>().GetCanHitPlayer() == true)
                {
                    canJump = true;
                }
            }

            if (canJump == true)
            {
                pVelocity.y = jumpSpeed;
                jumpPos = transform.position.y;
                isCokeJump = true;

                if (collision.tag == "Choke")
                {
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
