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
	public class Location : Base
	{
		[SerializeField]
		private Text m_LocationText;

		private string _m_Location;
		public string m_Location
		{
			get { return _m_Location; }
			set 
			{
				_m_Location = value;
				UpdateLocation();
			}
		}

		/// <summary>
		/// Update the Location view.
		/// </summary>
		private void UpdateLocation()
		{
			m_LocationText.text = m_Location;
		}

		/// <summary>
		/// Fired when Parse Data is set. Sets the value of Location from the Avatar Widget.
		/// </summary>
		override protected void OnParseData()
		{
			m_Location = m_Avatar.ITM.Location;
		}
	}
}