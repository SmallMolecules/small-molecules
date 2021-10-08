using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/** @brief A base class for dynamic field implementations

    This class specifies the requirements for an implemented instance of a dynamic field -
    a field that calculates forces between two particles. The force is added to both particles
    (due to conservation of forces).
    @author Isaac Bergl
    @date November 2021
    */
public class DynamicField
{ 
    /**
    Empty default constructor
    */
    public DynamicField(){
        
    }

    /**
    Virtual function that provides the dynamics of the field. Should be overwritten 
    by a custom function to provide the force on the particle.
    @returns zero vector (Vector3)
    */
    public virtual Vector3 fieldDynamics(Particle A, Particle B) {
        return new Vector3(0.0f, 0.0f, 0.0f);
    }

    /**
    Applies a force to the input particles. 
    */
    public void applyForce(Particle A, Particle B) {
        Vector3 F = fieldDynamics(A, B);

        A.addForce(F);
        B.addForce(-F);
    }

}
