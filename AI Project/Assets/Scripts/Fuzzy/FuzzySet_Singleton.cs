using UnityEngine;
using System.Collections;

public class FuzzySet_Singleton : FuzzySet
{
    private double m_dMidPoint;
    private double m_dLeftOffset;
    private double m_dRightOffset;

    public FuzzySet_Singleton(double _m_dMidPoint, double _m_dLeftOffset, double _m_dRightOffset) : base(_m_dMidPoint)
    {
        m_dMidPoint = _m_dMidPoint;
        m_dLeftOffset = _m_dLeftOffset;
        m_dRightOffset = _m_dRightOffset;
    }

    public override double CalculateDOM(double value)
    {
        if ((value >= m_dMidPoint - m_dLeftOffset) && (value <= m_dMidPoint + m_dRightOffset))
            return 1.0d;
        else
            return 0.0d;
    }
}