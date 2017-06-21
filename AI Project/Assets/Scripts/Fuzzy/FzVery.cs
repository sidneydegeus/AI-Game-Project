using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FzVery : FuzzyTerm
{
    FzSet set;

    public FzVery(FzSet _set)
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
        return set.GetDOM() * set.GetDOM();
    }

    public void ORwithDOM(double value)
    {
        set.ORwithDOM(value * value);
    }
}