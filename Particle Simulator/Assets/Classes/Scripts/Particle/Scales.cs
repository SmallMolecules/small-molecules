using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
// using System.Convert;

public class Scales 
{
    // One unit in unity represents x SI units
    public Scale time;
    public Scale length;
    public Scale charge;
    public Scale mass;

    private float DEFAULT_TIME_COEFF = 1.0f;
    private int DEFAULT_TIME_EXP = -9;

    private float DEFAULT_LENGTH_COEFF = 1.0f;
    private int DEFAULT_LENGTH_EXP = -9;

    public Scales() {
        setTime(DEFAULT_TIME_COEFF, DEFAULT_TIME_EXP);
        setLength(DEFAULT_LENGTH_COEFF, DEFAULT_LENGTH_EXP);
        // TODO add checks for charge and mass
        charge = new Scale (1.6f, -19);
        mass = new Scale(1.6f, -27);
    }

    // unused but may be needed to convert arbitary float string to a scale
    public void setTime(string S) {
        string[] str = S.Split('e');

        string str_coeff = str[0];
        string str_exp = str[1];
        
        int mult = 1;
        if (str_coeff[0] == '+') {
            mult *= -1;
        }

        int counter = -1;
        foreach (char c in str_coeff) {
            counter++;
            if (c != '0' && c != '+' && c != '-') {
                break;
            }
        }

        float coeff = (float) Convert.ToDouble(str_coeff);
        int exp = Int32.Parse(str_exp.Substring(counter));
        
        time = new Scale(coeff, exp);
    }

    public void setTime(float c, int e) {
        time = new Scale(c, e);
        return;
    }

    public void setLength(float c, int e) {

        length = new Scale(c, e);
    }

    // function that scales a coefficient involving mass, length, time and charge
    public float scaleFactor(float v, int kg, int m, int s, int q) {
        Scale MASS = pow(mass, -kg);
        Scale LENGTH = pow(length, -m);
        Scale TIME = pow(time, -s);
        Scale CHARGE = pow(charge, -q);

        return v*multiply(multiply(MASS, LENGTH), multiply(TIME, CHARGE)).VAL;
    }

    // function to multiply scales
    public static Scale multiply(Scale a, Scale b) {
        float coeff = a.COEFF * b.COEFF;
        int EXP = a.EXP + b.EXP;
        return new Scale(coeff, EXP);
    }

    // functions to raise scale to a power
    public static Scale pow(Scale a, int pow) {
        return new Scale(Mathf.Pow(a.COEFF, pow), a.EXP * pow);
    }

}

// struct for storing scale data
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

    public override string ToString() {
        return VAL.ToString("0.00");
    } 
}
