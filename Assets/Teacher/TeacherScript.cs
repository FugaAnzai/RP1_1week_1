using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherScript : MonoBehaviour
{
    public GameObject chokePrefab;
    public GameObject player;
    
    private PlayerScript playerScript;

    private bool isAttack;

    private void MakeChokePrefab()
    {
        //チョーク発射位置
        Vector3 shotPos = this.transform.position;
        shotPos.y = player.transform.position.y;
        GameObject choke = Instantiate(chokePrefab, shotPos, Quaternion.identity);
    }

    private void StraightChoke()
    {
        Vector3 shotPos = this.transform.position;
        shotPos.y = -2.5f;
        GameObject choke = Instantiate(chokePrefab, shotPos, Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
        isAttack = true;
        InvokeRepeating("StraightChoke", 0.0f, 2.0f);
        playerScript = player.GetComponent<PlayerScript>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //N押すと発射が止みます
        if (Input.GetKey(KeyCode.N) || playerScript.GetIsSleep() == false)
        {
            isAttack = false;
            CancelInvoke("StraightChoke");
        }

        if(isAttack == false && playerScript.GetIsSleep())
        {
            isAttack = true;
            InvokeRepeating("StraightChoke", 0.0f, 2.0f);
        }
    }
}
