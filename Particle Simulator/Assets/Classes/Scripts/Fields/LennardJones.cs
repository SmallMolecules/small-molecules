using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Lennard Jones is Broken ATM sorry
public class LennardJones: DynamicField
{
    // Constants
    [SerializeField]
    private float sig = 1E-9f;
    private float e = 1E-9f;


    // field dynamics
    public override Vector3 fieldDynamics(Particle A, Particle B) {

        float r = Vector3.Distance(A.getPos(), B.getPos());

        float SIG = A.scales.scaleFactor(sig, 0, 1, 0, 0);
        float E = A.scales.scaleFactor(e, 1, 1, -2, 0);

        //TODO - autimatically calculate collision distance
        if (r < 1.7f) {
            r = 1.7f;
        }

        float con = 4*E*(Mathf.Pow(SIG/r, 12)- Mathf.Pow(SIG/r, 6));

        return con*Vector3.Normalize(A.getPos() - B.getPos());


    }
 
    
}
