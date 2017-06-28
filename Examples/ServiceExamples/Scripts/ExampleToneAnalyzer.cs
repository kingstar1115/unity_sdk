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
using IBM.Watson.DeveloperCloud.Services.ToneAnalyzer.v3;
using IBM.Watson.DeveloperCloud.Utilities;

public class ExampleToneAnalyzer : MonoBehaviour
{
    private string _username = "";
    private string _password = "";
    private string _url = "";

    string m_StringToTestTone = "This service enables people to discover and understand, and revise the impact of tone in their content. It uses linguistic analysis to detect and interpret emotional, social, and language cues found in text.";

    void Start()
    {
        Credentials _credentials = new Credentials(_username, _password, _url);
        ToneAnalyzer m_ToneAnalyzer = new ToneAnalyzer(_credentials);
        m_ToneAnalyzer.GetToneAnalyze(OnGetToneAnalyze, m_StringToTestTone);
    }

    private void OnGetToneAnalyze(ToneAnalyzerResponse resp, string data)
    {
        Debug.Log("Response: " + resp + " - " + data);
    }
}
