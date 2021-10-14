using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/** @brief A DynamicField implementation of the Lennard-Jones Potential

    This class extends the DynamicField class and overrides the fieldDynamics 
    method to give a realistic implementation of the Lennard-Jones Potential.
    @author Isaac Bergl
    @date November 2021
    \see Coloumb DynamicField StaticField
    */
public class LennardJones : DynamicField
{
    /**Contructor method - calls base constructor. This is where
    the constant SI units should be registered.
    @param sim - the parent Simulator (Simulator)*/
    public LennardJones(Simulator sim) : base(sim)
    {
        int[] unitsSigma = { 0, 1, 0, 0 };
        registerConstant("sigma", 1E-9f, unitsSigma);
        int[] unitsEpsilon = { 1, 1, -2, 0 };
        registerConstant("epsilson", 1E-9f, unitsEpsilon);

    }

    /**
    Overriding function. Provides the dynamics of the lennard-jones potential.
    @param Particle A (Particle)
    @param Particle B (Particle)
    @returns force on A by B (Vector3)
    */
    public override Vector3 fieldDynamics(Particle A, Particle B)
    {

        float r = Vector3.Distance(A.getPos(), B.getPos());

        float SIGMA = constants["sigma"];
        float EPSILON = constants["epsilon"];

        //TODO - autimatically calculate collision distance
        if (r < 1.7f)
        {
            r = 1.7f;
        }

        float con = 4 * EPSILON * (Mathf.Pow(SIGMA / r, 12) - Mathf.Pow(SIGMA / r, 6));

        return con * Vector3.Normalize(A.getPos() - B.getPos());


    }


}
