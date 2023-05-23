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
    }

    public WAVE wave = WAVE.SetWave1;

    public GameObject chokePrefab;
    public GameObject player;
    public GameObject ground;

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

   //�`���[�N�𒼐���Ɍ���
    private IEnumerator StraightChoke()
    {
        //�R���[�`������
        while (true)
        {
            Vector3 shotPos = this.transform.position;
            shotPos.y = -3.5f;
            GameObject choke = Instantiate(chokePrefab, shotPos, Quaternion.identity);
            //�e����������w��b���R���[�`����~
            yield return new WaitForSeconds(bulletCoolDown);

        }

    }

    //Wave1
    private void SetWave1()
    {
        //�R���[�`���X�^�[�g
        StartCoroutine("StraightChoke");
    }
    private void Wave1()
    {
        if (Input.GetKey(KeyCode.N) || playerScript.GetIsSleep() == false)
        {
            //N���������v���C���[���N����ƍU�����I�t
            isAttack = false;
            //initCount���[���ɂ���@init���Ԃ����Z�b�g
            initCount = 0;
            //�������甭�ˊԊu�𒷂�����
            bulletCoolDown = 5.0f;
        }

        if(isAttack == false && playerScript.GetIsSleep())
        {
            //�v���C���[������ƍU�����I��
            isAttack = true;
            //�~�܂����甭�ˊԊu��Z������
            bulletCoolDown = 2.0f;
        }

    }

    //�K�i��U����i
    private IEnumerator Stair2Attack()
    {
        initPosY = new float[2];
        initPosY[0] = -3.0f;//�ꔭ��
        initPosY[1] = -2.0f;//�񔭖�

        while (true)
        {
            Vector3 shotPos = this.transform.position;
            //initCount�ō��W�����炷
            shotPos.y = initPosY[initCount];
            GameObject choke = Instantiate(chokePrefab, shotPos, Quaternion.identity);
            //���̒e���Ă�܂ŃR���[�`����~
            yield return new WaitForSeconds(0.3f);

            initCount++;

            if (initCount > 1)
            {
                initCount = 0;
                Debug.Log("hogege");
                //�e���ő吔������������w��b���R���[�`����~
                yield return new WaitForSeconds(bulletCoolDown);
            }

        }
    }

    //Wave2

    private void SetWave2()
    {
        initCount = 0;
        isAttack = true;
        //Wave1�̃R���[�`����~
        StopCoroutine("StraightChoke");
        //Wave2�̃R���[�`���X�^�[�g
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
        }

    }

    private void SetWave3()
    {
        initCount = 0;
        isAttack = true;
        //Wave2�̃R���[�`����~
        StopCoroutine("StraightChoke");
        //Wave3�̃R���[�`���X�^�[�g
        StartCoroutine("Stair2Choke");
    }

    private void Wave3()
    {
        if (playerScript.GetIsSleep() == false)
        {
            isAttack = false;
            bulletCoolDown = 4.0f;
        }

        if (isAttack == false && playerScript.GetIsSleep())
        {
            isAttack = true;
            bulletCoolDown = 2.0f;
        }
    }

    private void SetWave4()
    {
        initCount = 0;
        isAttack = true;
        //Wave3�̃R���[�`����~
        StopCoroutine("Stair2Choke");
        //Wave4�̃R���[�`���X�^�[�g
        StartCoroutine("StraightChoke");
    }

    private void Wave4()
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
        }
    }

    private void SetWave5()
    {
        initCount = 0;
        isAttack = true;
        //Wave1�̃R���[�`����~
        StopCoroutine("StraightChoke");
        //Wave2�̃R���[�`���X�^�[�g
        StartCoroutine("StraightChoke");
    }

    private void Wave5()
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
        }
    }

    private void SetWave6()
    {
        initCount = 0;
        isAttack = true;
        //Wave1�̃R���[�`����~
        StopCoroutine("StraightChoke");
        //Wave2�̃R���[�`���X�^�[�g
        StartCoroutine("StraightChoke");
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
        }
    }

    private void SetWave7()
    {
        initCount = 0;
        isAttack = true;
        //Wave1�̃R���[�`����~
        StopCoroutine("StraightChoke");
        //Wave2�̃R���[�`���X�^�[�g
        StartCoroutine("StraightChoke");
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
        }
    }

    private void SetWave8()
    {
        initCount = 0;
        isAttack = true;
        //Wave1�̃R���[�`����~
        StopCoroutine("StraightChoke");
        //Wave2�̃R���[�`���X�^�[�g
        StartCoroutine("StraightChoke");
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
            //F�L�[�ŃE�F�[�u�؂�ւ�
            wave++;
        }

        switch (wave)
        {
            case WAVE.SetWave1:
                SetWave1();
                wave++;
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
        }
    }
}
