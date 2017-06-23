using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class FuzzyModule
{
    Dictionary<string, FuzzyVariable> m_Variables = new Dictionary<string, FuzzyVariable>();

    private IList<FuzzyRule> m_Rules = new List<FuzzyRule>();

    public static readonly int NumSamples = 15;

    public enum DefuzzifyMethod
    {
        MaxAV,
        Centroid
    };

    private void SetConfidencesOfConsequentsToZero()
    {
        foreach (FuzzyRule fuzzyRule in m_Rules)
            fuzzyRule.SetConfidenceOfConsequentToZero();
    }

    public FuzzyVariable CreateFLV(string name)
    {
        FuzzyVariable fuzzyVariable = new FuzzyVariable();
        m_Variables.Add(name, fuzzyVariable);
        m_Variables.TryGetValue(name, out fuzzyVariable);
        return fuzzyVariable;
    }

    public void AddRule(FuzzyTerm antecedent, FuzzyTerm consequence)
    {
        m_Rules.Add(new FuzzyRule(antecedent, consequence));
    }

    public void Fuzzify(string name, double value)
    {
        if (m_Variables.ContainsKey(name))
        {
            FuzzyVariable fuzzyVariable;
            m_Variables.TryGetValue(name, out fuzzyVariable);
            fuzzyVariable.Fuzzify(value);
        }
    }

    public double DeFuzzify(string name, DefuzzifyMethod method)
    {
        if (m_Variables.ContainsKey(name))
        {
            SetConfidencesOfConsequentsToZero();

            foreach (FuzzyRule fuzzyRule in m_Rules)
                fuzzyRule.Calculate();

            switch (method)
            {
                case DefuzzifyMethod.Centroid:
                    FuzzyVariable fuzzyVariable;
                    m_Variables.TryGetValue(name, out fuzzyVariable);
                    return fuzzyVariable.DefuzzifyCentroid(NumSamples);

                case DefuzzifyMethod.MaxAV:
                    FuzzyVariable fuzzyVariableMaxAv;
                    m_Variables.TryGetValue(name, out fuzzyVariableMaxAv);
                    return fuzzyVariableMaxAv.DeFuzzifyMaxAv();
            }
            return 0.0f;
        }
        return 0.0f;

    }

}