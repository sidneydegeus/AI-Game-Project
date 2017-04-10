using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuzzyVariable
{
    public Dictionary<string, FuzzySet> m_MemberSets = new Dictionary<string, FuzzySet>();

    public double m_dMinRange;
    public double m_dMaxRange;
    public string m_dString;

    public FuzzyVariable(string m_dString)
    {
        m_dMinRange = 0;
        m_dMaxRange = 0;
        this.m_dString = m_dString;
    }

    public FuzzyVariable()
    {
    }

    public FuzzySet AddTriangular(string m_dString, double minBound, double maxBound, double peak)
    {
        FuzzySet_Triangle leftShoulder = new FuzzySet_Triangle(m_dString, minBound, maxBound, peak);
        //memberSets.Add(m_dString, leftShoulder);

        return leftShoulder;
    }

    public FuzzySet AddRightShoulderSet(string m_dString, double minBound, double maxBound, double peak)
    {
        FuzzySet_RightShoulder rightShoulder = new FuzzySet_RightShoulder(m_dString, minBound, maxBound, peak);
        //memberSets.Add(m_dString, rightShoulder);

        return rightShoulder;
    }

    public FuzzySet AddLeftShoulderSet(string m_dString, double minBound, double maxBound, double peak)
    {
        FuzzySet_LeftShoulder leftShoulder = new FuzzySet_LeftShoulder(m_dString, minBound, maxBound, peak);
        //memberSets.Add(m_dString, leftShoulder);

        return leftShoulder;
    }

    public void Fuzzify(double value)
    {
        foreach (KeyValuePair<string, FuzzySet> kvp in m_MemberSets)
        {
          
        }
    }

    public float DefuzzifyMaxAv()
    {
        throw new NotImplementedException();
    }

    public float DefuzzifyCentroid()
    {
        throw new NotImplementedException();
    }
}