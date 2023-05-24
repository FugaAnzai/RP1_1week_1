using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Particle00 : MonoBehaviour
{

    private Vector3 size = new Vector3 (1.0f, 1.0f, 1.0f);
    public Vector3 startSize = Vector3.zero;
    private float sizeMinusSpeed = 0.0f;

    private float alpha = 1.0f;
    private float alphaMinusSpeed = 0.0f;

    // Start is called before the first frame update
    void Start()
    {

        this.transform.localScale = Vector3.zero;
    }

    public void Init(float setSizeMinusSpeed, float setAlphaMinusSpeed,float setSize)
    {
        size = new Vector3(setSize, setSize, 1.0f);
        sizeMinusSpeed  = setSizeMinusSpeed * 0.01f;
        alphaMinusSpeed = setAlphaMinusSpeed * 0.01f;

        this.transform.localScale = size;

        startSize = this.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        alpha -= alphaMinusSpeed;
        size.x -= sizeMinusSpeed;
        size.y -= sizeMinusSpeed;

        if(alpha <= 0.0f || size.x <= 0.0f || size.y <= 0.0f)
        {
            Destroy(this.gameObject);
        }

        Vector3 sizeA;
        sizeA.x = startSize.x * size.x;
        sizeA.y = startSize.y * size.y;
        sizeA.z = startSize.z * size.z;

        this.transform.localScale = sizeA;
        this.GetComponent<SpriteRenderer>().color = new UnityEngine.Color(1.0f, 1.0f, 1.0f, alpha);
    }
}
