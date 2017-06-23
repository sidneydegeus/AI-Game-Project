using UnityEngine;
using System.Collections;

public class FzVery : FuzzyTerm
{
    private FuzzySet m_Set;

    private FzVery(FzVery fzVery)
    {
        m_Set = fzVery.m_Set;
    }

    public FzVery(FzSet fzVery)
    {
        m_Set = fzVery.m_Set.clone();
    }

    public override double GetDOM()
    {
        return m_Set.GetDOM() * m_Set.GetDOM();
    }

    public override FuzzyTerm Clone()
    {
        return new FzVery(this);
    }

    public override void ClearDOM()
    {
        m_Set.ClearDOM();
    }

    public override void ORwithDOM(double value)
    {
        m_Set.ORwithDOM(value * value);
    }
}