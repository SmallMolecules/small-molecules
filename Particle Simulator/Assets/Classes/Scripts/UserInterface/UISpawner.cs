using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;

/** @brief Class for managing UI interactions with simulator sub-menu

    This class extends Unity's MonoBehaviour class. It is attached to
    a canvas object and is responsible for managinging the interactions 
    between the simulator UI sub-menu and its corresponding simulator object.
    @author Isaac Bergl
    @author Dhruv Jobanputra
    @date November 2021
    \see UISpawner Simulator
*/
public class UISpawner : MonoBehaviour
{
    /**Reference to the (x,y,z) spawn location InputFeilds*/
    public InputField[] inputs;
    /**Reference to the timescale slider*/
    public Slider timescale;
    /**Reference to the text representation of the unit scales*/
    public Text scales;
    /**Reference to the text representation of the coefficient of the time scale
    (eg for a timescale of 1.2e-9 the coefficient would be 1.2)*/
    public Text coefficient;
    /**Reference to the text representation of the exponent of the time scale
    (eg for a timescale of 1.2e-9 the exponent would be -9)*/
    public InputField exponent;

    /**Reference to the corresponding simulator object*/
    public Simulator simulator;

    //needed for string formatting for who knows why
    CultureInfo ci = new CultureInfo("en-us");

    /**
    \see @link https://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
    */
    void Start()
    {
        gameObject.SetActive(false);
    }

    /**
    \see @link https://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
    */
    void Update()
    {

    }

    /**
    Callback function used to create (or fail to create) a particle object according
    to the input fields
    */
    public void AttemptCreateParticle()
    {
        bool abort = false;
        float[] val = new float[3];
        for (int i = 0; i < 3; i++)
        {

            if (!float.TryParse(inputs[i].text, out val[i]))
            {
                inputs[i].image.color = Color.red;
                abort = true;
            }
        }
        // if no errors - create new particle
        if (!abort)
        {
            Vector3 pos = new Vector3(val[0], val[1], val[2]);
            simulator.AddNewParticle(pos);
        }
    }

    /**
    Callback function to spawn a particle at random location
    */
    public void RandomCreate()
    {
        simulator.AddNewParticleRandom();
    }

    /**
    Binds the "simulator" property of this object to 
    the input GameObject
    @param Simulator to attach (GameObject)
    */
    public void AttachSimulator(GameObject sim)
    {
        simulator = sim.GetComponent<Simulator>();
        exponent.text = simulator.scales.time.EXP.ToString();
        UpdateScales();
    }

    /**
    Callback function used to clear the background colours of the input fields
    */
    public void ClearColour()
    {
        for (int i = 0; i < 3; i++)
        {
            inputs[i].image.color = Color.white;
        }
    }

    /**
    Updates the simulator's scale properites and the on-screen text
    representation of the scales according to the values of the 
    input fields.
    */
    public void UpdateScales()
    {
        float coeff = (float)Convert.ToDouble(timescale.value);
        int exp;
        Int32.TryParse(exponent.text, out exp);
        simulator.UpdateTime(coeff, exp);

        coefficient.text = timescale.value.ToString("0.00") + " x 10^"; ;
        exponent.text = simulator.scales.time.EXP.ToString();

        scales.text = "System 1:\n";
        scales.text += String.Format("Time:\t{0} sec\n",
                                simulator.scales.time.VAL.ToString("e02", ci));
        scales.text += String.Format("Length:\t{0} m",
                                simulator.scales.length.VAL.ToString("e02", ci));
    }

    /**
    Callback function used to hide and unhide the simulator UI submenu 
    */
    public void Show()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }

    /**
    Callback function used to toggle the destroy mode
    */
    public void ToggleDestroyT()
    {
        simulator.ToggleDestroy(true);
    }

    /**
    Callback function used to toggle the destroy mode
    */
    public void ToggleDestroyF()
    {
        simulator.ToggleDestroy(false);
    }

}
