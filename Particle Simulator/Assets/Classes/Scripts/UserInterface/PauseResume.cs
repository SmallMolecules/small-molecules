using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;

// Class to handle UI Pause Screen
public class PauseResume : MonoBehaviour
{
    //components of the pause screen
    public GameObject PauseScreen;
    public GameObject PauseButton;
    public Button system_button;
    public Text fps;


    public GameObject simulatorUI_spawner;

    // reference to Simulation Manager Script
    private SimulationManager manager;


    // Stored Colors
    private Color onnColor = new Color(0.4f, 1f, 0.8f);
    private Color offColor = new Color(1f, 1f, 1f);


    System.DateTime _lastTime; // marks the beginning the measurement began
    int _framesRendered; // an increasing count
    int _fps; // the FPS calculated from the last measurement
 

    void Start()
    {
        // show/hide menu items
        PauseScreen.SetActive(false);
        PauseButton.SetActive(true);
        system_button.GetComponent<Image>().color = offColor;

        // find manager script
        manager = GameObject.Find("Manager").GetComponent<SimulationManager>();
        
    }
 
    // Update is called once per frame
    void Update()
    {
        // update FPS counter
        _framesRendered++;

        // calculate frames
        if ((System.DateTime.Now - _lastTime).TotalSeconds >= 1)
        {
            // one second has elapsed
            _fps = _framesRendered;                     
            _framesRendered = 0;            
            _lastTime = System.DateTime.Now;
        }
        // display FPS counter
        fps.text = String.Format("FPS:\t{0}", _fps);
        
    }
 
    private void PauseGame()
    {
        manager.togglePause();
        PauseScreen.SetActive(true);
        PauseButton.SetActive(false);
    }
 
    private void ResumeGame()
    {
        manager.togglePause();
        PauseScreen.SetActive(false);
        PauseButton.SetActive(true);
    }

    // called by Simulation Manager when a new simulation is added
    public void newSimulator(GameObject sim) {
        // create simulation UI object
        GameObject UIentry = Instantiate(simulatorUI_spawner);
        // set parent as pause screen
        UIentry.transform.SetParent(PauseScreen.transform, false); 
        // find script
        UISpawner script = UIentry.GetComponent<UISpawner>();
        // give functionality to "system n" button
        system_button.onClick.AddListener (script.show);
        // pass simulation gameobject into UI spawner
        script.attachSimulator(sim);
        
        
    }

    // swaps the color of the selected element
    public void toggleSelectedColour() {
        Color orig = system_button.GetComponent<Image>().color;
        if (orig.Equals(offColor)) {
            system_button.GetComponent<Image>().color = onnColor;
        }
        else {
           system_button.GetComponent<Image>().color = offColor;
        }
        
    }


}