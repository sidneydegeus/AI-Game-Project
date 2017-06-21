using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface FuzzyTerm
{
    FuzzyTerm Clone();
    double GetDOM();
    void ClearDOM();
    void ORwithDOM(double value);
}