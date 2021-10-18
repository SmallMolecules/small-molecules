using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/** @brief A DynamicField implementation of the Lennard-Jones Potential

    This class extends the DynamicField class and overrides the fieldDynamics 
    method to give a realistic implementation of the Lennard-Jones Potential.
    @author Isaac Bergl
    @author Dhruv Jobanputra
    @date November 2021
    \see Coloumb DynamicField StaticField
    */
public class LennardJones : DynamicField
{
    /**The effective "size" of the particle*/
    float sigma;
    /**The dispersion energy*/
    float epsilon;
    /**Contructor method - calls base constructor. This is where
    the constant SI units should be assigned.
    @param sim - the parent Simulator (Simulator)*/
    public LennardJones(Simulator sim) : base(sim)
    {
        sigma = 2f;
        epsilon = 1E+18f;
    }

    /**
    Overriding function. Provides the dynamics of the lennard-jones potential.
    @param Particle A (Particle)
    @param Particle B (Particle)
    @returns force on A by B (Vector3)
    */
    public override Vector3 FieldDynamics(Particle A, Particle B)
    {
        float r = Vector3.Distance(A.GetPos(), B.GetPos());

        if (r < A.radius + B.radius)
        {
            r = 0.9f * (A.radius + B.radius);
        }

        float con = 4 * epsilon * (Mathf.Pow(sigma / r, 12) - Mathf.Pow(sigma / r, 6));

        return con * Vector3.Normalize(A.GetPos() - B.GetPos());
    }
}
