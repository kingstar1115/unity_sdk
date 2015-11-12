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
using UnityEngine.UI;
using IBM.Watson.Logging;
using IBM.Watson.Utilities;
using IBM.Watson.Data;

namespace IBM.Watson.Widgets.Question
{
	/// <summary>
	/// Handles all QuestionAndAndswer Facet functionality.
	/// </summary>
    public class QuestionAndAnswer : Base
    {
        [SerializeField]
        private Text m_QuestionText;
        [SerializeField]
        private Text m_AnswerText;
        [SerializeField]
        private Text m_ConfidenceText;


        private string m_AnswerString;
        public string AnswerString
        {
			get { return m_AnswerString; }
            set
            {
				m_AnswerString = value;
                UpdateAnswer();
            }
        }

        private string m_QuestionString;
        public string QuestionString
        {
            get { return m_QuestionString; }
            set
            {
                m_QuestionString = value;
                UpdateQuestion();
            }
        }

        private double m_Confidence;
        public double Confidence
        {
            get { return m_Confidence; }
            set
            {
                m_Confidence = value;
                UpdateConfidence();
            }
        }

		private Data.XRAY.Answers m_AnswerData = null;
		private Data.XRAY.Questions m_QuestionData = null;
		
		private void OnEnable()
		{
			EventManager.Instance.RegisterEventReceiver( Constants.Event.ON_QUESTION_ANSWERS, OnAnswerData );
			EventManager.Instance.RegisterEventReceiver( Constants.Event.ON_QUESTION, OnQuestionData );
		}
		
		private void OnDisable()
		{
			EventManager.Instance.UnregisterEventReceiver( Constants.Event.ON_QUESTION_ANSWERS, OnAnswerData );
			EventManager.Instance.UnregisterEventReceiver( Constants.Event.ON_QUESTION, OnQuestionData );
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
            float confidence = (float)Confidence * 100;
            m_ConfidenceText.text = confidence.ToString("f1");
        }

		/// <summary>
		/// Concantinates Variants.
		/// </summary>
		private string Variants()
		{
            if ( m_AnswerData == null || !m_AnswerData.HasAnswer() || m_AnswerData.answers[0].variants == null )
                return ".";

			string variantsString = "; ";
			int variantLength = m_AnswerData.answers [0].variants.Length;

			if (variantLength == 0)
				return ".";

			for (int i = 0; i < variantLength; i++)
			{
				string variant = m_AnswerData.answers[0].variants[i].text;
				variantsString += variant;

				if(i < variantLength - 1)
				{
					variantsString += ", ";
				} else {
					variantsString += ".";
				}
			}

			return variantsString;
		}

		/// <summary>
		/// Callback for Answer data event.
		/// </summary>
		/// <param name="args">Arguments.</param>
		private void OnAnswerData( object [] args )
		{
			m_AnswerData = args != null && args.Length > 0 ? args[0] as Data.XRAY.Answers : null;
			InitAnswers ();
		}

		/// <summary>
		/// Callback for Question data event.
		/// </summary>
		/// <param name="args">Arguments.</param>
		private void OnQuestionData( object [] args )
		{
			m_QuestionData = args != null && args.Length > 0 ? args[0] as Data.XRAY.Questions : null;
			InitQuestions ();
		}

		/// <summary>
		/// Initialize SubFacet with Answer data.
		/// </summary>
		private void InitAnswers()
		{
			AnswerString = m_AnswerData.answers[0].answerText + Variants();
			Confidence = m_AnswerData.answers[0].confidence;
		}

		/// <summary>
		/// Initialize SubFacet with Question data.
		/// </summary>
		private void InitQuestions()
		{
			QuestionString = m_QuestionData.questions[0].question.questionText;
		}
    }
}
