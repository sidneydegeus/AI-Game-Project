using UnityEngine;
using System.Collections;

public abstract class FuzzyTerm
{
    public abstract FuzzyTerm Clone();
    public abstract double GetDOM();
    public abstract void ClearDOM();
    public abstract void ORwithDOM(double val);
}