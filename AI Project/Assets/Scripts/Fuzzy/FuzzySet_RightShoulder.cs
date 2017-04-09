using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuzzySet_RightShoulder : FuzzySet
{
    private double maxBound;
    private double minBound;
    private string m_dString;
    private double peak;
    private double DOM = 0.0;

    public FuzzySet_RightShoulder(string m_dString, double minBound, double maxBound, double peak)
    {
        this.m_dString = m_dString;
        this.minBound = minBound;
        this.maxBound = maxBound;
        this.peak = peak;
    }

    public override double CalculateDOM(double value)
    {
        if (value <= minBound)
            return DOM;
        if (value >= peak)
            return DOM = 1.0;

        DOM = 1 / Math.Abs(minBound - peak) * Math.Abs(minBound - value);
        return DOM;
    }

    public override void ClearDOM()
    {
        DOM = 0.0;
    }

    public override double GetDOM()
    {
        return DOM;
    }

    public override void ORwithDOM(double value)
    {
        throw new NotImplementedException();
    }
}
