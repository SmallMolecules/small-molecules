using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @brief A DynamicField implementation of the Couloumb potential

    This class extends the DynamicField class and overrides the fieldDynamics 
    method to give a realistic implementation of the coloumb potential.
    @author Isaac Bergl
    @date November 2021
    \see Lennard-Jones DynamicField StaticField
    */
public class Coloumb: DynamicField
{
    /**the coloumb constant in SI units*/
    private float k = 8.988E+9f;

    /**
    Overriding function. Provides the dynamics of the coloumb potential.
    @param Particle A (Particle)
    @param Particle B (Particle)
    @returns force on A by B (Vector3)
    */
    public override Vector3 fieldDynamics(Particle A, Particle B) {

        float q1 = B.charge;
        float q2 = A.charge;
        
        float r = Vector3.Distance(A.getPos(), B.getPos());

        if (r < 2f) {
            r = 0.5f;
        }

        // TODO - make referecne to scales object from field class
        float con = A.scales.scaleFactor(k, 1, 3, -2, -2)*q1*q2/r;

        return con*Vector3.Normalize(A.getPos() - B.getPos());
    } 
}
