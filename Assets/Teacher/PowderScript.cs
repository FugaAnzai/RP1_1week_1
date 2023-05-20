using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowderScript : MonoBehaviour
{



    //生成されてから経った時間
    private float livingTime = 0;
    //プレイヤーに当たるか。これが無きゃ生成されて即合体!!
    private const float kCanHitPlayerTime = 0.1f;
    private bool canHitPlayer = false;
    

    public bool GetCanHitPlayer()
    {
        return canHitPlayer;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (livingTime >= kCanHitPlayerTime)
        {
            canHitPlayer = true;
        }

        livingTime += Time.deltaTime;


    }

   
   

}