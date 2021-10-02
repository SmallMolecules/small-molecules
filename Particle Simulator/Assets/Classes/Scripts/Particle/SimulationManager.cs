using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SimulationManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField][Range(0,20)]
    public int NUM_PARTICLES = 10;

    public GameObject particle;

    public GameObject simulator;

    public bool paused;

    private int newestSim;

    List<GameObject> simulations = new List<GameObject>();


    void Start()
    {
        newestSim = 1;
        // TODO - give position

        GameObject sim = Instantiate(simulator);
        sim.name = String.Format("System {0}", newestSim);
        newestSim++;
        sim.transform.parent = this.transform;
        simulations.Add(sim);

        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetSystems() {
        List<GameObject> newSimulations = new List<GameObject>();    
        foreach (GameObject S in simulations) {
            Destroy(S);
            GameObject sim = Instantiate(simulator);
            sim.transform.parent = this.transform;
            newSimulations.Add(sim);
        }
        simulations = newSimulations;
    
    }
    

    public Simulator firstSystem() {
        return simulations[0].GetComponent<Simulator>();
    }

    public void togglePause() {
        paused = !paused;
        foreach (GameObject S in simulations) {
            S.GetComponent<Simulator>().paused =  paused;
        }
    }

    public void setTime(float t) {
        foreach (GameObject S in simulations) {
            // TODO - figure out why tf this is throwing an error
            try {
            S.GetComponent<Simulator>().scales.setTime(t);
            }
            catch (Exception e)
        {
            
        }
        }
    }
}
