using UnityEngine;
using System.Collections;
using System;

public class FzFairly : FuzzyTerm {

	private FuzzySet m_Set;

	private FzFairly(FzFairly fzFairly) {
		m_Set = fzFairly.m_Set;
	}

	public FzFairly (FzSet fzSet) {
		m_Set = fzSet.m_Set.clone();
	}

	public override double GetDOM() {
		return Mathf.Sqrt ((float)m_Set.GetDOM ());
	}

	public override FuzzyTerm Clone() {
		return new FzFairly (this);
	}

	public override void ClearDOM() {
		m_Set.ClearDOM ();
	}

	public override void ORwithDOM(double value) {
		m_Set.ORwithDOM(Mathf.Sqrt ((float)value));
	}
}
