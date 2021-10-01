using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scales 
{
    // One unit in unity represents x SI units
    private float time;
    private float length;

    private float DEFAULT_TIME = 0.0001f;
    private float MAX_TIME = 2f;

    private float DEFAULT_LENGTH = 10E-10f;
    private float MIN_LENGTH = 10E-20f;

    //constructors
    public Scales(float t, float l) {
        setTime(t);
        setLength(l);
    }

    public Scales() {
        setTime(DEFAULT_TIME);
        setLength(DEFAULT_LENGTH);
    }

    public float getTime() {
        return time;
    }

    public float getLength() {
        return length;
    }

    public void setTime(float t) {
        if (t < 0) {
            time = 0;
            return;
        }
        if (t > MAX_TIME) {
            time = MAX_TIME;
            return;
        }

        time = t;
    }

    public void setLength(float l) {
        if (l < 0) {
            length = MIN_LENGTH;
        }

        length = l;
    }

}
