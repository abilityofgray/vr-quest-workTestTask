using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour {

    public enum GAME_STATE {

        IDLE,
        START_EVENT,
        STOP_EVENT


    }
    
    public TextMeshProUGUI min;
    public TextMeshProUGUI sec;

    public GAME_STATE game_state;

    bool startTimer;


    float timer;
    float seconds;
	// Use this for initialization
	void Start () {

        game_state = GAME_STATE.IDLE;

        startTimer = false;
		
	}
	
	// Update is called once per frame
	void Update () {

        if (startTimer && seconds < 60) {

            timer += Time.deltaTime;
            seconds = Mathf.RoundToInt(timer % 60);
            min.text = seconds.ToString();
            //sec.text += Mathf.RoundToInt(Time.deltaTime % 60).ToString();

        }

        if (seconds >= 60) {


            startTimer = false;
            StopEvent();

        }


    }

    public void StartEvent() {

        timer = 0;
        seconds = 0;
        game_state = GAME_STATE.START_EVENT;
        startTimer = true;
        Broadcaste.StartEventTest();
        Debug.Log("StartEvent");

    }

    public void StopEvent() {

        startTimer = false;
        game_state = GAME_STATE.STOP_EVENT;
        Broadcaste.StopEventTest();
        Debug.Log("StopEvent");
            


    }
}
