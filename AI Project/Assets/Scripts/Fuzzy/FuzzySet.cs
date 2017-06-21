using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FuzzySet 
{
    double m_dDOM;
    double m_dRepresentativeValue;

    public FuzzySet(double _m_dRepresentativeValue)
    {
        m_dDOM = 0.0;
        m_dRepresentativeValue = _m_dRepresentativeValue;
    }

    public FuzzySet() { }

    public double GetRepresentativeValue() {
        return m_dRepresentativeValue;
    }

    public double CalculateDOM(double value) {
        return 0;
    }

    public double GetDOM() {
        return m_dDOM;
    }

    public void SetDOM(double value)
    {
        if(value > 1 || value < 0)
            throw new ArgumentOutOfRangeException();

        m_dDOM = value;            
    }

    public void ClearDOM() {
        m_dDOM = 0.0;
    }

    public void ORwithDOM(double value) {
        if (value > m_dDOM)
            m_dDOM = value;
    }
}