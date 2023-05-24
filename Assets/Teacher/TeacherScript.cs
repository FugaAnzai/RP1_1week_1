using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TeacherScript : MonoBehaviour
{
    public enum WAVE
    {
        SetWave1,
        Wave1,
        SetWave2,
        Wave2,
        SetWave3,
        Wave3,
        SetWave4,
        Wave4,
        SetWave5,
        Wave5,
        SetWave6,
        Wave6,
        SetWave7,
        Wave7,
        SetWave8,
        Wave8,
        GameClear,
    }

    public WAVE wave = WAVE.SetWave1;

    public GameObject chokePrefab;
    public GameObject player;
    public GameObject ground;
    public GameObject gameStart;

    private PlayerScript playerScript;
    private GroundCheck groundCheck;


    public bool isAttack = true;
    private bool isStartCalculate = false;
    private bool isRunningCoroutine = false;
    private float[] initPosY;
    public int initCount = 0;
    private int hitCount = 0;
    public float bulletCoolDown = 5.0f;
    private const int kWave1ChokeMax = 1;
    private const int kWave2ChokeMax = 2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ChokeScript chokeScript = collision.GetComponent<ChokeScript>();

        if(collision.tag == "Choke" && chokeScript.GetIsTurn())
        {
            hitCount++;
            Destroy(collision.gameObject);
            isStartCalculate = true;
        }
    }

    public bool GetIsStartCalculate()
    {
        return isStartCalculate;
    }

    public void SetIsStartCalculate(bool isStartCalculate_)
    {
        isStartCalculate = isStartCalculate_;
    }

    public int GetHitCount()
    {
        return hitCount;
    }

    public void SetHitCount(int hitCount_)
    {
        hitCount = hitCount_;
    }

   //チョークを直線上に撃つ
    private IEnumerator StraightChoke()
    {
        //コルーチン処理
        while (true)
        {
            Vector3 shotPos = this.transform.position;
            shotPos.y = -3.5f;
            GameObject choke = Instantiate(chokePrefab, shotPos, Quaternion.identity);
            //弾を撃ったら指定秒数コルーチン停止
            yield return new WaitForSeconds(bulletCoolDown);

        }

    }

    //階段状攻撃二段
    private IEnumerator Stair2Choke()
    {
        initPosY = new float[2];
        initPosY[0] = -3.0f;//一発目
        initPosY[1] = -2.0f;//二発目

        while (true)
        {
            Vector3 shotPos = this.transform.position;
            //initCountで座標をずらす
            shotPos.y = initPosY[initCount];
            GameObject choke = Instantiate(chokePrefab, shotPos, Quaternion.identity);
            //次の弾撃てるまでコルーチン停止
            yield return new WaitForSeconds(0.3f);

            initCount++;

            if (initCount > 1)
            {
                initCount = 0;
                //弾を最大数撃ちきったら指定秒数コルーチン停止
                yield return new WaitForSeconds(bulletCoolDown);
            }

        }
    }

    private IEnumerator Stair3ChokeAndReverse()
    {
        initPosY = new float[6];
        initPosY[0] = -3.0f;//一発目
        initPosY[1] = -2.0f;//二発目
        initPosY[2] = -1.0f;//三発目
        initPosY[3] = -1.0f;//四発目
        initPosY[4] = -2.0f;//五発目
        initPosY[5] = -3.0f;//六発目

        while (true)
        {
            Vector3 shotPos = this.transform.position;
            //initCountで座標をずらす
            shotPos.y = initPosY[initCount];
            GameObject choke = Instantiate(chokePrefab, shotPos, Quaternion.identity);
            //次の弾撃てるまでコルーチン停止
            yield return new WaitForSeconds(0.3f);

            initCount++;

            if (initCount == 3)
            {
                //弾を二発撃ったら指定秒数コルーチン停止
                yield return new WaitForSeconds(bulletCoolDown);
            }

            if (initCount > 5)
            {
                initCount = 0;
                //弾を最大数撃ちきったら指定秒数コルーチン停止
                yield return new WaitForSeconds(bulletCoolDown);
            }

        }
    }

    private IEnumerator Stair3BumpyChoke()
    {
        initPosY = new float[6];
        initPosY[0] = -2.0f;//一発目
        initPosY[1] = -3.0f;//二発目
        initPosY[2] = -1.0f;//三発目
        initPosY[3] = -1.0f;//四発目
        initPosY[4] = -3.0f;//五発目
        initPosY[5] = -2.0f;//六発目

        while (true)
        {
            Vector3 shotPos = this.transform.position;
            //initCountで座標をずらす
            shotPos.y = initPosY[initCount];
            GameObject choke = Instantiate(chokePrefab, shotPos, Quaternion.identity);
            if(initCount == 1 || initCount == 3) { 
                
                initCount++;
                shotPos.y = initPosY[initCount];
                GameObject choke2 = Instantiate(chokePrefab, shotPos, Quaternion.identity);
            }
            //次の弾撃てるまでコルーチン停止
            yield return new WaitForSeconds(0.3f);

            initCount++;

            if (initCount == 3)
            {
                //弾を二発撃ったら指定秒数コルーチン停止
                yield return new WaitForSeconds(bulletCoolDown);
            }

            if (initCount > 5)
            {
                initCount = 0;
                //弾を最大数撃ちきったら指定秒数コルーチン停止
                yield return new WaitForSeconds(bulletCoolDown);
            }

        }
    }

    private IEnumerator Stair4Choke()
    {
        initPosY = new float[4];
        initPosY[0] = -3.0f;//一発目
        initPosY[1] = -2.0f;//二発目
        initPosY[2] = -1.0f;//三発目
        initPosY[3] = 0.0f;//四発目

        while (true)
        {
            Vector3 shotPos = this.transform.position;
            //initCountで座標をずらす
            shotPos.y = initPosY[initCount];
            GameObject choke = Instantiate(chokePrefab, shotPos, Quaternion.identity);
            //次の弾撃てるまでコルーチン停止
            yield return new WaitForSeconds(0.3f);

            initCount++;

            if (initCount > 3)
            {
                initCount = 0;
                //弾を最大数撃ちきったら指定秒数コルーチン停止
                yield return new WaitForSeconds(bulletCoolDown);
            }

        }
    }

    private IEnumerator Stair4ChokeAndReverse()
    {
        initPosY = new float[8];
        initPosY[0] = -3.0f;//一発目
        initPosY[1] = -2.0f;//二発目
        initPosY[2] = -1.0f;//三発目
        initPosY[3] = 0.0f;//四発目
        initPosY[4] = 0.0f;//五発目
        initPosY[5] = -1.0f;//六発目
        initPosY[6] = -2.0f;//七発目
        initPosY[7] = -3.0f;//八発目

        while (true)
        {
            Vector3 shotPos = this.transform.position;
            //initCountで座標をずらす
            shotPos.y = initPosY[initCount];
            GameObject choke = Instantiate(chokePrefab, shotPos, Quaternion.identity);
            //次の弾撃てるまでコルーチン停止
            yield return new WaitForSeconds(0.3f);

            initCount++;

            if (initCount == 4)
            {
                //弾を二発撃ったら指定秒数コルーチン停止
                yield return new WaitForSeconds(bulletCoolDown);
            }

            if (initCount > 7)
            {
                initCount = 0;
                //弾を最大数撃ちきったら指定秒数コルーチン停止
                yield return new WaitForSeconds(bulletCoolDown);
            }

        }
    }

    private IEnumerator Stair5ChokeAndBumpy()
    {
        initPosY = new float[10];
        initPosY[0] = -3.0f;//一発目
        initPosY[1] = -2.0f;//二発目
        initPosY[2] = -1.0f;//三発目
        initPosY[3] = 0.0f;//四発目
        initPosY[4] = 1.0f;//五発目
        initPosY[5] = -1.0f;//六発目
        initPosY[6] = -2.0f;//七発目
        initPosY[7] = 0.0f;//八発目
        initPosY[8] = -3.0f;//九発目
        initPosY[9] = 1.0f;//十発目

        while (true)
        {
            Vector3 shotPos = this.transform.position;
            //initCountで座標をずらす
            shotPos.y = initPosY[initCount];
            GameObject choke = Instantiate(chokePrefab, shotPos, Quaternion.identity);
            if (initCount == 6 || initCount == 8)
            {

                initCount++;
                shotPos.y = initPosY[initCount];
                GameObject choke2 = Instantiate(chokePrefab, shotPos, Quaternion.identity);
            }
            //次の弾撃てるまでコルーチン停止
            yield return new WaitForSeconds(0.3f);

            initCount++;

            if (initCount == 5)
            {
                //弾を二発撃ったら指定秒数コルーチン停止
                yield return new WaitForSeconds(bulletCoolDown);
            }

            if (initCount > 9)
            {
                initCount = 0;
                //弾を最大数撃ちきったら指定秒数コルーチン停止
                yield return new WaitForSeconds(bulletCoolDown);
            }

        }
    }

    //Wave1
    private void SetWave1()
    {
        
    }
    private void Wave1()
    {
        if (Input.GetKey(KeyCode.N) || playerScript.GetIsSleep() == false)
        {
            //Nを押すかプレイヤーが起きると攻撃をオフ
            isAttack = false;
            //initCountをゼロにする　init順番をリセット
            initCount = 0;
            //動いたら発射間隔を長くする
            bulletCoolDown = 5.0f;
        }

        if(isAttack == false && playerScript.GetIsSleep())
        {
            //プレイヤーが眠ると攻撃をオン
            isAttack = true;
            //止まったら発射間隔を短くする
            bulletCoolDown = 2.0f;
            StopCoroutine("StraightChoke");
            StartCoroutine("StraightChoke");
        }

    }

   

    //Wave2

    private void SetWave2()
    {
        initCount = 0;
        isAttack = true;
        //Wave1のコルーチン停止
        StopCoroutine("StraightChoke");
        //Wave2のコルーチンスタート
        StartCoroutine("StraightChoke");
    }
    private void Wave2()
    {
        if (playerScript.GetIsSleep() == false)
        {
            isAttack = false;
            bulletCoolDown = 4.0f;
        }

        if (isAttack == false && playerScript.GetIsSleep())
        {
            isAttack = true;
            bulletCoolDown = 0.8f;
            StopCoroutine("StraightChoke");
            StartCoroutine("StraightChoke");
        }

    }

    private void SetWave3()
    {
        initCount = 0;
        isAttack = true;
        //Wave2のコルーチン停止
        StopCoroutine("StraightChoke");
        //Wave3のコルーチンスタート
        StartCoroutine("Stair2Choke");
    }

    private void Wave3()
    {
        if (playerScript.GetIsSleep() == false)
        {
            isAttack = false;
            bulletCoolDown = 5.0f;
        }

        if (isAttack == false && playerScript.GetIsSleep())
        {
            isAttack = true;
            bulletCoolDown = 2.0f;
            StopCoroutine("Stair2Choke");
            StartCoroutine("Stair2Choke");
        }
    }

    private void SetWave4()
    {
        initCount = 0;
        isAttack = true;
        //Wave3のコルーチン停止
        StopCoroutine("Stair2Choke");
        //Wave4のコルーチンスタート
        StartCoroutine("Stair3ChokeAndReverse");
    }

    private void Wave4()
    {
        if (playerScript.GetIsSleep() == false)
        {
            isAttack = false;
            bulletCoolDown = 5.0f;
        }

        if (isAttack == false && playerScript.GetIsSleep())
        {
            isAttack = true;
            bulletCoolDown = 1.5f;
            StopCoroutine("Stair3ChokeAndReverse");
            StartCoroutine("Stair3ChokeAndReverse");
        }
    }

    private void SetWave5()
    {
        initCount = 0;
        isAttack = true;
        //Wave1のコルーチン停止
        StopCoroutine("Stair3ChokeAndReverse");
        //Wave2のコルーチンスタート
        StartCoroutine("Stair3BumpyChoke");
    }

    private void Wave5()
    { 
        if (playerScript.GetIsSleep() == false)
        {
            isAttack = false;
            bulletCoolDown = 12.0f;
        }

        if (isAttack == false && playerScript.GetIsSleep())
        {
            isAttack = true;
            bulletCoolDown = 1.5f;
            StopCoroutine("Stair3BumpyChoke");
            StartCoroutine("Stair3BumpyChoke");
        }
    }

    private void SetWave6()
    {
        initCount = 0;
        isAttack = true;
        //Wave1のコルーチン停止
        StopCoroutine("Stair3BumpyChoke");
        //Wave2のコルーチンスタート
        StartCoroutine("Stair4Choke");
    }

    private void Wave6()
    {
        if (playerScript.GetIsSleep() == false)
        {
            isAttack = false;
            bulletCoolDown = 4.0f;
        }

        if (isAttack == false && playerScript.GetIsSleep())
        {
            isAttack = true;
            bulletCoolDown = 0.8f;
            StopCoroutine("Stair4Choke");
            StartCoroutine("Stair4Choke");
        }
    }

    private void SetWave7()
    {
        initCount = 0;
        isAttack = true;
        //Wave1のコルーチン停止
        StopCoroutine("Stair4Choke");
        //Wave2のコルーチンスタート
        StartCoroutine("Stair4ChokeAndReverse");
    }

    private void Wave7()
    {
        if (playerScript.GetIsSleep() == false)
        {
            isAttack = false;
            bulletCoolDown = 4.0f;
        }

        if (isAttack == false && playerScript.GetIsSleep())
        {
            isAttack = true;
            bulletCoolDown = 0.8f;
            StopCoroutine("Stair4ChokeAndReverse");
            StartCoroutine("Stair4ChokeAndReverse");
        }
    }

    private void SetWave8()
    {
        initCount = 0;
        isAttack = true;
        //Wave1のコルーチン停止
        StopCoroutine("Stair4ChokeAndReverse");
        //Wave2のコルーチンスタート
        StartCoroutine("Stair5ChokeAndBumpy");
    }

    private void Wave8()
    {
        if (playerScript.GetIsSleep() == false)
        {
            isAttack = false;
            bulletCoolDown = 4.0f;
        }

        if (isAttack == false && playerScript.GetIsSleep())
        {
            isAttack = true;
            bulletCoolDown = 0.8f;
            StopCoroutine("Stair5ChokeAndBumpy");
            StartCoroutine("Stair5ChokeAndBumpy");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isAttack = true;
        playerScript = player.GetComponent<PlayerScript>();
        groundCheck = ground.GetComponent<GroundCheck>();
        wave = WAVE.SetWave1;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            //Fキーでウェーブ切り替え
            wave++;
        }

        switch (wave)
        {
            case WAVE.SetWave1:
                SetWave1();
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    //コルーチンスタート
                    StartCoroutine("StraightChoke");
                    gameStart.SetActive(false);
                    wave++;
                }
                break;
            case WAVE.Wave1:
                Wave1();
                break;
            case WAVE.SetWave2:
                SetWave2();
                wave++;
                break;
            case WAVE.Wave2:
                Wave2();
                break;
            case WAVE.SetWave3:
                SetWave3();
                wave++;
                break;
            case WAVE.Wave3:
                Wave3();
                break;
            case WAVE.SetWave4:
                SetWave4();
                wave++;
                break;
            case WAVE.Wave4:
                Wave4();
                break;
            case WAVE.SetWave5:
                SetWave5();
                wave++;
                break;
            case WAVE.Wave5:
                Wave5();
                break;
            case WAVE.SetWave6:
                SetWave6();
                wave++;
                break;
            case WAVE.Wave6:
                Wave6();
                break;
            case WAVE.SetWave7:
                SetWave7();
                wave++;
                break;
            case WAVE.Wave7:
                Wave7();
                break;
            case WAVE.SetWave8:
                SetWave8();
                wave++;
                break;
            case WAVE.Wave8:
                Wave8();
                break;
            case WAVE.GameClear:
                break;
        }
    }
}
