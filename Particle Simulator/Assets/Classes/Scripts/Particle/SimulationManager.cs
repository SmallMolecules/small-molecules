using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SimulationManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject particle;

    public GameObject simulator;

    public bool paused;

    List<GameObject> simulations = new List<GameObject>();


    void Start()
    {
        // TODO - give position
        GameObject sim = Instantiate(simulator);
        sim.transform.parent = this.transform;
        simulations.Add(sim);

        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        
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
