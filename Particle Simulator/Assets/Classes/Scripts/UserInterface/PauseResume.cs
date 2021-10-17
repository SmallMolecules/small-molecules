using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;

/** @brief Class for mananging UI interactions

    This class extends Unity's MonoBehaviour class. It is attached to
    a canvas object and is responsible for managinging the interactions 
    between the UI screen and the system.
    @author Isaac Bergl
    @author Dhruv Jobanputra
    @date November 2021
    \see UISpawner SimulationManager
    */
public class PauseResume : MonoBehaviour
{
    /**Reference to the pause-screen elemenet*/
    public GameObject pauseScreen;
    /**Reference to the pause button*/
    public GameObject pauseButton;
    /**Reference to the system button*/
    public Button systemButton;
    /**Reference to the FPS display text*/
    public Text fps;
    
    /**GameObject to spawn for the simulator sub-menu*/
    public GameObject simulatorUISpawner;

    /**Reference to the simulation manager object*/
    private SimulationManager manager;

    // stored colours
    private Color onnColor = new Color(0.4f, 1f, 0.8f);
    private Color offColor = new Color(1f, 1f, 1f);

    System.DateTime _lastTime; // marks the beginning the measurement began
    int _framesRendered; // an increasing count
    int _fps; // the FPS calculated from the last measurement
 
    /**
    \see @link https://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
    */
    void Start()
    {
        pauseScreen.SetActive(false);
        pauseButton.SetActive(true);
        systemButton.GetComponent<Image>().color = offColor;

        manager = GameObject.Find("Manager").GetComponent<SimulationManager>();
        
    }
 
    /**
    \see @link https://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
    */
    void Update()
    {
        _framesRendered++;

        if ((System.DateTime.Now - _lastTime).TotalSeconds >= 1)
        {
            _fps = _framesRendered;                     
            _framesRendered = 0;            
            _lastTime = System.DateTime.Now;
        }

        fps.text = String.Format("FPS:\t{0}", _fps);
        
    }

    /**
    Callback function used to pause all simulations
    */
    private void PauseGame()
    {
        manager.TogglePause();
        pauseScreen.SetActive(true);
        pauseButton.SetActive(false);
    }
    /**
    Callback function used to resume all simulations
    */
    private void ResumeGame()
    {
        manager.TogglePause();
        pauseScreen.SetActive(false);
        pauseButton.SetActive(true);
    }

    /**
    Callback function used to add a new simulator to the list of 
    simulations managed by the simulation manager object
    @param GameObject sim
    */
    public void NewSimulator(GameObject sim) 
    {
        GameObject UIentry = Instantiate(simulatorUISpawner);
        UIentry.transform.SetParent(pauseScreen.transform, false); 

        UISpawner script = UIentry.GetComponent<UISpawner>();

        systemButton.onClick.AddListener (script.Show);

        script.AttachSimulator(sim);
    }

    /**
    Callback function used to toggle a button between the "selected" colour
    and the "unselected" colour.
    */
    public void ToggleSelectedColour() 
    {
        Color orig = systemButton.GetComponent<Image>().color;
        if (orig.Equals(offColor)) {
            systemButton.GetComponent<Image>().color = onnColor;
        }
        else {
           systemButton.GetComponent<Image>().color = offColor;
        }    
    }

}