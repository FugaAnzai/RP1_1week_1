using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public float timer = 0.0f;
    public float timeLimit = 60.0f;

    public GameObject timerObject;

    private Image timerImage;

    // Start is called before the first frame update
    void Start()
    {
        timer = timeLimit;
        timerImage = timerObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

        

        if(timer <= 0.0f)
        {
            timer = 0.0f;
        }

        timerImage.fillAmount = timer / timeLimit;

        if (timer > 0.0f)
        {
            timer -= Time.deltaTime;
        }
    }
}
