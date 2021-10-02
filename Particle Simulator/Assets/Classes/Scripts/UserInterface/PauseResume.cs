using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;

public class PauseResume : MonoBehaviour
{
    //components of the pause screen
    public GameObject PauseScreen;
    public GameObject PauseButton;
    public GameObject AddParticle;
    public GameObject System1;
    public GameObject simSettings;

    private SimulationManager manager;

    public Text text;

    public Text fps;
    public Text scales;
    CultureInfo ci = new CultureInfo("en-us");//needed for string formatting for who knows why

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
        System1.SetActive(false);
        // get system object
        // TODO - MAKE MORE GENERAL
        manager = GameObject.Find("Manager").GetComponent<SimulationManager>();
        
        // set value of slider to dt of system object
        
        slider.value = 0.0001f; //TEMPORARY
        // Debug.Log(slider.value);
    }
 
    // Update is called once per frame
    void Update()
    {
        _framesRendered++;

        text.text = slider.value.ToString("0.00000");

        updateScales();

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
 
    private void PauseGame()
    {
        manager.togglePause();
        PauseScreen.SetActive(true);
        PauseButton.SetActive(false);
        System1.SetActive(true);
    }
 
    private void ResumeGame()
    {
        manager.togglePause();
        PauseScreen.SetActive(false);
        PauseButton.SetActive(true);
        AddParticle.SetActive(false);
        System1.SetActive(false);
    }

    // called by the add particle button - shows new menu
    private void AddNewParticle() {
        if (AddParticle.activeSelf) {
            AddParticle.SetActive(false);
        }
        else {
            AddParticle.SetActive(true);
        }

    }

    public void ShowSimSettings() {
        simSettings.SetActive(!simSettings.activeInHierarchy);
    }

    private void updateScales() {
        scales.text = "System 1:\n";
        scales.text +=  String.Format("Time:\t{0} sec\n", 
                                manager.firstSystem().scales.getTime().ToString("e02", ci));
        scales.text +=  String.Format("Length:\t{0} m",                        
                                manager.firstSystem().scales.getLength().ToString("e02", ci));
        
    }

    //updates the global timescale as per the slider
    private void TimeScale(float dt) {
        manager.setTime(dt);
    }
}