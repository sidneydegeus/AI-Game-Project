using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuzzyModule
{
    public List<FuzzyRule> m_Rules = new List<FuzzyRule>();
    public Dictionary<string, FuzzyVariable> m_Variables = new Dictionary<string, FuzzyVariable>();

    public void RunRules()
    {
        
    }

    public FuzzyVariable CreateFLV(string VarName)
    {
        FuzzyVariable fv;
        m_Variables.Add(VarName, new FuzzyVariable());
        m_Variables.TryGetValue(VarName, out fv);
        return fv;
    }

    public void AddRule(FuzzyTerm antecedent, FuzzyTerm consequence)
    {
        m_Rules.Add(new FuzzyRule(antecedent, consequence));
    }

    public void Fuzzify(string NameOfFLV, double val)
    {
        if (m_Variables.ContainsKey(NameOfFLV))
        {
            FuzzyVariable f;
            m_Variables.TryGetValue(NameOfFLV, out f);
            f.Fuzzify(val);
        }
    }
}