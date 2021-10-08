using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @brief A base class for static field implementations

    This class specifies the requirements for an implemented instance of a static field -
    a field that calculates a force on one particle.
    @author Isaac Bergl
    @date November 2021
    */
public class StaticField
{ 
    /**
    Empty default constructor
    */
    public StaticField(){

    }

    /**
    Virtual function that provides the dynamics of the field. Should be overwritten 
    by a custom function to provide the force on A by B.
    @returns zero vector (Vector3)
    */
    public virtual Vector3 fieldDynamics(Particle A) {
        return new Vector3(0.0f, 0.0f, 0.0f);
    }

    // wrapper function for applying a force
    public void applyForce(Particle A, Scales s) {
        Vector3 F = fieldDynamics(A);
        A.addForce(F);
    }

    
}
