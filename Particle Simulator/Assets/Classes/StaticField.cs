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
    public virtual Vector3 force(Particle A) {
        return new Vector3(0.0f, 0.0f, 0.0f);
    }

    // PLEASE let me know if you think of a better way to wrap dynamic and static
    // fields into one general "field" superclass - without it being unintuitive
    // for a developer to implement in ParticleSystem.cs (Isaac)
    
}
