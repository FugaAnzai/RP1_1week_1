using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherScript : MonoBehaviour
{
    public GameObject chokePrefab;
    public GameObject player;
    private void MakeChokePrefab()
    {
        //�`���[�N���ˈʒu
        Vector3 shotPos = this.transform.position;
        shotPos.y = player.transform.position.y;
        GameObject choke = Instantiate(chokePrefab, shotPos, Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("MakeChokePrefab", 0.0f, 2.0f);
        
    }

    // Update is called once per frame
    void Update()
    {
        //N�����Ɣ��˂��~�݂܂�
        if (Input.GetKey(KeyCode.N))
        {
            CancelInvoke("MakeChokePrefab");
        }
    }
}
