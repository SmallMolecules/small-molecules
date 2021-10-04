using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;

// class for create simulator UI
// NOTE - debating the necessity of this class being MonoBehaviour
public class UISpawner : MonoBehaviour
{
    // UI features
    public InputField[] inputs;
    public Slider slider;
    public Text scales;
    public Text coefficient;
    public InputField exponent;

    // reference to simulator script
    public Simulator simulator;

    //needed for string formatting for who knows why
    CultureInfo ci = new CultureInfo("en-us");

    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
          
    }

    // attempt to parse the input fields
    // [callback function]
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
            simulator.AddNewParticle(pos);
        }
    }

    // creates randomly positioned paritcle
    // [callback function]
    public void RandomCreate() {
        simulator.AddNewParticleRandom();
    }

    // accepts simulator and adds to data-structure
    public void attachSimulator(GameObject sim) {
        simulator = sim.GetComponent<Simulator>();
        exponent.text = simulator.scales.time.EXP.ToString();
        updateScales();
    }

    //clears red error colour of fields
    public void ClearColour() {
        for (int i=0; i < 3; i++) {
            inputs[i].image.color = Color.white;         
        }
    }

    // called to set scale class to value of slider
    // and update the display
    public void updateScales() {
        // Debug.Log(simulator);

        float coeff = (float) Convert.ToDouble(slider.value);
        int exp;
        Int32.TryParse(exponent.text, out exp);
        simulator.scales.setTime(coeff, exp);

        coefficient.text = slider.value.ToString("0.00")+ " x 10^";;
        exponent.text = simulator.scales.time.EXP.ToString();
        
        scales.text = "System 1:\n";
        scales.text +=  String.Format("Time:\t{0} sec\n", 
                                simulator.scales.time.VAL.ToString("e02", ci));
        scales.text +=  String.Format("Length:\t{0} m",                        
                                simulator.scales.length.VAL.ToString("e02", ci));   
    }

    //updates the global timescale as per the slider
    private void TimeScale(float dt) {
        simulator.scales.setTime(dt, -9);
    }

    // hides the UI component
    // [callback function]
    public void show() {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }

}
