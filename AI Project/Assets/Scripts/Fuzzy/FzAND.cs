using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FzAND : FuzzyTerm
{
    private IList<FuzzyTerm> m_Terms = new List<FuzzyTerm>(4);

    public FzAND(FzAND fzAnd)
    {
        foreach (FuzzyTerm fuzzyTerm in fzAnd.m_Terms)
            m_Terms.Add(fuzzyTerm.Clone());
    }

    public FzAND(FuzzyTerm op1, FuzzyTerm op2)
    {
        m_Terms.Add(op1.Clone());
        m_Terms.Add(op2.Clone());
    }

    public FzAND(FuzzyTerm op1, FuzzyTerm op2, FuzzyTerm op3)
    {
        m_Terms.Add(op1.Clone());
        m_Terms.Add(op2.Clone());
        m_Terms.Add(op3.Clone());
    }

    public FzAND(FuzzyTerm op1, FuzzyTerm op2, FuzzyTerm op3, FuzzyTerm op4)
    {
        m_Terms.Add(op1.Clone());
        m_Terms.Add(op2.Clone());
        m_Terms.Add(op3.Clone());
        m_Terms.Add(op4.Clone());
    }

    public override FuzzyTerm Clone()
    {
        return new FzAND(this);
    }

    public override double GetDOM()
    {
        double smallest = double.MaxValue;
        foreach (FuzzyTerm fuzzyTerm in m_Terms)
        {
            if (fuzzyTerm.GetDOM() < smallest)
                smallest = fuzzyTerm.GetDOM();
        }
        return smallest;
    }

    public override void ClearDOM()
    {
        foreach (FuzzyTerm fuzzyTerm in m_Terms)
            fuzzyTerm.ClearDOM();
    }

    public override void ORwithDOM(double value)
    {
        foreach (FuzzyTerm fuzzyTerm in m_Terms)
            fuzzyTerm.ORwithDOM(value);
    }
}