using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @brief A StaticField implementation of a wind vector field

    This class extends the DynamicField class and overwrites the fieldDynamics 
    method to give an implementation of a wind vector field.
    @author Isaac Bergl
    @date November 2021
    \see StaticField
    */
public class Wind: StaticField
{
    /**The wind strength in SI units*/
    private float k = 1.0E+10f;

    /**
    Overriding function. Provides the dynamics of a wind vector field.
    @param Particle A (Particle)
    @param Particle B (Particle)
    @returns force on A by B (Vector3)
    */
    public override Vector3 fieldDynamics(Particle A) {
        return k*(new Vector3(0.0f, 1.0f, 0.0f));
    }   

    
}
