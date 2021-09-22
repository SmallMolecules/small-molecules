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
    public virtual Vector3 force(Particle A, Particle B, Scales s) {
        return new Vector3(0.0f, 0.0f, 0.0f);
    }

    // PLEASE let me know if you think of a better way to wrap dynamic and static
    // fields into one general "field" superclass - without it being unintuitive
    // for a developer to implement in ParticleSystem.cs (Isaac)
}
