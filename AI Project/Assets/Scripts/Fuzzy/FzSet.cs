using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FzSet : FuzzyTerm
{
    FuzzySet set;

    public FzSet() { }

    public FzSet(FuzzySet _set)
    {
        set = _set;
    }

    public void ClearDOM()
    {
        set.ClearDOM();
    }

    public FuzzyTerm Clone()
    {
        return null;
    }

    public double GetDOM()
    {
        return set.GetDOM();
    }

    public void ORwithDOM(double value)
    {
        set.ORwithDOM(value);
    }

    public FuzzySet GetSet()
    {
        return set;
    }
}