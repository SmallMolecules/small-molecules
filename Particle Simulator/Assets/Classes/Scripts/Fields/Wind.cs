using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @brief A StaticField implementation of a wind vector field

    This class extends the DynamicField class and overrides the fieldDynamics 
    method to give an implementation of a wind vector field.
    @author Isaac Bergl
    @author Dhruv Jobanputra
    @date November 2021
    \see StaticField
    */
public class Wind : StaticField
{
    float wind;
    /**Contructor method - calls base constructor. This is where
    the constant SI units should be registered.
    @param sim - the parent Simulator (Simulator)*/
    public Wind(Simulator sim) : base(sim)
    {
        wind = sim.scales.constantFromSI(1.0E+10f, 1, 1, -2, 0);
    }

    /**
    Overriding function. Provides the dynamics of a wind vector field.
    @param Particle A (Particle)
    @param Particle B (Particle)
    @returns force on A by B (Vector3)
    */
    public override Vector3 FieldDynamics(Particle A)
    {
        return wind * (new Vector3(0.0f, 1.0f, 0.0f));
    }


}
