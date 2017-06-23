using UnityEngine;
using System.Collections;

public class FuzzyRule
{

    private FuzzyTerm m_pAntecedent;
    private FuzzyTerm m_pConsequence;

    private FuzzyRule(FuzzyRule fuzzyRule) { }

    public FuzzyRule(FuzzyTerm _m_pAntecedent, FuzzyTerm _m_pConsequence)
    {
        m_pAntecedent = _m_pAntecedent.Clone();
        m_pConsequence = _m_pConsequence.Clone();
    }

    public void SetConfidenceOfConsequentToZero()
    {
        m_pConsequence.ClearDOM();
    }

    public void Calculate()
    {
        m_pConsequence.ORwithDOM(m_pAntecedent.GetDOM());

    }

}