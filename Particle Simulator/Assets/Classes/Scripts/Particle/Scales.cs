using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scales 
{
    // One unit in unity represents x SI units
    private Scale time;
    private Scale length;
    private Scale charge;
    private Scale mass;

    private float DEFAULT_TIME_COEFF = 1.0f;
    private int DEFAULT_TIME_EXP = -6;
    // private float MAX_TIME = 2f;

    private float DEFAULT_LENGTH_COEFF = 1.0f;
    private int DEFAULT_LENGTH_EXP = -9;
    // private float MIN_LENGTH = 10E-20f;

    //constructors
    // public Scales(float t, int t_e, float l, int l_e) {
    //     setTime(t, t_e);
    //     setLength(l, l_e);
    // }

    public Scales() {
        setTime(DEFAULT_TIME_COEFF, DEFAULT_TIME_EXP);
        setLength(DEFAULT_LENGTH_COEFF, DEFAULT_LENGTH_EXP);
        charge = new Scale (1.6f, -19);
        mass = new Scale(1.6f, -27);
    }

    public float getTime() {
        return time.VAL;
    }

    public float getLength() {
        return length.VAL;
    }

    public void setTime(float c, int e) {
        // if (t < 0) {
        //     time = new Scale(t);
        //     return;
        // }
        // if (t > MAX_TIME) {
        //     time = new Scale(MAX_TIME);
        //     return;
        // }

        time = new Scale(c, e);
        return;
    }

    public void setLength(float c, int e) {
        // if (l < 0) {
        //     length = new Scale(MIN_LENGTH);
        // }

        length = new Scale(c, e);
    }

    public float scaleFactor(float v, int kg, int m, int s, int q) {
        Scale MASS = pow(mass, kg);
        Scale LENGTH = pow(length, m);
        Scale TIME = pow(time,s);
        Scale CHARGE = pow(charge, q);

        // Debug.Log("Start");
        // Debug.Log(MASS.ToString());
        //  Debug.Log(LENGTH.ToString());
        //   Debug.Log(TIME.ToString());
        //    Debug.Log(CHARGE.ToString());
        Debug.Log(multiply(multiply(MASS, LENGTH), multiply(TIME, CHARGE)));
        return multiply(multiply(MASS, LENGTH), multiply(TIME, CHARGE)).VAL;
    }

    public static Scale multiply(Scale a, Scale b) {
        float coeff = a.COEFF * b.COEFF;
        int EXP = a.EXP + b.EXP;
        return new Scale(coeff, EXP);
    }

    public static Scale pow(Scale a, int pow) {
        return new Scale(a.COEFF, a.EXP * pow);
    }

}

public struct Scale
{
    public float COEFF;
    public int EXP;
    public float VAL;

    public Scale(float coeff, int exp)
    {
        COEFF = coeff;
        EXP = exp;
        VAL = (float)(COEFF*Mathf.Pow(10, EXP));
    }

    public override string ToString() => $"{COEFF}E{EXP}";
}
