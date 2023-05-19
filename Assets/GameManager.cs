using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject chokePrefab;

    private void MakeChokePrefab()
    {
        GameObject choke = Instantiate(chokePrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("MakeChokePrefab", 0.0f, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
