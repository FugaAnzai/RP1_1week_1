using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TeacherScript : MonoBehaviour
{
    private enum WAVE
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
    }

    WAVE wave = WAVE.SetWave1;

    public GameObject chokePrefab;
    public GameObject player;
    
    private PlayerScript playerScript;

    private bool isAttack = true;
    private float[] initPosY;
    public int initCount = 0;
    private int currentWave = 0;

    private const int kWave1ChokeMax = 1;
    private const int kWave2ChokeMax = 2;

    //Wave1
    private IEnumerator Wave1Attack()
    {
        //�R���[�`������
        while (true)
        {

            Vector3 shotPos = this.transform.position;
            shotPos.y = -3.0f;
            GameObject choke = Instantiate(chokePrefab, shotPos, Quaternion.identity);
            //�e�����������b�ԃR���[�`����~
            yield return new WaitForSeconds(2.0f);

        }
    }

    private void SetWave1()
    {
        //�R���[�`���X�^�[�g
        StartCoroutine("Wave1Attack");
    }
    private void Wave1()
    {
        if (Input.GetKey(KeyCode.N) || playerScript.GetIsSleep() == false)
        {
            //N���������v���C���[���N����ƍU�����I�t
            isAttack = false;
            //initCount���[���ɂ���@init���Ԃ����Z�b�g
            initCount = 0;
            //�R���[�`���X�g�b�v
            StopCoroutine("Wave1Attack");
        }

        if(isAttack == false && playerScript.GetIsSleep())
        {
            //�v���C���[������ƍU�����I��
            isAttack = true;
            StartCoroutine("Wave1Attack");
        }

    }

    //Wave2
    private IEnumerator Wave2Attack()
    {

        while (true)
        {
            Vector3 shotPos = this.transform.position;
            //initCount�ō��W�����炷
            shotPos.y = initPosY[initCount];
            GameObject choke = Instantiate(chokePrefab, shotPos, Quaternion.identity);
            //���̒e���Ă�܂ŃR���[�`����~
            yield return new WaitForSeconds(0.3f);

            initCount++;

            if (initCount > kWave2ChokeMax - 1)
            {
                initCount = 0;
                //�e���ő吔�������������b�ԃR���[�`����~
                yield return new WaitForSeconds(2.0f);
            }

        }
    }

    private void SetWave2()
    {
        initPosY = new float[kWave2ChokeMax];
        initPosY[0] = -3.0f;//�ꔭ��
        initPosY[1] = -2.0f;//�񔭖�
        initCount = 0;

        //Wave1�̃R���[�`����~
        StopCoroutine("Wave1Attack");
        //Wave2�̃R���[�`���X�^�[�g
        StartCoroutine("Wave2Attack");
    }
    private void Wave2()
    {
        if (Input.GetKey(KeyCode.N) || playerScript.GetIsSleep() == false)
        {
            isAttack = false;
            initCount = 0;
            StopCoroutine("Wave2Attack");
        }

        if (isAttack == false && playerScript.GetIsSleep())
        {
            isAttack = true;
            StartCoroutine("Wave2Attack");
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        isAttack = true;
        playerScript = player.GetComponent<PlayerScript>();
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
        }
    }
}
