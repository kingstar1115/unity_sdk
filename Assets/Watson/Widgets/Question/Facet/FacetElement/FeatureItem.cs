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

namespace IBM.Watson.Widgets.Question.Facet.FacetElement
{
	public class FeatureItem : MonoBehaviour
	{
		[SerializeField]
		private Text m_FeatureText;
		[SerializeField]
		private Text m_FeatureIndexText;

		private string _FeatureString;
		public string FeatureString
		{
			get { return _FeatureString; }
			set
			{
				_FeatureString = value;
				UpdateFeature();
			}
		}

		private double _FeatureIndex;
		public double FeatureIndex
		{
			get { return _FeatureIndex; }
			set
			{
				_FeatureIndex = value;
				UpdateFeatureIndex();
			}
		}

		/// <summary>
		/// Updates the Features. Displays only the first 15 characters.
		/// </summary>
		private void UpdateFeature()
		{
			if (FeatureString != "") {
				gameObject.SetActive (true);
				if(FeatureString.Length > 15) {
					string temp = FeatureString.Substring (0, 15);
					m_FeatureText.text = temp + "...";
				} else {
					m_FeatureText.text = FeatureString;
				}
			} else {
				gameObject.SetActive(false);
			}
		}

		/// <summary>
		/// Updates the Feature Index. 
		/// </summary>
		private void UpdateFeatureIndex()
		{
			float featureIndex = (float)FeatureIndex;
			m_FeatureIndexText.text = featureIndex.ToString ("f2");
	}
	}
}
