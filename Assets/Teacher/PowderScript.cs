using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class PowderScript : MonoBehaviour
{
    //��������Ă���o��������
    private float livingTime = 0;
    //�v���C���[�ɓ����邩�B���ꂪ�����ᐶ������đ�����!!
    private const float kCanHitPlayerTime = 0.1f;
    private bool canHitPlayer = false;
    private bool isMove = false;

    private float moveT = 0;

    public const float DeathTime = 20.0f;

    private Vector3 start = Vector3.zero;
    private Vector3 end = Vector3.zero;

    public bool GetCanHitPlayer()
    {
        return canHitPlayer;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        UnityEngine.Debug.Log("powderHit");

        if (collision.tag == "Player" && isMove == false)
        {
            UnityEngine.Debug.Log("powderHit");
            start = this.transform.position;
            end = this.transform.position + new Vector3(1, 0, 0);
            moveT = 0;
            isMove = true;

        }
    }

    // Update is called once per frame
    void Update()
    {

        if (livingTime >= kCanHitPlayerTime)
        {
            canHitPlayer = true;
        }

        livingTime += Time.deltaTime;

        //��莞�Ԍo���������
        if(livingTime >= DeathTime)
        {
            Destroy(this.gameObject);
        }

        if(isMove)
        {
            moveT += 0.01f;
            this.transform.position = Vector3.Lerp(start, end, moveT);

            if(moveT > 1.0f)
            {
                isMove = false;
            }

        }

    }

   
   

}