using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FzAND : FuzzyTerm
{
    private List<FuzzyTerm> m_Terms;

    public FzAND(FuzzyTerm op1)
    {
        m_Terms.Add(op1);
    }

    public FzAND(FuzzyTerm op1, FuzzyTerm op2)
    {
        m_Terms.Add(op1);
        m_Terms.Add(op2);
    }

    public FzAND(FuzzyTerm op1, FuzzyTerm op2, FuzzyTerm op3)
    {
        m_Terms.Add(op1);
        m_Terms.Add(op2);
        m_Terms.Add(op3);
    }

    public FzAND(FuzzyTerm op1, FuzzyTerm op2, FuzzyTerm op3, FuzzyTerm op4)
    {
        m_Terms.Add(op1);
        m_Terms.Add(op2);
        m_Terms.Add(op3);
        m_Terms.Add(op4);
    }

    public void ClearDOM()
    {
        foreach (FuzzyTerm term in m_Terms)
        {
            term.ClearDOM();
        }
    }

    public FuzzyTerm Clone()
    {
        return null;
    }

    public double GetDOM()
    {
        double smallest = double.MaxValue;

        foreach (FuzzyTerm term in m_Terms)
        {
            if (term.GetDOM() < smallest)
            {
                smallest = term.GetDOM();
            }
        }
        return smallest;

    }

    public void ORwithDOM(double value)
    {
        foreach (FuzzyTerm term in m_Terms)
            term.ORwithDOM(value);
    }
}