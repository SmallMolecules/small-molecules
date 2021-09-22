using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scales 
{
    // One unit in unity represents x SI units
    private float time;
    private float length;

    //constructor
    public Scales(float t = 0.000001f, float l=10E-10f) {
        setTime(t);
        setLength(l);
    }

    public float getTime() {
        return time;
    }

    public float getLength() {
        return length;
    }

    public void setTime(float t) {
        time = t;//TODO - checks
    }

    public void setLength(float l) {
        length = l;
    }

}
