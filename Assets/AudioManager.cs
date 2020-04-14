using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    AudioSource _audioClip;

    Timer _gameTimer;

	// Use this for initialization
	void Start () {

        Broadcaste.startTestEvent += this.StartTest;
        Broadcaste.stopTestEvent += this.StopTest;
        _audioClip = this.GetComponent<AudioSource>();
        _audioClip.loop = true;
        
        _gameTimer = GameObject.FindGameObjectWithTag("TIMER").GetComponent<Timer>();
		
	}

    // Update is called once per frame
    void StartTest() {

        
        _audioClip.Play();



    }

    void StopTest() {

        _audioClip.Stop();

    }
}
