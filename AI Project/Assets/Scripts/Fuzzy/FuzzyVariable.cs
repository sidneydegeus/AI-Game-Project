using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FuzzyVariable {
		
	private Dictionary<string, FuzzySet> m_MemberSets = new Dictionary<string, FuzzySet>();
	private Dictionary<string, double> m_domDictionary = new Dictionary<string, double> ();
	
	private FuzzyVariable(FuzzyVariable fv) {}
	
	private double m_dMinRange;
	private double m_dMaxRange;
	
	private void AdjustRangeToFit(double minBound, double maxBound) {
		if (minBound < m_dMinRange) {
			m_dMinRange = minBound;
		}
		if (maxBound > m_dMaxRange) {
			m_dMaxRange = maxBound;
		}
	}
	
	public FuzzyVariable() {
		m_dMinRange = 0.0d;
		m_dMaxRange = 0.0d;
	}

    public FzSet AddLeftShoulderSet(string name, double minBound, double peak, double maxBound) {
		m_MemberSets.Add (name, new FuzzySet_LeftShoulder (peak, peak - minBound, maxBound - peak));
		
		AdjustRangeToFit (minBound, maxBound);
		
		FuzzySet fuzzySet;
		m_MemberSets.TryGetValue (name, out fuzzySet);
		return new FzSet (fuzzySet);
	}
	
	public FzSet AddRightShoulderSet(string name, double minBound, double peak, double maxBound) {
		m_MemberSets.Add (name, new FuzzySet_RightShoulder (peak, peak - minBound, maxBound - peak));
		
		AdjustRangeToFit (minBound, maxBound);
		
		FuzzySet fuzzySet;
		m_MemberSets.TryGetValue (name, out fuzzySet);
		return new FzSet (fuzzySet);
		
	}
	
	public FzSet AddTriangularSet(string name, double minBound, double peak, double maxBound) {
		m_MemberSets.Add (name, new FuzzySet_Triangle (peak, peak - minBound, maxBound - peak));
		
		AdjustRangeToFit (minBound, maxBound);
		
		FuzzySet fuzzySet;
		m_MemberSets.TryGetValue (name, out fuzzySet);
		return new FzSet (fuzzySet);
		
	}
	
	public FzSet AddSingletonSet(string name, double minBound, double peak, double maxBound) {
		m_MemberSets.Add (name, new FuzzySet_Singleton (peak, peak - minBound, maxBound - peak));
		
		AdjustRangeToFit (minBound, maxBound);
		
		FuzzySet fuzzySet;
		m_MemberSets.TryGetValue (name, out fuzzySet);
		return new FzSet (fuzzySet);
		
	}
	
	public void Fuzzify(double value) {
		if (value >= m_dMinRange && value <= m_dMaxRange) {}

		foreach (KeyValuePair<string, FuzzySet> keyValuePair in m_MemberSets) {
            keyValuePair.Value.SetDOM(keyValuePair.Value.CalculateDOM(value));
		}
	}
	
	public double DeFuzzifyMaxAv() {
		double bottom = 0.0d;
		double top = 0.0d;

		foreach (KeyValuePair<string, FuzzySet> keyValuePair in m_MemberSets) {
			bottom += keyValuePair.Value.GetDOM();
			top += keyValuePair.Value.GetRepresentativeVal() * keyValuePair.Value.GetDOM();
		}
		
		if (isEqual (0.0d, bottom))
			return 0.0d;
		
		return top / bottom;
	}
	
	public double DefuzzifyCentroid(int NumSamples) {
		double StepSize = (m_dMaxRange - m_dMinRange) / (double)NumSamples;
		
		double TotalArea = 0.0d;
		double SumOfMoments = 0.0d;
		
		for (int i = 1; i <= NumSamples; i++) {
			foreach (KeyValuePair<string, FuzzySet> keyValuePair in m_MemberSets) {
				double contribution = Mathf.Min ((float)keyValuePair.Value.CalculateDOM(m_dMinRange + i * StepSize), (float)keyValuePair.Value.GetDOM());
				
				TotalArea += contribution;
				
				SumOfMoments += (m_dMinRange + i * StepSize) * contribution;
			}
		}
		
		if (isEqual (0.0d, TotalArea))
			return 0.0d;
		
		return (SumOfMoments / TotalArea);
	}	
	
	public bool isEqual(double a, double b) {
		if (Mathf.Abs ((float)a - (float)b) < 0.000000000001d)
			return true;
		
		return false;	
	}
}
