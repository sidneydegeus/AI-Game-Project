using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FzOR : FuzzyTerm {

	private IList<FuzzyTerm> m_Terms = new List<FuzzyTerm> (4);

	public FzOR(FzOR fzOr) {
		foreach (FuzzyTerm fuzzyTerm in fzOr.m_Terms)
			m_Terms.Add(fuzzyTerm.Clone ());
	}

	public FzOR(FuzzyTerm op1, FuzzyTerm op2) {
		m_Terms.Add(op1.Clone());
		m_Terms.Add(op2.Clone());
	}
	
	public FzOR(FuzzyTerm op1, FuzzyTerm op2, FuzzyTerm op3) {
		m_Terms.Add(op1.Clone());
		m_Terms.Add(op2.Clone());
		m_Terms.Add(op3.Clone());
	}
	
	public FzOR(FuzzyTerm op1, FuzzyTerm op2, FuzzyTerm op3, FuzzyTerm op4) {
		m_Terms.Add(op1.Clone());
		m_Terms.Add(op2.Clone());
		m_Terms.Add(op3.Clone());
		m_Terms.Add(op4.Clone());
	}
	
	public override FuzzyTerm Clone() {
		return new FzOR(this);
	}

	public override double GetDOM() {
		double largest = float.MinValue;

		foreach (FuzzyTerm fuzzyTerm in m_Terms) {
			if (fuzzyTerm.GetDOM() > largest)
				largest = fuzzyTerm.GetDOM();
		}
		return largest;
	}

	public override void ClearDOM() {}

	public override void ORwithDOM(double val) {}
}
