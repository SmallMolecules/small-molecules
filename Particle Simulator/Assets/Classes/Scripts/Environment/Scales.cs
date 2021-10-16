using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/** @brief Class for managing scaling factors for a simulator

    This class manages scale factors such as as time, length, charge and mass. The purpose
    of abstracting these scales to a single class is so the physics caluculations can be 
    done in Unity space and translated to SI units upon the user's request. By doing so no 
    precison is lost to floating point rounding of tiny floats, and that the GameObject 
    attributes mirror the physical properties such length and position. Each scaled quantity
    is represented as a Struct (Scale).
    @author Isaac Bergl
    @date October 2021
    \see Scale Simulator
    */
public class Scales
{
    /**The time-scale in seconds (s)*/
    public Scale time;
    /**The length-scale in meters (m)*/
    public Scale length;
    /**The charge-scale in coloumbs (C)*/
    public Scale charge;
    /**The mass-scale in kilograms (kg)*/
    public Scale mass;


    private float DEFAULT_TIME_COEFF = 1.0f;
    private int DEFAULT_TIME_EXP = -9;

    private float DEFAULT_LENGTH_COEFF = 1.0f;
    private int DEFAULT_LENGTH_EXP = -9;

    /**
    Default constructor for the scales class. Sets all scales to the default values.
    */
    public Scales()
    {
        setTime(DEFAULT_TIME_COEFF, DEFAULT_TIME_EXP);
        setLength(DEFAULT_LENGTH_COEFF, DEFAULT_LENGTH_EXP);
        // TODO add checks for charge and mass
        charge = new Scale(1.6f, -19);
        mass = new Scale(1.6f, -27);
    }

    /**
    Used to set the timescale given a scientific notation representation
    of the value. This enables the program to extract the coefficient and the
    exponent seperately
    @param S, the input string (string)
    */
    public void setTime(string S)
    {
        string[] str = S.Split('e');

        string str_coeff = str[0];
        string str_exp = str[1];

        int mult = 1;
        if (str_coeff[0] == '-')
        {
            mult *= -1;
        }

        int counter = -1;
        foreach (char c in str_coeff)
        {
            counter++;
            if (c != '0' && c != '+' && c != '-')
            {
                break;
            }
        }

        float coeff = (float)Convert.ToDouble(str_coeff);
        int exp = Int32.Parse(str_exp.Substring(counter));

        time = new Scale(coeff, exp);
    }

    /**
    Sets the time-scale given a coefficent and an exponent
    @param coefficient (float)
    @param exponent (int)
    */
    public void setTime(float coefficent, int exponent)
    {
        time = new Scale(coefficent, exponent);
        return;
    }

    /**
    Sets the length-scale given a coefficent and an exponent
    @param coefficient (float)
    @param exponent (int)
    */
    public void setLength(float c, int e)
    {

        length = new Scale(c, e);
    }

    /**
    The purpose of this function is to be used in the DynamicField and StaticFeild classes.
    This solves the problem of scale invariance - for example a user may wish to use a constant
    value in SI units in their field implementation. However this constnat needs to be scaled 
    according to the values in the scales class to be representative of unity the unity units. 

    The use of this function is to pass in the value you wish to scale and the order of each of the 
    dimensions. For example if the input value (v) is of units Kg.m^-3.s^2 the approprate input would be 
    scaleFactor(v, 1, -3, 2, 0).

    Currently this function only supports length, mass, time and charge. 

    @param v - the value to convert (float)
    @param kg - the order of the mass dimension (int)
    @param m - the order of the length dimension (int)
    @param s - the order of the time dimension (int)
    @param q - the order of the charge dimension (int)
    @returns the scaled value (flaot)
    */
    public float scaleFactor(float v, int kg, int m, int s, int q)
    {
        Scale MASS = pow(mass, -kg);
        Scale LENGTH = pow(length, -m);
        Scale TIME = pow(time, -s);
        Scale CHARGE = pow(charge, -q);
        // Debug.Log(time);
        return v * multiply(multiply(MASS, LENGTH), multiply(TIME, CHARGE)).VAL;
    }

    /**
    Multiplies two Scale objects together
    @param a (Scale)
    @param b (Scale)
    @returns new scale (Scale)
    */
    public static Scale multiply(Scale a, Scale b)
    {
        float coeff = a.COEFF * b.COEFF;
        int EXP = a.EXP + b.EXP;
        return new Scale(coeff, EXP);
    }

    /**
    Raises a Scale to a power and returns a new scale
    @param scale (Scale)
    @param pow (int)
    */
    public static Scale pow(Scale scale, int pow)
    {
        return new Scale(Mathf.Pow(scale.COEFF, pow), scale.EXP * pow);
    }

}

/** @brief Struct for holding scale data

    This Struct is used effectively as a datatype, where the scale value (VAL)
    is stored separately as an coefficient (COEFF) and an exponent (EXP). For example 0.0001223
    is represented in a Scale as COEFF = 1.223, EXP = -4 and VAL = 0.0001223
    */
public struct Scale
{
    public float COEFF;
    public int EXP;
    public float VAL;

    /**Default constructor for a struct
    @param coeff (float)
    @param exp (int)
    */
    public Scale(float coeff, int exp)
    {
        COEFF = coeff;
        EXP = exp;
        VAL = (float)(COEFF * Mathf.Pow(10, EXP));
    }

    /**Overrides default "ToString" method*/
    public override string ToString()
    {
        return VAL.ToString("0.00");
    }
}
