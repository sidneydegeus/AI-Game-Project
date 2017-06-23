using UnityEngine;
using System.Collections;

public class FzSet : FuzzyTerm
{
    public FuzzySet m_Set;

    public FzSet(FuzzySet fuzzySet)
    {
        m_Set = fuzzySet;
    }

    private FzSet(FzSet fzSet)
    {
        m_Set = fzSet.m_Set;
    }

    public override FuzzyTerm Clone()
    {
        return (FuzzyTerm) new FzSet(this);
    }

    public override double GetDOM()
    {
        return m_Set.GetDOM();
    }

    public override void ClearDOM()
    {
        m_Set.ClearDOM();
    }

    public override void ORwithDOM(double value)
    {
        m_Set.ORwithDOM(value);
    }


}