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
using IBM.Watson.DeveloperCloud.Services.DocumentConversion.v1;
using System.Collections;
using IBM.Watson.DeveloperCloud.Logging;

public class ExampleDocumentConversion : MonoBehaviour
{
    private DocumentConversion m_DocumentConversion = new DocumentConversion();
	
	void Start ()
    {
        string examplePath = Application.dataPath + "/Watson/Examples/ServiceExamples/TestData/watson_beats_jeopardy.html";

        if (!m_DocumentConversion.ConvertDocument(OnConvertDocument, examplePath))
            Log.Debug("ExampleDocumentConversion", "Document conversion failed!");
    }

    private void OnConvertDocument(ConvertedDocument documentConversionResponse, string data)
    {
        if (documentConversionResponse != null)
        {
            Log.Debug("ExampleDocumentConversion", "mediaTypeDetected: {0}", documentConversionResponse.media_type_detected);
        }
    }
}
