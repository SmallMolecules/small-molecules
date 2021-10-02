using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// class for create particle UI
// This class is broken and needs to be fixed : (
public class UISpawner : MonoBehaviour
{
    // input fields
    public InputField[] inputs;

    public SimulationManager system;


    void Start()
    {
        system = GameObject.Find("Manager").GetComponent<SimulationManager>();//VERY TEMPORARY FIX
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    // attempt to parse the input fields
    public void AttemptCreate() {
        bool abort = false;
        float[] val = new float[3];
        for (int i=0; i < 3; i++) {
            
            if(!float.TryParse(inputs[i].text, out val[i])) {
                inputs[i].image.color = Color.red;
                abort = true;
            }
        }
        // if no errors - create new particle
        if (!abort) {
            Vector3 pos = new Vector3(val[0], val[1], val[2]);
            system.firstSystem().AddNewParticle(pos);
        }
    }

    // creates randomly positioned paritcle
    public void RandomCreate() {
        system.firstSystem().AddNewParticleRandom();
    }

    //clears red error colour of fields
    public void ClearColour() {
        for (int i=0; i < 3; i++) {
            inputs[i].image.color = Color.white;
            
        }
    }


}
