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
using System.Collections;
using IBM.Watson.DeveloperCloud.Services.AlchemyAPI.v1;
using IBM.Watson.DeveloperCloud.Logging;
using System;

public class ExampleAlchemyDataNews : MonoBehaviour {
    private AlchemyAPI m_AlchemyAPI = new AlchemyAPI();
    
    void Start () {
        LogSystem.InstallDefaultReactors();

		if (!m_AlchemyAPI.GetNews(OnGetNews))
            Log.Debug("ExampleAlchemyData", "Failed to get news!");
    }

    private void OnGetNews(NewsResponse newsData, string data)
    {
        if(newsData != null)
            Log.Debug("ExampleAlchemyData", "status: {0}", newsData.status);
    }
}
