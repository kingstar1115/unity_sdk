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
using System.Collections.Generic;
using IBM.Watson.Utilities;
using IBM.Watson.Widgets.Question.Facet.FacetElement;

namespace IBM.Watson.Widgets.Question.Facet
{
	public class Evidence : FacetBase
	{
		[Header("Evidence")]
		[SerializeField]
		private EvidenceItem[] m_EvidenceItems;

		/// <summary>
		/// Initialize with data from Question Widget.
		/// </summary>
		public override void Init()
		{
			base.Init ();

			//	TODO replace <answer> with the outline
			if (m_QuestionWidget.Answers.answers.Length == 0)
				return;

			if (m_QuestionWidget.Answers.answers [0].evidence.Length == 1) {
				m_EvidenceItems [0].m_Evidence = m_QuestionWidget.Answers.answers [0].evidence [0].decoratedPassage;
				m_EvidenceItems [1].gameObject.SetActive (false);
			} else if (m_QuestionWidget.Answers.answers [0].evidence.Length > 1) {
				m_EvidenceItems [1].gameObject.SetActive (true);
				m_EvidenceItems [0].m_Evidence = m_QuestionWidget.Answers.answers [0].evidence [0].decoratedPassage;
				m_EvidenceItems [1].m_Evidence = m_QuestionWidget.Answers.answers [0].evidence [1].decoratedPassage;
			} else {
				m_EvidenceItems [0].gameObject.SetActive (false);
				m_EvidenceItems [1].gameObject.SetActive (false);
			}
		}
	}
}
