using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuzzyVariable
{
    private Dictionary<string, FuzzySet> m_MemberSets = new Dictionary<string, FuzzySet>();
    private double m_dMinRange, m_dMaxRange;

    private void AdjustRangeToFit(double min, double max)
    {
        if (min < m_dMinRange) m_dMinRange = min;
        if (max > m_dMaxRange) m_dMaxRange = max;
    }

    private double MinOf(double a, double b)
    {
        if (a > b)
            return a;
        else
            return b;
    }

    private FzSet fzSet(FuzzySet fuzzySet)
    {
        return new FzSet(fuzzySet);
    }

    public FzSet AddTriangularSet(string name, double minBound, double peak, double maxBound)
    {
        FuzzySet_Triangle triangle = new FuzzySet_Triangle(peak, peak - minBound, maxBound - peak);
        m_MemberSets.Add(name, triangle);

        AdjustRangeToFit(minBound, maxBound);

        FzSet fzSet = new FzSet(m_MemberSets[name]);
        return fzSet;
    }

    public FzSet AddRightShoulderSet(string name, double minBound, double peak, double maxBound)
    {
        FuzzySet_RightShoulder rightShoulder = new FuzzySet_RightShoulder(peak, peak - minBound, maxBound - peak);
        m_MemberSets.Add(name, rightShoulder);

        AdjustRangeToFit(minBound, maxBound);

        FzSet fzSet = new FzSet(m_MemberSets[name]);
        return fzSet;
    }

    public FzSet AddLeftShoulderSet(string name, double minBound, double peak, double maxBound)
    {
        FuzzySet_LeftShoulder leftShoulder = new FuzzySet_LeftShoulder(peak, peak - minBound, maxBound - peak);
        m_MemberSets.Add(name, leftShoulder);
        AdjustRangeToFit(minBound, maxBound);

        FzSet fzSet = new FzSet(m_MemberSets[name]);
        return fzSet;
    }

    public FzSet AddSingletonSet(string name, double minBound, double peak, double maxBound)
    {
        FuzzySet_Singleton singleTon = new FuzzySet_Singleton(peak, peak - minBound, maxBound - peak);
        m_MemberSets.Add(name, singleTon);

        AdjustRangeToFit(minBound, maxBound);

        FzSet fzSet = new FzSet(m_MemberSets[name]);
        return fzSet;
    }

    public void Fuzzify(double value)
    {
        foreach (FuzzySet fSet in m_MemberSets.Values)
            fSet.SetDOM(fSet.CalculateDOM(value));
    }

    public double DefuzzifyMaxAv()
    {
        double bottom = 0.0;
        double top = 0.0;

        foreach(FuzzySet fSet in m_MemberSets.Values)
        {
            bottom += fSet.GetDOM();
            top += fSet.GetRepresentativeValue() * fSet.GetDOM();
        }

        if (bottom == 0)
            return 0.0;

        return top / bottom;
    }

    public double DefuzzifyCentroid(int numberOfSamples)
    {
        //calculate the step size
        double StepSize = (m_dMaxRange - m_dMinRange) / (double)numberOfSamples;

        double TotalArea = 0.0;
        double SumOfMoments = 0.0;

        for (int i = 1; i <= numberOfSamples; i++)
        {
            foreach (FuzzySet fSet in m_MemberSets.Values)
            {
                double contribution =
                    MinOf(fSet.CalculateDOM(m_dMinRange + i * StepSize), fSet.GetDOM());

                TotalArea += contribution;

                SumOfMoments += (m_dMinRange + i * StepSize) * contribution;
            }
        }

        //make sure total area is not equal to zero
        if (0 == TotalArea) return 0.0;

        return (SumOfMoments / TotalArea);
    }
}