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
*/

using UnityEngine;
using UnityEngine.UI;
using IBM.Watson.DataModels;
using IBM.Watson.Logging;
using IBM.Watson.Services.v1;
using IBM.Watson.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;

namespace IBM.Watson.Widgets
{

    /// <summary>
    /// Translation widget to handle translation service calls
    /// </summary>
	public class TranslationWidget : Widget
    {
		#region Private Data
		private Translate m_Translate = new Translate();

		[SerializeField]
		private InputField m_Input = null;
		[SerializeField]
		private InputField m_Output = null;
		[SerializeField]
		private Dropdown m_DropDownSourceLanguage = null;
		[SerializeField]
		private Dropdown m_DropDownTargetLanguage = null;



		private Dictionary<string, string> m_LanguageToIdentify = new Dictionary<string, string> ();
		private Dictionary<string, List<string>> m_LanguageToTranslate = new Dictionary<string, List<string>> ();
		private string[] m_LanguagesFrom = null;
		//private string[] m_LanguagesTo = null;

		private string m_FromDetectedLanguage = null;
		private string m_DetectLanguage = "DETECT";
		private string m_DetectLanguageName = "Detect Language";
		private string m_DetectedLanguageNameFormat = "Detected: {0}";
		private string m_DefaultLanguageFromTranslate = "DETECT";
		private string m_DefaultLanguageToTranslate = "en";
		private string m_DefaultDomainToUse = "conversational";
		#endregion

        #region Widget interface
        protected override string GetName()
        {
			return "Translation";
        }
        #endregion

		#region Public Members

		public string FromLanguage{
			get{
				string valueToReturn = null;
				if(m_DropDownSourceLanguage != null && !string.IsNullOrEmpty(m_FromDetectedLanguage) && m_DropDownSourceLanguage.value == 0){
					valueToReturn = m_FromDetectedLanguage;
				}
				else if(m_DropDownSourceLanguage != null && m_LanguageToTranslate.Keys != null && m_LanguageToTranslate.Keys.Count > m_DropDownSourceLanguage.value && m_DropDownSourceLanguage.value >= 0){
					valueToReturn = m_LanguagesFrom[m_DropDownSourceLanguage.value];
				}

				return valueToReturn;
			}
		}

		public string ToLanguage{
			get{
				string valueToReturn = null;
				if(m_DropDownTargetLanguage != null && m_LanguageToTranslate != null && FromLanguage != null && m_LanguageToTranslate.ContainsKey(FromLanguage) && m_LanguageToTranslate[FromLanguage].Count > m_DropDownTargetLanguage.value && m_DropDownTargetLanguage.value >= 0){
					valueToReturn = m_LanguageToTranslate[FromLanguage][m_DropDownTargetLanguage.value];
				}
				return valueToReturn;
			}
		}

		public bool IsFromLanguageToDetect{
			get{
				return string.Equals(FromLanguage, m_DetectLanguage);
			}
		}

		public bool IsSameLanguage{	//No need to call translate
			get{
				return String.Equals(FromLanguage, ToLanguage);
			}
		}

		#endregion

		private void OnEnable()
		{
			m_Translate.GetLanguages( OnGetLanguagesAndGetModelsAfter );
			//m_Translate.GetTranslation( "What does the fox say?", "en", "es", OnGetTranslation );
			//m_Translate.GetTranslation( "What does the fox say?", "en", "fr", OnGetTranslation );

			//m_Translate.GetModel( "en-es", OnGetModel );

			


		}
		
		private void OnDisable()
		{

		}

		/// <summary>
		/// Button event handler.
		/// </summary>
		public void OnTranslation()
		{
			if (m_Translate != null) {
				if(FromLanguage != null && ToLanguage != null){
					if (m_Input != null) {
						Translate(m_Input.text);

						if(IsFromLanguageToDetect && !string.IsNullOrEmpty(m_Input.text)){	//Identify and translate
							m_Translate.Identify(m_Input.text, OnIdentifyAndTranslateFromLanguage );
						}

					}
					else{
						Log.Error("TranslationWidget", "OnTranslation - Input is null");
					}
				}
				else{
					Log.Error("TranslationWidget", "OnTranslation - From language and To Language should be set!");
				}

			} else {
				Log.Error("TranslationWidget", "OnTranslation - Translate object is null");
			}
		}

		private void Translate(string text){
			if (!string.IsNullOrEmpty (text)) {
				if (IsSameLanguage || IsFromLanguageToDetect) {	//same language or it is still not detected!
					SetOutput(text);
				} else {
					m_Translate.GetTranslation (m_Input.text, FromLanguage, ToLanguage, OnGetTranslation);
				}
			} else {
				//translation text is empty - clear output text
				SetOutput("");
			}


		}

		private void SetOutput(string text){
			if (m_Output != null) {
				m_Output.text = text;
			}
		}

		private void OnGetTranslation( Translations translation )
		{
			if ( translation != null && translation.translations.Length > 0 ){
				//Log.Status( "TranslationWidget", "OnGetTranslation - Translation: {0}", translation.translations[0].translation );
				SetOutput( translation.translations[0].translation );
			}
		}

		private void OnGetLanguagesAndGetModelsAfter( Languages languages )
		{
			if (languages != null && languages.languages.Length > 0) {
				m_LanguageToIdentify.Clear();

				foreach (var lang in languages.languages) {
					m_LanguageToIdentify[lang.language] = lang.name;
				}

				m_LanguageToIdentify[m_DetectLanguage] = m_DetectLanguageName;

				m_Translate.GetModels( OnGetModels );	//To fill dropdown with models to use in Translation

			} else {
				Log.Error("TranslationWidget", "OnGetLanguages - There is no language to translate. Check the connections and service of Translation Service.");
			}

		}

		private void OnIdentifyAndTranslateFromLanguage(string lang){
			//Log.Status( "TranslationWidget", "OnIdentifyAndTranslateFromLanguage as {0}", lang );
			for (int i = 0; m_LanguagesFrom != null && i < m_LanguagesFrom.Length; i++) {
				if(String.Equals(m_LanguagesFrom[i], lang)){
					m_FromDetectedLanguage = lang;
					m_DropDownSourceLanguage.captionText.text = string.Format(m_DetectedLanguageNameFormat, m_LanguageToIdentify[lang]);
					m_DropDownSourceLanguage.options[0].text = string.Format(m_DetectedLanguageNameFormat, m_LanguageToIdentify[lang]);
					ResetTargetLanguageDropDown();
					Translate(m_Input.text);
					break;
				}
			}
		}

		private IEnumerator EnableInteractiveDropDown(){
			yield return  null;
			m_DropDownSourceLanguage.interactable = true;
		}

		private void OnGetModels( TranslationModels models )
		{
			if ( models != null && models.models.Length > 0)
			{
				m_LanguageToTranslate.Clear();
				List<string> listLanguages = new List<string> ();	//From - To language list to use in translation

				m_DropDownSourceLanguage.options.Clear ();
				m_DropDownTargetLanguage.options.Clear ();
				int defaultInitialValueFromLanguage = 0;
				int defaultInitialValueToLanguage = 0;

				//Adding initial language as detected!
				listLanguages.Add (m_DetectLanguage);
				m_LanguageToTranslate.Add(m_DetectLanguage, new List<string>());

				foreach( var model in models.models )
				{
					if(string.Equals( model.domain, m_DefaultDomainToUse)){

						if(m_LanguageToTranslate.ContainsKey(model.source)){
							if(!m_LanguageToTranslate[model.source].Contains(model.target))
								m_LanguageToTranslate[model.source].Add (model.target);
						}
						else{
							m_LanguageToTranslate.Add(model.source, new List<string>());
							m_LanguageToTranslate[model.source].Add (model.target);
						}

						if(!listLanguages.Contains(model.source)){
							listLanguages.Add(model.source);
						}
						if(!listLanguages.Contains(model.target)){
							listLanguages.Add(model.target);
						}

						if(!m_LanguageToTranslate[m_DetectLanguage].Contains(model.target))
							m_LanguageToTranslate[m_DetectLanguage].Add (model.target);
					}

					//Log.Status( "TestTranslate", "ModelID: {0}, Source: {1}, Target: {2}, Domain: {3}", model.model_id, model.source, model.target, model.domain );
				}

				//Adding all languages to SourceLanguage dropdown
				foreach (string itemLanguage in listLanguages) {
					if(m_LanguageToIdentify.ContainsKey(itemLanguage)){
						m_DropDownSourceLanguage.options.Add (new Dropdown.OptionData (m_LanguageToIdentify[itemLanguage]));
					}

					if(String.Equals(m_DefaultLanguageFromTranslate, itemLanguage)){
						defaultInitialValueFromLanguage = m_DropDownSourceLanguage.options.Count - 1; 
					}

				}
				m_LanguagesFrom = listLanguages.ToArray ();
				
				m_DropDownSourceLanguage.value = defaultInitialValueFromLanguage;

			}
		}

		public void DropDownSourceValueChanged(){

			if(m_DropDownSourceLanguage.value != 0){
				m_DropDownSourceLanguage.options[0].text = m_LanguageToIdentify[m_DetectLanguage];
				m_FromDetectedLanguage = null;
			}

			ResetTargetLanguageDropDown ();

			OnTranslation ();
		}

		public void DropDownTargetValueChanged(){
			OnTranslation ();
		}

		public void ResetTargetLanguageDropDown(){
			if (FromLanguage != null) {

				//Add all languages, because first item is detect language
				if(m_LanguageToTranslate != null && string.Equals(m_DetectLanguage, FromLanguage)){
					string languageToPrevious = ToLanguage;
					m_DropDownTargetLanguage.options.Clear ();
					m_DropDownTargetLanguage.value = -1;
					int defaultInitialValueToLanguage = 0;

					foreach (string itemLanguage in m_LanguageToTranslate.Keys) {
						if(string.Equals(itemLanguage, m_DetectLanguage))
						   continue;

						m_DropDownTargetLanguage.options.Add (new Dropdown.OptionData (m_LanguageToIdentify[itemLanguage]));
						
						if(String.Equals(m_DefaultLanguageToTranslate, itemLanguage)){
							defaultInitialValueToLanguage = m_DropDownTargetLanguage.options.Count - 1; //first item 'Detect Language' will be removed
						}
					}

					m_DropDownTargetLanguage.captionText.text = m_DropDownTargetLanguage.options[0].text;
					m_DropDownTargetLanguage.value = defaultInitialValueToLanguage;

				}
				//Add target language corresponding source language
				else if(m_LanguageToTranslate != null && m_LanguageToTranslate.ContainsKey(FromLanguage) && m_LanguageToTranslate[FromLanguage] != null){
					string languageToPrevious = ToLanguage;
					m_DropDownTargetLanguage.options.Clear ();
					m_DropDownTargetLanguage.value = -1;
					int defaultInitialValueToLanguage = 0;

					foreach (string itemLanguage in m_LanguageToTranslate[FromLanguage]) {
						if(string.Equals(itemLanguage, m_DetectLanguage))
							continue;

						m_DropDownTargetLanguage.options.Add (new Dropdown.OptionData (m_LanguageToIdentify[itemLanguage]));

						if(String.Equals(m_DefaultLanguageToTranslate, itemLanguage)){
							defaultInitialValueToLanguage = m_DropDownTargetLanguage.options.Count - 1; //first item 'Detect Language' will be removed
						}
					}

					m_DropDownTargetLanguage.captionText.text = m_DropDownTargetLanguage.options[0].text;
					m_DropDownTargetLanguage.value = defaultInitialValueToLanguage;

				}
				else{
					Log.Error("TranslationWidget", "ResetTargetLanguageDropDown - There is invalid condition. ");
				}

			} else {
				Log.Error("TranslationWidget", "ResetTargetLanguageDropDown - Source language has not been set.");
			}
		}


    }



}