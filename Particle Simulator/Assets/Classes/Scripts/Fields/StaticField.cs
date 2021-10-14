using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @brief A base class for static field implementations

    This class specifies the requirements for an implemented instance of a static field -
    a field that calculates a force on one particle.
    @author Isaac Bergl
    @date November 2021
    \see DynamicField Coloumb StaticField
    */
public class StaticField
{
    /**Reference to the scales object of the parent simulator*/
    private Scales scales;

    /**A dictionary of constants. The key value is the string name
    and the value is the float value of the constant*/
    protected Dictionary<string, float> constants;

    /**A Dictionary of the units of the constants for internal use. The key
    value is the string name and the value is an int array of the unit exponents
    in the form {kg, m, s, C}.*/
    private Dictionary<string, int[]> units;

    /**
    The constructor method
    @param sim - the parent simulator (Simulator)
    */
    protected StaticField(Simulator sim)
    {
        scales = sim.scales;
        constants = new Dictionary<string, float>();
        units = new Dictionary<string, int[]>();
    }

    /**Called by an extension class to regester an SI constant to be
    updated with the scales of the simulator.
    @param name - name of the constant (string)
    @param val - float value of the constant in SI units (float)
    @param unit - int array of the multiplicities of the SI unit (int[4])
    */
    protected void registerConstant(string name, float val, int[] unit)
    {
        constants.Add(name, val);
        units.Add(name, unit);
        updateConstants();
    }
    /**
    Called by the simulator to update each unit when a Scale update event occurs
    */
    public void updateConstants()
    {
        foreach (string con in units.Keys)
        {
            int[] unit = units[con];
            float val = constants[con];
            constants[con] = scales.scaleFactor(val, unit[0], unit[1], unit[2], unit[3]);
        }

    }

    /**
    Virtual function that provides the dynamics of the field. Should be overwritten 
    by a custom function to provide the force on A by B.
    @param Particle A (Particle)
    @returns zero vector (Vector3)
    */
    public virtual Vector3 fieldDynamics(Particle A)
    {
        return new Vector3(0.0f, 0.0f, 0.0f);
    }

    /**
    Applies a force to the input particles. 
    @param Particle A (Particle)
    */
    public void applyForce(Particle A, Scales s)
    {
        Vector3 F = fieldDynamics(A);
        A.addForce(F);
    }


}
