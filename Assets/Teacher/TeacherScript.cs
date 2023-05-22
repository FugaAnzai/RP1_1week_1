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

    private IEnumerator Wave1Attack()
    {
        while (true)
        {

            Vector3 shotPos = this.transform.position;
            shotPos.y = -3.0f;
            GameObject choke = Instantiate(chokePrefab, shotPos, Quaternion.identity);
            yield return new WaitForSeconds(2.0f);

        }
    }

    private void SetWave1()
    {
        StartCoroutine("Wave1Attack");
    }
    private void Wave1()
    {
        if (Input.GetKey(KeyCode.N) || playerScript.GetIsSleep() == false)
        {
            isAttack = false;
            initCount = 0;
            StopCoroutine("Wave1Attack");
        }

        if(isAttack == false && playerScript.GetIsSleep())
        {
            isAttack = true;
            StartCoroutine("Wave1Attack");
        }

    }

    private IEnumerator Wave2Attack()
    {

        while (true)
        {

            initCount++;

            if (initCount > kWave2ChokeMax - 1)
            {
                initCount = 0;
                yield return new WaitForSeconds(2.0f);
            }

            Vector3 shotPos = this.transform.position;
            shotPos.y = initPosY[initCount];
            GameObject choke = Instantiate(chokePrefab, shotPos, Quaternion.identity);
            yield return new WaitForSeconds(0.3f);

        }
    }

    private void SetWave2()
    {
        initPosY = new float[kWave2ChokeMax];
        initPosY[0] = -3.0f;
        initPosY[1] = -2.0f;
        initCount = 0;

        StopCoroutine("Wave1Attack");
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
