using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuzzySet_RightShoulder : FuzzySet
{
    private double peak, leftOffset, rightOffset;

    public FuzzySet_RightShoulder(double _peak, double _leftOffset, double _rightOffset)
    {
        peak = _peak;
        leftOffset = _leftOffset;
        rightOffset = _rightOffset;
    }

    public new double CalculateDOM(double value)
    {
        if (((rightOffset == 0.0) && ((peak == value))) || ((leftOffset == 0.0) && ((peak == value))))
            return 1.0;

        //find DOM if left of center
        else if ((value <= peak) && (value > (peak - leftOffset)))
        {
            double grad = 1.0 / leftOffset;

            return grad * (value - (peak - leftOffset));
        }
        //find DOM if right of center and less than center + right offset
        else if ((value > peak) && (value <= (peak + rightOffset)))
            return 1.0;
        else
            return 0;
    }
}