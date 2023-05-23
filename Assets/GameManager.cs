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

    public GameObject damageTimerObject;
    public GameObject player;
    public GameObject teacher;
    public float teacherDamageLimit = 60.0f;

    private Image timerImage;
    private Slider damageSlider;

    private TeacherScript teacherScript;

    private float damageTimer = 0.0f;
    private const float kDamageTimerAddValue = 0.1f;

    private const float kWave1HealthMAX = 1.0f;
    private const float kWave2HealthMAX = 2.0f;
    private const float kWave3HealthMAX = 3.0f;
    private const float kWave4HealthMAX = 3.0f;
    private const float kWave5HealthMAX = 3.0f;
    private const float kWave6HealthMAX = 3.0f;
    private const float kWave7HealthMAX = 3.0f;
    private const float kWave8HealthMAX = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        timer = timeLimit;
        timerImage = timerObject.GetComponent<Image>();

        damageSlider = damageTimerObject.GetComponent<Slider>();
        teacherScript = teacher.GetComponent<TeacherScript>();
    }

    void ClearObject(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);

        foreach (GameObject obj in objects)
        {
            Destroy(obj);
        }

    }

    void ResetSleep()
    {
<<<<<<< HEAD
        sleepTimer = 0.0f;

        float stl = 0.0f; //クリア時間
=======
>>>>>>> 6b3c06d1ed4cab161bf9a62ed7a96bfd07127d8a

        switch (teacher.GetComponent<TeacherScript>().wave)
        {
            case TeacherScript.WAVE.SetWave1:
                damageTimer = 0.0f;
                teacherDamageLimit = kWave1HealthMAX;
                timer = timeLimit;
                timerImage = timerObject.GetComponent<Image>();
                break;
            case TeacherScript.WAVE.SetWave2:
                damageTimer = 0.0f;
                teacherDamageLimit = kWave2HealthMAX;
                timer = timeLimit;
                timerImage = timerObject.GetComponent<Image>();
                break;
            case TeacherScript.WAVE.SetWave3:
                damageTimer = 0.0f;
                teacherDamageLimit = kWave3HealthMAX;
                timer = timeLimit;
                timerImage = timerObject.GetComponent<Image>();
                break;
            case TeacherScript.WAVE.SetWave4:
                damageTimer = 0.0f;
                teacherDamageLimit = kWave4HealthMAX;
                timer = timeLimit;
                timerImage = timerObject.GetComponent<Image>();
                break;
            case TeacherScript.WAVE.SetWave5:
                damageTimer = 0.0f;
                teacherDamageLimit = kWave5HealthMAX;
                timer = timeLimit;
                timerImage = timerObject.GetComponent<Image>();
                break;
            case TeacherScript.WAVE.SetWave6:
                damageTimer = 0.0f;
                teacherDamageLimit = kWave6HealthMAX;
                timer = timeLimit;
                timerImage = timerObject.GetComponent<Image>();
                break;
            case TeacherScript.WAVE.SetWave7:
                damageTimer = 0.0f;
                teacherDamageLimit = kWave7HealthMAX;
                timer = timeLimit;
                timerImage = timerObject.GetComponent<Image>();
                break;
            case TeacherScript.WAVE.SetWave8:
                damageTimer = 0.0f;
                teacherDamageLimit = kWave8HealthMAX;
                timer = timeLimit;
                timerImage = timerObject.GetComponent<Image>();
                break;
        }

<<<<<<< HEAD

        sleepTimerLimit = stl;
=======
>>>>>>> 6b3c06d1ed4cab161bf9a62ed7a96bfd07127d8a
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
        
        if(damageTimer >= teacherDamageLimit)
        {
            damageTimer = teacherDamageLimit;
        }

        if (teacherScript.GetIsStartCalculate())
        {
            damageTimer += kDamageTimerAddValue * teacherScript.GetHitCount();
            teacherScript.SetHitCount(0);
            teacherScript.SetIsStartCalculate(false);
        }

        if(damageSlider.value >= 1.0f)
        {
            damageSlider.value = 0;
            ClearObject("Choke");
            ClearObject("Powder");
            teacherScript.wave++;

        }

        ResetSleep();

        damageSlider.value = damageTimer / teacherDamageLimit;

        

        
    }
}
