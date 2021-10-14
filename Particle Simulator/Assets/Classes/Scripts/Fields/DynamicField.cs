using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/** @brief A base class for dynamic field implementations

    This class specifies the requirements for an implemented instance of a dynamic field -
    a field that calculates forces between two particles. The force is added to both particles
    (due to conservation of forces).
    @author Isaac Bergl
    @date November 2021
    \see StaticField Lennard-Jones Coloumb
    */
public class DynamicField
{
    /**
    Reference to parent simulator's scales
    */
    private Scales scales;

    protected Dictionary<string, float> constants;

    private Dictionary<string, int[]> units;

    /**
    Empty default constructor
    */
    protected DynamicField(Simulator sim)
    {
        scales = sim.scales;
        constants = new Dictionary<string, float>();
        units = new Dictionary<string, int[]>();
    }

    protected void registerConstant(string name, float val, int[] unit)
    {
        constants.Add(name, val);
        units.Add(name, unit);
        updateConstants();
    }

    public void updateConstants()
    {
        foreach (string con in units.Keys)
        {
            int[] unit = units[con];
            float val = constants[con];
            constants[con] = scales.scaleFactor(val, unit[0], unit[1], unit[2], unit[3]);
            Debug.Log(constants["con"]);
        }

    }

    /**
    Virtual function that provides the dynamics of the field. Should be overwritten 
    by a custom function to provide the force on the particle.
    @param particle A (Particle)
    @param particle B (Particle)
    @returns zero vector (Vector3)
    */
    public virtual Vector3 fieldDynamics(Particle A, Particle B)
    {
        return new Vector3(0.0f, 0.0f, 0.0f);
    }

    /**
    Applies a force to the input particles. 
    @param particle A (Particle)
    @param particle B (Particle)
    */
    public void applyForce(Particle A, Particle B)
    {
        Vector3 F = fieldDynamics(A, B);

        A.addForce(F);
        B.addForce(-F);
    }

}
