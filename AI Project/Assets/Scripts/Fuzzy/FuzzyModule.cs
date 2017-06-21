using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuzzyModule
{
    public enum DefuzzifyMethod { MaxAv, Centroid };

    private Dictionary<string, FuzzyVariable> m_Variables = new Dictionary<string, FuzzyVariable>();
    private List<FuzzyRule> m_Rules = new List<FuzzyRule>();
    private int numberOfSamples = 15;

    public FuzzyVariable CreateFLV(string m_Name)
    {
        FuzzyVariable newVariable = new FuzzyVariable();
        m_Variables.Add(m_Name, newVariable);
        return newVariable;
    }

    public void AddRule(FuzzyTerm m_pAntecedent, FuzzyTerm m_pConsequent)
    {
        m_Rules.Add(new FuzzyRule(m_pAntecedent, m_pConsequent));
    }
    
    public void Fuzzify(string name, double val)
    {
        if (m_Variables.ContainsKey(name))
            m_Variables[name].Fuzzify(val);
    }

    public double Defuzzify(string name, DefuzzifyMethod method)
    {
        if (m_Variables.ContainsKey(name))
        {
            SetConfidencesOfConsequentsToZero();

            foreach (FuzzyRule rule in m_Rules)
                rule.Calculate();

            switch (method)
            {
                case DefuzzifyMethod.Centroid:
                    return m_Variables[name].DefuzzifyCentroid(numberOfSamples);
                case DefuzzifyMethod.MaxAv:
                    return m_Variables[name].DefuzzifyMaxAv();
            }
        }
        return 0;
    }

    void SetConfidencesOfConsequentsToZero()
    {
        foreach (FuzzyRule rule in m_Rules)
            rule.SetConfidenceOfConsequentToZero();
    }
}