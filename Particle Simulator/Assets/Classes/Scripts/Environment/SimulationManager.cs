using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

/** @brief Class for managing the properites of all simulator instances

    This class manages all simulations in the environment. It specifies values such as the 
    pause state of all the simulations, the max number of particles.
    @author Isaac Bergl
    @date October 2021
    \see Simulator Scales
    */
public class SimulationManager : MonoBehaviour
{   
    /**The maximum number of particles allowed for each simulator*/
    [SerializeField][Range(0,20)]
    public int NUM_PARTICLES = 10;

    /**Reference to the GameObject this class is attached to*/
    public GameObject simulatorSpawner;

    /**Reference to the UI pause screen that interacts with the simulatorManager*/
    public GameObject UI;

    /**List of all simulation GameObjects*/
    List<GameObject> simulations = new List<GameObject>();

    /**Pause-state of all simulators. Dicates the pause-state of all simulators - if
    True then all simulators can be unpaused, if False then all simulators are paused*/
    public bool paused;

    /**Integer that specifies how many Simulators currently exist for naming purposes*/
    private int newestSim;


    /**
    \see @link https://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
    */
    void Start()
    {
        newestSim = 1;
        // TODO - give position

        CreateSimulator();
        paused = false;
    }

    /**
    \see @link https://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
    */
    void Update()
    {
        
    }

    /**Makes a new simulator and adds it to the list of simulators. Also names the system as
    "System X".*/
    private void CreateSimulator() {
        GameObject sim = Instantiate(simulatorSpawner);
        sim.name = String.Format("System {0}", newestSim);

        newestSim++;

        // set parent
        sim.transform.parent = this.transform;
        simulations.Add(sim);
        
        // add new simulator to UI menu
        UI.GetComponent<PauseResume>().NewSimulator(sim);
    }


    // TODO - implement this function
    /**Resets a simulator*/
    public void ResetSystems() {
        List<GameObject> newSimulations = new List<GameObject>();    
        foreach (GameObject S in simulations) {
            Destroy(S);
            GameObject sim = Instantiate(simulatorSpawner);
            sim.transform.parent = this.transform;
            newSimulations.Add(sim);
        }
        simulations = newSimulations;
    
    }
    
    /**Toggles the pause state of the simulation*/
    public void TogglePause() {
        paused = !paused;
        foreach (GameObject S in simulations) {
            S.GetComponent<Simulator>().paused = paused;
        }
    }

}
