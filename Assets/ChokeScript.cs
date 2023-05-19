using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChokeScript : MonoBehaviour
{
    public float moveSpeed;

    private Vector2 velocity = new Vector2(-1,0);

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
        Debug.Log("delete");
    }

    // Start is called before the first frame update
    void Start()
    {

        velocity = velocity * moveSpeed;

    }

    // Update is called once per frame
    void Update()
    {

        this.transform.position += new Vector3(velocity.x,velocity.y,0) * Time.deltaTime;      
    }
}
