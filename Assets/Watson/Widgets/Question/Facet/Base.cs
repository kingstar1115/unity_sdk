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
using IBM.Watson.Widgets;
using IBM.Watson.Widgets.Avatar;
using IBM.Watson.Services.v1;
using IBM.Watson.Data;
using IBM.Watson.Logging;

namespace IBM.Watson.Widgets.Question.Facet
{
	public class Base: MonoBehaviour
	{
		//	TODO 
		/// <summary>
		/// Holds a reference to the Avatar from the Question Widget.
		/// </summary>
		/// <value>The Avatar.</value>
		protected AvatarWidget m_Avatar { get; set; }

		/// <summary>
		/// Holds a reference to the Question Widget.
		/// </summary>
		/// <value>The m_ question.</value>
		private QuestionWidget m_Question { get; set; }

		/// <summary>
		/// Holds a reference to the Questions from the Question Widget.
		/// </summary>
		private Questions _Questions;
		public Questions Questions
		{
			get { return _Questions; }
			set
			{
				_Questions = value;
//				OnQuestionData ();
			}
		}

		/// <summary>
		/// Holds a reference to to the Answers from the Question Widget.
		/// </summary>
		private Answers _Answers;
		public Answers Answers
		{
			get { return _Answers; }
			set
			{
				_Answers = value;
//				OnAnswerData ();
			}
		}

		/// <summary>
		/// Holds a reference to the Parse Data from the Question Widget.
		/// </summary>
		private ParseData _ParseData;
		public ParseData ParseData
		{
			get { return _ParseData; }
			set
			{
				_ParseData = value;
				OnParseData ();
			}
		}

//		public virtual void Init() {}
		protected virtual void Show() {}
		protected virtual void Hide() {}

		void Start()
		{
			m_Question = GetComponentInParent<QuestionWidget>();
			if (m_Question != null)
				m_Avatar = m_Question.Avatar;
			else
				m_Avatar = GetComponentInParent<AvatarWidget>();
			
			if (m_Avatar != null)
			{
				m_Avatar.QuestionEvent += OnQuestion;
				m_Avatar.AnswerEvent += OnAnswer;
			}
			else
				Log.Warning("Facet Base", "Unable to find AvatarWidget.");
		}

		/// <summary>
		/// Clears dynamically generated Facet Elements when a question is answered. Called from answer event handler.
		/// </summary>
		protected virtual void Clear() {}

		/// <summary>
		/// Fired when Parse Data is set.
		/// </summary>
		protected virtual void OnParseData() {}

		/// <summary>
		/// Callback for Avatar Question.
		/// </summary>
		protected virtual void OnQuestion(string data) {
			Questions = m_Question.Questions;
		}

		/// <summary>
		/// Callback for Avatar Answer
		/// </summary>
		protected virtual void OnAnswer(string data) {
			Clear ();
			Answers = m_Question.Answers;
		}

		/// <summary>
		/// Remove event listeners OnDestroy
		/// </summary>
		protected virtual void OnDestroy()
		{
			if (m_Avatar != null)
			{
				m_Avatar.QuestionEvent -= OnQuestion;
				m_Avatar.AnswerEvent -= OnAnswer;
			}
		}

//		protected virtual void OnQuestionData() {}
//		protected virtual void OnAnswerData() {}
	}
}
