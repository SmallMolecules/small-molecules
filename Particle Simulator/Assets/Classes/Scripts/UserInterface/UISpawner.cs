using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;

// class for create particle UI
// This class is broken and needs to be fixed : (
// TODO - create new UISpawner for different systems
public class UISpawner : MonoBehaviour
{
    // input fields
    public InputField[] inputs;

    public Simulator simulator;

    public Slider slider;
    public Text sliderText;

    public Text scales;

    CultureInfo ci = new CultureInfo("en-us");//needed for string formatting for who knows why

    void Start()
    {
        simulator = GameObject.Find("System 1").GetComponent<Simulator>();//VERY TEMPORARY FIX
        slider.value = 0.0001f;
        updateScales(slider.value);
    }

    // Update is called once per frame
    void Update()
    {
        sliderText.text = slider.value.ToString("0.00000");
        
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
            simulator.AddNewParticle(pos);
        }
    }

    // creates randomly positioned paritcle
    public void RandomCreate() {
        simulator.AddNewParticleRandom();
    }

    //clears red error colour of fields
    public void ClearColour() {
        for (int i=0; i < 3; i++) {
            inputs[i].image.color = Color.white;
            
        }
    }

    public void updateScales(float t) {

        simulator.scales.setTime(t);

        scales.text = "System 1:\n";
        scales.text +=  String.Format("Time:\t{0} sec\n", 
                                simulator.scales.getTime().ToString("e02", ci));
        scales.text +=  String.Format("Length:\t{0} m",                        
                                simulator.scales.getLength().ToString("e02", ci));
        
        
    }

    //updates the global timescale as per the slider
    private void TimeScale(float dt) {
        simulator.scales.setTime(dt);
    }


}
