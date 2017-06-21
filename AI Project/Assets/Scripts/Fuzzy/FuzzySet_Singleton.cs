using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuzzySet_Singleton : FuzzySet
{
    private double midPoint, leftOffset, rightOffset;

    public FuzzySet_Singleton(double _midPoint, double _leftOffset, double _rightOffset)
    {
        midPoint = _midPoint;
        leftOffset = _leftOffset;
        rightOffset = _rightOffset;
    }

    public new double CalculateDOM(double value)
    {
        if ((value >= midPoint - leftOffset) && (value <= midPoint + rightOffset))
            return 1.0;

        //out of range of this FLV, return zero
        else
            return 0.0;
    }
}