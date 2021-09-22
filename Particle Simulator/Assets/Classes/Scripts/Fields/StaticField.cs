using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Superclass for staticfields (particle-environment interactions)
public class StaticField
{ 
     
    public StaticField(){

    }

    // returns force contribution from ENVIRONMENT on A:
    // THIS IS WHERE THE LOGIC GOES FOR NEW FIELDS
    public virtual Vector3 force(Particle A, Scales s) {
        return new Vector3(0.0f, 0.0f, 0.0f);
    }

    
}
