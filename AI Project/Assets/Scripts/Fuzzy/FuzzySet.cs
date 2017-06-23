using UnityEngine;
using System.Collections;
using System;

public abstract class FuzzySet : ICloneable
{
    protected double m_dDOM;
    protected double m_dRepresentativeValue;

    public FuzzySet(double _m_dRepresentativeValue)
    {
        m_dDOM = 0.0d;
        m_dRepresentativeValue = _m_dRepresentativeValue;
    }

    public abstract double CalculateDOM(double value);

    public void ORwithDOM(double value)
    {
        if (value > m_dDOM)
        {
            m_dDOM = value;
        }
    }

    public double GetRepresentativeVal()
    {
        return m_dRepresentativeValue;
    }

    public void ClearDOM()
    {
        m_dDOM = 0.0d;
    }

    public void SetDOM(double value)
    {
        m_dDOM = value;
    }

    public double GetDOM()
    {
        return m_dDOM;
    }

    public FuzzySet clone()
    {
        return (FuzzySet)base.MemberwiseClone();
    }

    public object Clone()
    {
        return (FuzzySet)this.MemberwiseClone();
    }
}