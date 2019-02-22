﻿/**
* Copyright 2019 IBM Corp. All Rights Reserved.
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

using IBM.Cloud.SDK;
using IBM.Watson.Assistant.V2;
using IBM.Watson.Assistant.V2.Model;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.TestTools;

namespace IBM.Watson.Tests
{
    public class AssistantV2IntegrationTests
    {
        private AssistantService service;
        private string versionDate = "2019-02-13";
        private string assistantId;
        private string sessionId;

        [SetUp]
        public void TestSetup()
        {
            LogSystem.InstallDefaultReactors();
        }

        [UnityTest]
        public IEnumerator TestMessage()
        {
            service = new AssistantService(versionDate);

            while (!service.Credentials.HasIamTokenData())
                yield return null;

            assistantId = Environment.GetEnvironmentVariable("CONVERSATION_ASSISTANT_ID");
            string sessionId = null;

            SessionResponse createSessionResponse = null;
            Log.Debug("AssistantV2IntegrationTests", "Attempting to CreateSession...");
            service.CreateSession(
                callback: (WatsonResponse<SessionResponse> response, WatsonError error, Dictionary<string, object> customData) =>
                {
                    Log.Debug("AssistantV1IntegrationTests", "result: {0}", customData["json"].ToString());
                    createSessionResponse = response.Result;
                    sessionId = createSessionResponse.SessionId;
                    Assert.IsNotNull(createSessionResponse);
                    Assert.IsNotNull(response.Result.SessionId);
                    Assert.IsNull(error);
                },
                assistantId: assistantId
            );

            while (createSessionResponse == null)
                yield return null;

            MessageResponse messageResponse = null;
            Log.Debug("AssistantV2IntegrationTests", "Attempting to Message...");
            service.Message(
                callback: (WatsonResponse<MessageResponse> response, WatsonError error, Dictionary<string, object> customData) =>
                {
                    Log.Debug("AssistantV1IntegrationTests", "result: {0}", customData["json"].ToString());
                    messageResponse = response.Result;
                    Assert.IsNotNull(messageResponse);
                    Assert.IsNull(error);
                },
                assistantId: assistantId,
                sessionId: sessionId
            );

            while (messageResponse == null)
                yield return null;

            messageResponse = null;
            MessageRequest messageRequest = new MessageRequest()
            {
                Input = new MessageInput()
                {
                    Text = "Are you open on Christmas?",
                    Options = new MessageInputOptions()
                    {
                        ReturnContext = true
                    }
                }
            };
            Log.Debug("AssistantV2IntegrationTests", "Attempting to Message...Are you open on Christmas?");
            service.Message(
                callback: (WatsonResponse<MessageResponse> response, WatsonError error, Dictionary<string, object> customData) =>
                {
                    Log.Debug("AssistantV1IntegrationTests", "result: {0}", customData["json"].ToString());
                    messageResponse = response.Result;
                    Assert.IsNotNull(messageResponse);
                    Assert.IsNull(error);
                },
                assistantId: assistantId,
                sessionId: sessionId,
                request: messageRequest
            );

            while (messageResponse == null)
                yield return null;

            messageResponse = null;
            messageRequest = new MessageRequest()
            {
                Input = new MessageInput()
                {
                    Text = "What are your hours?",
                    Options = new MessageInputOptions()
                    {
                        ReturnContext = true
                    }
                }
            };
            Log.Debug("AssistantV2IntegrationTests", "Attempting to Message...What are your hours?");
            service.Message(
                callback: (WatsonResponse<MessageResponse> response, WatsonError error, Dictionary<string, object> customData) =>
                {
                    Log.Debug("AssistantV1IntegrationTests", "result: {0}", customData["json"].ToString());
                    messageResponse = response.Result;
                    Assert.IsNotNull(messageResponse);
                    Assert.IsNull(error);
                },
                assistantId: assistantId,
                sessionId: sessionId,
                request: messageRequest
            );

            while (messageResponse == null)
                yield return null;

            messageResponse = null;
            messageRequest = new MessageRequest()
            {
                Input = new MessageInput()
                {
                    Text = "I'd like to make an appointment for 12pm.",
                    Options = new MessageInputOptions()
                    {
                        ReturnContext = true
                    }
                }
            };
            Log.Debug("AssistantV2IntegrationTests", "Attempting to Message...I'd like to make an appointment for 12pm.");
            service.Message(
                callback: (WatsonResponse<MessageResponse> response, WatsonError error, Dictionary<string, object> customData) =>
                {
                    Log.Debug("AssistantV1IntegrationTests", "result: {0}", customData["json"].ToString());
                    messageResponse = response.Result;
                    Assert.IsNotNull(messageResponse);
                    Assert.IsNull(error);
                },
                assistantId: assistantId,
                sessionId: sessionId,
                request: messageRequest
            );

            while (messageResponse == null)
                yield return null;

            messageResponse = null;
            messageRequest = new MessageRequest()
            {
                Input = new MessageInput()
                {
                    Text = "On Friday please.",
                    Options = new MessageInputOptions()
                    {
                        ReturnContext = true
                    }
                }
            };
            Log.Debug("AssistantV2IntegrationTests", "Attempting to Message...On Friday please.");
            service.Message(
                callback: (WatsonResponse<MessageResponse> response, WatsonError error, Dictionary<string, object> customData) =>
                {
                    Log.Debug("AssistantV1IntegrationTests", "result: {0}", customData["json"].ToString());
                    messageResponse = response.Result;
                    Assert.IsNotNull(messageResponse);
                    Assert.IsNull(error);
                },
                assistantId: assistantId,
                sessionId: sessionId,
                request: messageRequest
            );

            while (messageResponse == null)
                yield return null;

            messageResponse = null;
            messageRequest = new MessageRequest()
            {
                Input = new MessageInput()
                {
                    Text = "Yes.",
                    Options = new MessageInputOptions()
                    {
                        ReturnContext = true
                    }
                }
            };
            Log.Debug("AssistantV2IntegrationTests", "Attempting to Message...Yes.");
            service.Message(
                callback: (WatsonResponse<MessageResponse> response, WatsonError error, Dictionary<string, object> customData) =>
                {
                    Log.Debug("AssistantV1IntegrationTests", "result: {0}", customData["json"].ToString());
                    messageResponse = response.Result;
                    Assert.IsNotNull(messageResponse);
                    Assert.IsNull(error);
                },
                assistantId: assistantId,
                sessionId: sessionId,
                request: messageRequest
            );

            while (messageResponse == null)
                yield return null;
            
            object deleteSessionResponse = null;
            Log.Debug("AssistantV2IntegrationTests", "Attempting to DeleteSession...");
            service.DeleteSession(
                callback: (WatsonResponse<object> response, WatsonError error, Dictionary<string, object> customData) =>
                {
                    Log.Debug("AssistantV1IntegrationTests", "result: {0}", customData["json"].ToString());
                    deleteSessionResponse = response.Result;
                    Assert.IsNotNull(response.Result);
                    Assert.IsNull(error);
                },
                assistantId: assistantId,
                sessionId: sessionId
            );

            while (deleteSessionResponse == null)
                yield return null;
        }

        [TearDown]
        public void TestTearDown() { }
    }
}
