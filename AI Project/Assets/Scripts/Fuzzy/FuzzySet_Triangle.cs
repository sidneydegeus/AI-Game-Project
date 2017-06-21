using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuzzySet_Triangle : FuzzySet
{
    private double midPoint, leftOffset, rightOffset;

    public FuzzySet_Triangle(double _midPoint, double _leftOffset, double _rightOffset)
    {
        midPoint = _midPoint;
        leftOffset = _leftOffset;
        rightOffset = _rightOffset;
    }

    public new double CalculateDOM(double value)
    {
        if (((rightOffset == 0.0) && (midPoint == value)) || ((leftOffset == 0.0) && (midPoint == value)))
            return 1.0;

        //find DOM if left of center
        if ((value <= midPoint) && (value >= (midPoint - leftOffset)))
        {
            double grad = 1.0 / leftOffset;

            return grad * (value - (midPoint - leftOffset));
        }

        //find DOM if right of center
        else if ((value > midPoint) && (value < (midPoint + rightOffset)))
        {
            double grad = 1.0 / -rightOffset;

            return grad * (value - midPoint) + 1.0;
        }

        //out of range of this FLV, return zero
        else
            return 0.0;
    }
}