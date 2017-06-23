using UnityEngine;
using System.Collections;

public class FuzzySet_Triangle : FuzzySet
{
    private double m_dPeakPoint;
    private double m_dLeftOffset;
    private double m_dRightOffset;

    public FuzzySet_Triangle(double _m_dPeakPoint, double _m_dLeftOffset, double _m_dRightOffset) : base(_m_dPeakPoint)
    {
        m_dPeakPoint = _m_dPeakPoint;
        m_dLeftOffset = _m_dLeftOffset;
        m_dRightOffset = _m_dRightOffset;
    }

    public override double CalculateDOM(double value)
    {
        if ((isEqual(m_dRightOffset, 0.0d) && (isEqual(m_dPeakPoint, value))) || (isEqual(m_dLeftOffset, 0.0d) && (isEqual(m_dPeakPoint, value))))
            return 1.0d;

        if ((value <= m_dPeakPoint) && (value >= (m_dPeakPoint - m_dLeftOffset)))
        {
            double grad = 1.0d / m_dLeftOffset;
            return grad * (value - (m_dPeakPoint - m_dLeftOffset));
        }

        else if ((value > m_dPeakPoint) && (value < (m_dPeakPoint + m_dRightOffset)))
        {
            double grad = 1.0d / -m_dRightOffset;
            return grad * (value - m_dPeakPoint) + 1.0d;
        }

        else
            return 0.0d;
    }

    public bool isEqual(double a, double b)
    {
        if (Mathf.Abs((float)a - (float)b) < 0.000000000001d)
            return true;

        return false;
    }
}