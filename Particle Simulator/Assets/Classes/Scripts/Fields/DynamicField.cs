using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/** @brief A base class for dynamic field implementations

    This class specifies the requirements for an implemented instance of a dynamic field -
    a field that calculates forces between two particles. The force is added to both particles
    (due to conservation of forces).
    @author Isaac Bergl
    @author Dhruv Jobanputra
    @date November 2021
    \see StaticField Lennard-Jones Coloumb
    */
public class DynamicField
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
    protected DynamicField(Simulator sim)
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
    protected void RegisterConstant(string name, float val, int[] unit)
    {
        constants.Add(name, val);
        units.Add(name, unit);
        UpdateConstants();
    }
    /**
    Called by the simulator to update each unit when a Scale update event occurs
    */
    public void UpdateConstants()
    {
        foreach (string con in units.Keys)
        {
            int[] unit = units[con];
            float val = constants[con];
            constants[con] = scales.ScaleFactor(val, unit[0], unit[1], unit[2], unit[3]);
        }
    }

    /**
    Virtual function that provides the dynamics of the field. Should be overwritten 
    by a custom function to provide the force on the particle.
    @param particle A (Particle)
    @param particle B (Particle)
    @returns zero vector (Vector3)
    */
    public virtual Vector3 FieldDynamics(Particle A, Particle B)
    {
        return new Vector3(0.0f, 0.0f, 0.0f);
    }

    /**
    Applies a force to the input particles. 
    @param particle A (Particle)
    @param particle B (Particle)
    */
    public void ApplyForce(Particle A, Particle B)
    {
        Vector3 F = FieldDynamics(A, B);

        A.AddForce(F);
        B.AddForce(-F);
    }

}
