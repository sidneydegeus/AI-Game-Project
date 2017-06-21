using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuzzySet_LeftShoulder : FuzzySet
{
    private double peak, leftOffset, rightOffset;

    public FuzzySet_LeftShoulder(double _peak, double _leftOffset, double _rightOffset)
    {
        peak = _peak;
        leftOffset = _leftOffset;
        rightOffset = _rightOffset;
    }

    public new double CalculateDOM(double value)
    {
        if (((rightOffset == 0.0) && ((peak == value))) || ((leftOffset == 0.0) && ((peak == value))))
            return 1.0;

        //find DOM if right of center
        else if ((value >= peak) && (value < (peak + rightOffset)))
        {
            double grad = 1.0 / -rightOffset;

            return grad * (value - peak) + 1.0;
        }

        //find DOM if left of center
        else if ((value < peak) && value >= (peak - leftOffset))
            return 1.0;

        //out of range of this FLV, return zero
        else
            return 0.0;
    }
}