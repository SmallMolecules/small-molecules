using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @brief A DynamicField implementation of the Couloumb potential

    This class extends the DynamicField class and overrides the fieldDynamics 
    method to give a realistic implementation of the coloumb potential.
    @author Isaac Bergl
    @author Dhruv Jobanputra
    @date November 2021
    \see Lennard-Jones DynamicField StaticField
    */
public class Coloumb : DynamicField
{
    float constant;
    /**Contructor method - calls base constructor. This is where
    the constant SI units should be registered.
    @param sim - the parent Simulator (Simulator)*/
    public Coloumb(Simulator sim) : base(sim)
    {
        constant = sim.scales.constantFromSI(8.988E+9f, 1, 3, -2, -2);
    }

    /**
    Overriding function. Provides the dynamics of the coloumb potential.
    @param Particle A (Particle)
    @param Particle B (Particle)
    @returns force on A by B (Vector3)
    */
    public override Vector3 FieldDynamics(Particle A, Particle B)
    {
        float q1 = B.charge;
        float q2 = A.charge;

        float r = Vector3.Distance(A.GetPos(), B.GetPos());

        if (r < 2f)
        {
            r = 0.5f;
        }

        float coeff = constant * q1 * q2 / r;
        return coeff * Vector3.Normalize(A.GetPos() - B.GetPos());
    }
}
