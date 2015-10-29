﻿/**
* Copyright 2015 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
* @author Taj Santiago
*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace IBM.Watson.Widgets.Question.Facet
{
	public class QuestionAndAnswer : Base
		{
		[SerializeField]
		private Text m_QuestionText;
		[SerializeField]
		private Text m_AnswerText;
		[SerializeField]
		private Text m_ConfidenceText;


		private string _AnswerString;
		public string AnswerString
		{
			get { return _AnswerString; }
			set 
			{
				_AnswerString = value;
				UpdateAnswer();
			}
		}

		private string _QuestionString;
		public string QuestionString
		{
			get { return _QuestionString; }
			set 
			{
				_QuestionString = value;
				UpdateQuestion();
			}
		}

		private double _m_Confidence;
		public double m_Confidence 
		{
			get { return _m_Confidence; }
			set {
				_m_Confidence = value;
				UpdateConfidence();
			}
		}

		override public void Init()
		{
			QuestionString = m_Question.QuestionData.QuestionDataObject.questions [0].question.questionText;
			AnswerString = m_Question.QuestionData.AnswerDataObject.answers [0].answerText;
			m_Confidence = m_Question.QuestionData.AnswerDataObject.answers [0].confidence;
		}

		/// <summary>
		/// Update answer view.
		/// </summary>
		private void UpdateAnswer()
		{
			m_AnswerText.text = AnswerString;
		}

		/// <summary>
		/// Update question view.
		/// </summary>
		private void UpdateQuestion()
		{
			m_QuestionText.text = QuestionString;
		}

		/// <summary>
		/// Update confidence view.
		/// </summary>
		private void UpdateConfidence()
		{
			float confidence = (float)m_Confidence * 100;
			m_ConfidenceText.text = confidence.ToString ("f1");
		}
	}
}
