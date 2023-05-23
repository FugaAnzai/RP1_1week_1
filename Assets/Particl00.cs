using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particl00 : MonoBehaviour
{

    public GameObject ParticlePrefab;
    public float generateRadius = 5.0f;
    public float reGenerateTime = 0.3f;

    public bool isRunning = false;
    private float generateTime = 0.0f;

    private Vector3 GenertePos;

    // Start is called before the first frame update
    void Start()
    {
        generateTime = reGenerateTime;
    }


        // Update is called once per frame
    void Update()
    {
        if (isRunning == true)
        {
            if (generateTime >= reGenerateTime)
            {
                GenertePos = this.transform.position;

                float degree = Random.Range(0.0f, 360.0f);
                float radius = Random.Range(0.0f, generateRadius);

                GenertePos.x = Mathf.Cos(degree) * radius;
                GenertePos.y = Mathf.Sin(degree) * radius;

                GameObject particle = Instantiate(ParticlePrefab, GenertePos, Quaternion.identity);


                generateTime -= reGenerateTime;
            }
            else{

                generateTime += Time.deltaTime;
            }
        }
    }
}
