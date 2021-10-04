using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coloumb: DynamicField
{
    // Constants
    [SerializeField]
    private float k = 8.988E+9f;


    // field dynamics
    public override Vector3 fieldDynamics(Particle A, Particle B) {

        float q1 = B.charge;
        float q2 = A.charge;
        
        float r = Vector3.Distance(A.getPos(), B.getPos());

        if (r < 2f) {
            r = 0.5f;
        }

        float con = A.scales.scaleFactor(k, -1, -3, 2, 2)*q1*q2/r;
        // Debug.Log(A.scales.scaleFactor(k, -1, -3, 2, 2).ToString());
        return con*Vector3.Normalize(A.getPos() - B.getPos());
    } 
}
