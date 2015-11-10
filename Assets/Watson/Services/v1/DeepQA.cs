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
* @author Richard Lyle (rolyle@us.ibm.com)
*/


using FullSerializer;
using IBM.Watson.Connection;
using IBM.Watson.Data;
using IBM.Watson.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IBM.Watson.Services.v1
{
    /// <summary>
    /// The DeepQA provides an abstraction for the deepQa/ services of Watson.
    /// </summary>
    public class DeepQA
    {
        #region Public Types
        /// <exclude />
        public delegate void OnAskQuestion( Data.QA.Question response );
        #endregion

        #region Private Data
        private const string SERVICE_ID = "DeepQuestionAnswerV1";
        private static fsSerializer sm_Serializer = new fsSerializer();
        #endregion

        #region AskQuestion
        /// <summary>
        /// Ask a question using the given pipeline.
        /// </summary>
        /// <param name="id">The ID of the service to ask the question.</param>
        /// <param name="question">The text of the question.</param>
        /// <param name="callback">The callback to receive the response.</param>
        /// <returns>Returns true if the request was submitted.</returns>
        public bool AskQuestion( string id, string question, OnAskQuestion callback )
        {
            if ( string.IsNullOrEmpty( id ) )
                throw new ArgumentNullException("id");
            if ( string.IsNullOrEmpty( question ) )
                throw new ArgumentNullException("question");
            if ( callback == null )
                throw new ArgumentNullException("callback");

            RESTConnector connector = RESTConnector.GetConnector(SERVICE_ID, "/v1/question");
            if (connector == null)
                return false;

            Dictionary<string,object> questionJson = new Dictionary<string, object>();
            questionJson["question"] = new Dictionary<string,object>() { { "questionText", question } };
            string json = MiniJSON.Json.Serialize( questionJson );

            AskQuestionReq req = new AskQuestionReq();
            req.Function = "/" + id;
            req.Headers["Content-Type"] = "application/json";
            req.Send = Encoding.UTF8.GetBytes( json );
            req.Callback = callback;
            req.OnResponse = OnAskQuestionResp;

            return connector.Send(req);
        }
        private class AskQuestionReq : RESTConnector.Request
        {
            public OnAskQuestion Callback { get; set; }
        };
        private void OnAskQuestionResp(RESTConnector.Request req, RESTConnector.Response resp)
        {
            //Data.QA.ResponseList response = new Data.QA.ResponseList();
            //if (resp.Success)
            //{
            //    try
            //    {
            //        string json = Encoding.UTF8.GetString(resp.Data);
            //        // FIXUP the JSON data so we can use the FullSerializer
            //        json = "{ \"responses\": " + json + " }";

            //        fsData data = null;
            //        fsResult r = fsJsonParser.Parse(json, out data);
            //        if (!r.Succeeded)
            //            throw new WatsonException(r.FormattedMessages);

            //        object obj = response;
            //        r = sm_Serializer.TryDeserialize(data, obj.GetType(), ref obj);
            //        if (!r.Succeeded)
            //            throw new WatsonException(r.FormattedMessages);
            //    }
            //    catch (Exception e)
            //    {
            //        Log.Error("NLC", "GetClassifiers Exception: {0}", e.ToString());
            //        resp.Success = false;
            //    }
            //}

            //if (((AskQuestionReq)req).Callback != null)
            //{
            //    if ( resp.Success && response.responses != null && response.responses.Length > 0 )
            //        ((AskQuestionReq)req).Callback( response.responses[0] );
            //    else
            //        ((AskQuestionReq)req).Callback( null );
            //}
        }
        #endregion
    }
}
