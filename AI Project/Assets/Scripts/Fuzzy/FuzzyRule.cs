using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuzzyRule
{
    public FuzzyTerm m_pAntescedent;
    public FuzzyTerm m_pConsequent;

    public FuzzyRule(FuzzyTerm _m_pAntescedent, FuzzyTerm _m_pConsequent)
    {
        m_pAntescedent = _m_pAntescedent;
        m_pConsequent = _m_pConsequent;
    }

    public void Calculate()
    {
        m_pConsequent.ORwithDOM(m_pAntescedent.GetDOM());
    }
}