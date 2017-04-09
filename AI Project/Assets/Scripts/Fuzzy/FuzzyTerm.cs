using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FuzzyTerm
{
    public abstract double GetDOM();
    public abstract void ClearDOM();
    public abstract void ORwithDOM(double value);
    public abstract double CalculateDOM(double value);
}