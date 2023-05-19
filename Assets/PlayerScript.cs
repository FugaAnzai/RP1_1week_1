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
    public Rigidbody2D PlayerRigidBody;

    private bool isGround = false;
    private bool isJump = false;
    private float jumpPos = 0.0f;
    private bool isPreSpace;

    // Start is called before the first frame update
    void Start()
    {
        PlayerRigidBody = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //Ú’n”»’è‚ð“¾‚é
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

        if(isGround)
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

        }else if (isJump)
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

        PlayerRigidBody.velocity = pVelocity;

        isPreSpace = Input.GetKey(KeyCode.Space);

    }

}
