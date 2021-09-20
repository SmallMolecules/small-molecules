using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind: StaticField
{
    // Constants
    [SerializeField]
    private float k = 1f;

    // field dynamics
    public override Vector3 force(Particle A) {
        return k*(new Vector3(0.0f, 1.0f, 0.0f));
    }   

    
}
