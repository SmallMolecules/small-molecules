using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;

// TODO - migrate objects to UISpawner
public class PauseResume : MonoBehaviour
{
    //components of the pause screen
    public GameObject PauseScreen;
    public GameObject PauseButton;

    public GameObject simulator_UI;

    public Button system_button;

    private SimulationManager manager;

    public Text fps;


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
        // TODO - MAKE MORE GENERAL
        manager = GameObject.Find("Manager").GetComponent<SimulationManager>();
        
    }
 
    // Update is called once per frame
    void Update()
    {
        _framesRendered++;


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
    }
 
    private void ResumeGame()
    {
        manager.togglePause();
        PauseScreen.SetActive(false);
        PauseButton.SetActive(true);
    }

    public void newSimulator(GameObject sim) {
        GameObject UIentry = Instantiate(simulator_UI);
        UIentry.transform.SetParent(PauseScreen.transform, false); 
        UISpawner script = UIentry.GetComponent<UISpawner>();
        system_button.onClick.AddListener (script.show);
        script.attachSimulator(sim);
        
        
    }


}