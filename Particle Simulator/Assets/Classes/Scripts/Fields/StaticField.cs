using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @brief A base class for static field implementations

    This class specifies the requirements for an implemented instance of a static field -
    a field that calculates a force on one particle.
    @author Isaac Bergl
    @author Dhruv Jobanputra
    @date November 2021
    \see DynamicField Coloumb StaticField
    */
public class StaticField
{
    /**Reference to the scales object of the parent simulator*/
    private Scales scales;

    /**
    The constructor method
    @param sim - the parent simulator (Simulator)
    */
    protected StaticField(Simulator sim)
    {
        scales = sim.scales;
    }

    /**
    Virtual function that provides the dynamics of the field. Should be overwritten 
    by a custom function to provide the force on A by B.
    @param Particle A (Particle)
    @returns zero vector (Vector3)
    */
    public virtual Vector3 FieldDynamics(Particle A)
    {
        return new Vector3(0.0f, 0.0f, 0.0f);
    }

    /**
    Applies a force to the input particles. 
    @param Particle A (Particle)
    */
    public void ApplyForce(Particle A)
    {
        Vector3 F = FieldDynamics(A);
        A.AddForce(F);
    }


}
