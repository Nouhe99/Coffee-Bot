using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    public float ClientTime;
    float maxtimer = 1;
    public float timerValue=1;
    public Color color1;
    public Color color2;

    Image radialClock;
    [SerializeField]
    int id;

    public CoffeeManager coffeScript;

    // Start is called before the first frame update
    void Start()
    {
        radialClock = gameObject.GetComponent<Image>();
        coffeScript = FindObjectOfType<CoffeeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        timerValue -= Time.deltaTime/ ClientTime;
        radialClock.fillAmount = timerValue/ maxtimer;
        if(timerValue <= maxtimer / 4)
        {
            gameObject.GetComponent<Image>().color = color2;
        }
        else
        {
            gameObject.GetComponent<Image>().color = color1;

        }

        if (timerValue <= 0)
        {
            coffeScript.SugarError(id);
            timerValue = 1;

        }
    }


}
