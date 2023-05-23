using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particl00Emitter : MonoBehaviour
{
    public enum SPAWNMODE
    {
        Circle,
        Cube
    }

    public SPAWNMODE spawnMode = SPAWNMODE.Circle;


    public GameObject ParticlePrefab;

    public Vector2 generateCubeSize = Vector2.one;
    public float generateRadius = 5.0f;

    public GameObject parent;

    public float reGenerateTime = 0.3f;

    public float sizeMinusSpeed = 0.01f;
    public float alphaMinusSpeed = 0.01f;

    public float minSize = 0.0f;
    public float maxSize = 0.0f;

    public bool isRunning = false;
    private float generateTime = 0.0f;

    private Vector3 GenertePos;

    public Sprite image;

    // Start is called before the first frame update
    void Start()
    {
        generateTime = reGenerateTime;
        parent = GameObject.Find("Particles");
    }


        // Update is called once per frame
    void Update()
    {
        if (isRunning == true)
        {
            if (generateTime >= reGenerateTime)
            {
                GenertePos = this.transform.position;

                switch (spawnMode) 
                { 
                    case SPAWNMODE.Circle:
                        float degree = Random.Range(0.0f, 360.0f);
                        float radius = Random.Range(0.0f, generateRadius);

                        GenertePos.x = Mathf.Cos(degree) * radius;
                        GenertePos.y = Mathf.Sin(degree) * radius;
                     break;
                     case SPAWNMODE.Cube:

                        Vector2 generateCubeSizeHalf = generateCubeSize / 2;

                        GenertePos.x = Random.Range(-generateCubeSizeHalf.x, generateCubeSizeHalf.x);
                        GenertePos.y = Random.Range(-generateCubeSizeHalf.y, generateCubeSizeHalf.y);
                        break;
                }

                GenertePos.x += this.transform.position.x;
                GenertePos.y += this.transform.position.y;

                float setSize = Random.Range(minSize,maxSize);

                GameObject particle = Instantiate(ParticlePrefab, GenertePos, Quaternion.identity);
                particle.transform.parent = parent.transform;
                particle.GetComponent<Particle00>().Init(sizeMinusSpeed, alphaMinusSpeed, setSize);
                particle.GetComponent<SpriteRenderer>().sprite = image;

                generateTime -= reGenerateTime;
            }
            else{

                generateTime += Time.deltaTime;
            }
        }
    }
}
