using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FzFairly : FuzzyTerm
{
    private FuzzySet set;

    public FzFairly(FzSet _set)
    {
        set = _set.GetSet();
    }

    public void ClearDOM()
    {
        set.ClearDOM();
    }

    public FuzzyTerm Clone()
    {
        return this; ;
    }

    public double GetDOM()
    {
        return Math.Sqrt(set.GetDOM());
    }

    public void ORwithDOM(double value)
    {
        set.ORwithDOM(Math.Sqrt(value));
    }
}