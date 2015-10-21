﻿using UnityEngine;
using System.Collections;

public class AnswersAndConfidence : QuestionComponentBase {
	[Header("UI Faces")]
	[SerializeField]
	private AnswerConfidenceBar[] m_AnswerConfidenceBars;

	void Start ()
	{
		base.Start ();
		for(int i = 0; i < m_AnswerConfidenceBars.Length; i++) {
			m_AnswerConfidenceBars[i].m_Answer = qWidget.Answers.answers[i].answerText;
			m_AnswerConfidenceBars[i].m_Confidence = qWidget.Answers.answers[i].confidence;
		}
	}
}
