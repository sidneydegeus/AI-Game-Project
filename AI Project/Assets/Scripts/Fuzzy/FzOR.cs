using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FzOR : FuzzyTerm
{
    List<FuzzyTerm> m_Terms;

    public FzOR(FuzzyTerm op1, FuzzyTerm op2)
    {
        m_Terms.Add(op1);
        m_Terms.Add(op2);
    }

    public FzOR(FuzzyTerm op1, FuzzyTerm op2, FuzzyTerm op3)
    {
        m_Terms.Add(op1);
        m_Terms.Add(op2);
        m_Terms.Add(op3);
    }

    public FzOR(FuzzyTerm op1, FuzzyTerm op2, FuzzyTerm op3, FuzzyTerm op4)
    {
        m_Terms.Add(op1);
        m_Terms.Add(op2);
        m_Terms.Add(op3);
        m_Terms.Add(op4);
    }

    public void ClearDOM(){}

    public FuzzyTerm Clone() {
        return null;
    }

    public double GetDOM()
    {
        double largest = double.MinValue;

        foreach (FuzzyTerm term in m_Terms)
        {
            if (term.GetDOM() > largest)
                largest = term.GetDOM();
        }

        return largest;
    }

    public void ORwithDOM(double value){}
}