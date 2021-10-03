using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

// class to manage instances of simulators
public class SimulationManager : MonoBehaviour
{   
    [SerializeField][Range(0,20)]
    public int NUM_PARTICLES = 10;

    // reference to object to spawn
    public GameObject simulator_spawner;

    // reference to UI pause screen
    public GameObject UI;

    // list of all simulations 
    List<GameObject> simulations = new List<GameObject>();

    // pauses all simulations
    public bool paused;

    // index of how many systems exist
    private int newestSim;

    void Start()
    {
        newestSim = 1;
        // TODO - give position

        createSimulator();
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // makes new random simulator
    private void createSimulator() {
        GameObject sim = Instantiate(simulator_spawner);
        sim.name = String.Format("System {0}", newestSim);

        newestSim++;

        // set parent
        sim.transform.parent = this.transform;
        simulations.Add(sim);
        
        // add new simulator to UI menu
        UI.GetComponent<PauseResume>().newSimulator(sim);
    }


    // TODO - implement this function
    // resets system to some system state
    public void resetSystems() {
        List<GameObject> newSimulations = new List<GameObject>();    
        foreach (GameObject S in simulations) {
            Destroy(S);
            GameObject sim = Instantiate(simulator_spawner);
            sim.transform.parent = this.transform;
            newSimulations.Add(sim);
        }
        simulations = newSimulations;
    
    }
    
    // pauses/unpauses
    public void togglePause() {
        paused = !paused;
        foreach (GameObject S in simulations) {
            S.GetComponent<Simulator>().paused =  paused;
        }
    }

}
