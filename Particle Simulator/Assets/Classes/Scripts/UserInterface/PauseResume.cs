using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PauseResume : MonoBehaviour
{
    //components of the pause screen
    public GameObject PauseScreen;
    public GameObject PauseButton;
    public GameObject AddParticle;

    private ParticleSystem system;

    public Text text;

    public Text fps;

    public Slider slider;

    // may need to include these in a separate class TODO
    System.DateTime _lastTime; // marks the beginning the measurement began
    int _framesRendered; // an increasing count
    int _fps; // the FPS calculated from the last measurement
 
    // Start is called before the first frame update
    void Start()
    {
        // show/hide menu items
        PauseScreen.SetActive(false);
        PauseButton.SetActive(true);
        AddParticle.SetActive(false);
        // get system object
        system = GameObject.Find("System").GetComponent<ParticleSystem>();
        
        // set value of slider to dt of system object
        slider.value = system.scales.getTime();
        // Debug.Log(slider.value);
    }
 
    // Update is called once per frame
    void Update()
    {
        _framesRendered++;

        text.text = slider.value.ToString("0.000");

        // calculate frames
        if ((System.DateTime.Now - _lastTime).TotalSeconds >= 1)
        {
            // one second has elapsed
            _fps = _framesRendered;                     
            _framesRendered = 0;            
            _lastTime = System.DateTime.Now;
        }
        fps.text = String.Format("FPS:\t{0}", _fps);
    }
 
    public void PauseGame()
    {
        system.togglePause();
        PauseScreen.SetActive(true);
        PauseButton.SetActive(false);
        
    }
 
    public void ResumeGame()
    {
        system.togglePause();
        PauseScreen.SetActive(false);
        PauseButton.SetActive(true);
        AddParticle.SetActive(false);
    }

    // called by the add particle button - shows new menu
    public void AddNewParticle() {
        if (AddParticle.activeSelf) {
            AddParticle.SetActive(false);
        }
        else {
            AddParticle.SetActive(true);
        }

    }

    //updates the timescale as per the slider
    public void TimeScale(float dt) {
        system.scales.setTime(dt);
    }
}