using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuzzyRule
{
    private FuzzyTerm m_pAntecedent;
    private FuzzyTerm m_pConsequent;

    public FuzzyRule(FuzzyTerm _m_pAntecedent, FuzzyTerm _m_pConsequent)
    {
        m_pAntecedent = _m_pAntecedent;
        m_pConsequent = _m_pConsequent;
    }

    public void SetConfidenceOfConsequentToZero()
    {
        m_pConsequent.ClearDOM();
    }

    public void Calculate()
    {
        m_pConsequent.ORwithDOM(m_pAntecedent.GetDOM());
    }
}