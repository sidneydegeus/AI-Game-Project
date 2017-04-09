using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FuzzySet
{
    public double m_dDOM;
    public double m_dReprensativeValue;

    public abstract double GetDOM();
    public abstract void ClearDOM();
    public abstract void ORwithDOM(double value);
    public abstract double CalculateDOM(double value);
}