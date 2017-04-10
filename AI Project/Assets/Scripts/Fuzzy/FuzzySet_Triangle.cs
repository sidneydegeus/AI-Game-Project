using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuzzySet_Triangle : FuzzySet
{
    private double maxBound;
    private double minBound;
    private string m_dString;
    private double peak;

    public FuzzySet_Triangle(string m_dString, double minBound, double maxBound, double peak)
    {
        this.m_dString = m_dString;
        this.minBound = minBound;
        this.maxBound = maxBound;
        this.peak = peak;
    }

    public override double CalculateDOM(double value)
    {
        //if(maxBound == peak)
        return 0;
    }

    public override void ClearDOM()
    {
        throw new NotImplementedException();
    }

    public override double GetDOM()
    {
        throw new NotImplementedException();
    }

    public override void ORwithDOM(double value)
    {
        throw new NotImplementedException();
    }
}