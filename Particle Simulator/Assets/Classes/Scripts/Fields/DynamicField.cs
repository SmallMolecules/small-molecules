using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Superclass for dynamic fields (particle-particle interactions)
public class DynamicField
{ 
    // constructor
    public DynamicField(){
        
    }

    // returns force contribution from B on A:
    // THIS IS WHERE THE LOGIC GOES FOR NEW FIELDS
    public virtual Vector3 fieldDynamics(Particle A, Particle B) {
        return new Vector3(0.0f, 0.0f, 0.0f);
    }

    // wrapper function for applying a force
    public Vector3 applyForce(Particle A, Particle B, Scales s) {
        return s.getLength()*fieldDynamics(A, B);
    }

}
