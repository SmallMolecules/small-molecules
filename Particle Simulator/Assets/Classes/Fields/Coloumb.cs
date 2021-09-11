using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coloumb: DynamicField
{
    // Constants
    [SerializeField]
    private float k = 5f;


    // field dynamics
    public override Vector3 force(Particle A, Particle B) {

        float q1 = B.charge;
        float q2 = A.charge;

        float r = Vector3.Distance(A.getPos(), B.getPos());

        if (r < 1) {
            r = 0.5f;
        }

        float con = k*q1*q2/r;

        return con*Vector3.Normalize(A.getPos() - B.getPos());


    }
 
    
}
