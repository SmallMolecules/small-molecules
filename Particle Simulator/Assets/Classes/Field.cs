using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field
{
    // Constants

    public Vector3 calculateForce(GameObject A, GameObject B) {

        return -1*Vector3.Normalize(-A.transform.position + B.transform.position);

    }
}
