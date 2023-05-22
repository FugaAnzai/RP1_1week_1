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

    public GameObject sleepTimerObject;
    public GameObject player;
    public float sleepTimerLimit = 60.0f;

    private Image timerImage;
    private Slider sleepSlider;

    private float sleepTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        timer = timeLimit;
        timerImage = timerObject.GetComponent<Image>();

        sleepSlider = sleepTimerObject.GetComponent<Slider>();
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

        if(player.GetComponent<PlayerScript>().GetIsSleep()){
            sleepTimer += Time.deltaTime;
        }

        if(sleepTimer >= sleepTimerLimit)
        {
            sleepTimer = sleepTimerLimit;
        }

        sleepSlider.value = sleepTimer / sleepTimerLimit;
    }
}
