using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LennardJones: DynamicField
{
    // Constants
    [SerializeField]
    private float e = 1f;
    private float sig = 1f;


    // field dynamics
    public override Vector3 force(Particle A, Particle B, Scales s) {

        float r = Vector3.Distance(A.getPos(), B.getPos());

        if (r < 2f) {
            r = 1f;
        }

        float con = 4*e*(Mathf.Pow(sig/r, 12)- Mathf.Pow(sig/r, 6));

        return con*Vector3.Normalize(A.getPos() - B.getPos());


    }
 
    
}
