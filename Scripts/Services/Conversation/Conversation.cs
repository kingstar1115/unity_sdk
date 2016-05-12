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

using System;
using System.Text;
using System.Collections.Generic;
using FullSerializer;
using MiniJSON;
using IBM.Watson.DeveloperCloud.Utilities;
using IBM.Watson.DeveloperCloud.Connection;
using IBM.Watson.DeveloperCloud.Logging;

namespace IBM.Watson.DeveloperCloud.Services.Conversation.v1
{
	/// <summary>
	/// This class wraps the Watson Conversation service. 
	/// <a href="http://www.ibm.com/smarterplanet/us/en/ibmwatson/developercloud/conversation.html">Conversation Service</a>
	/// </summary>
	public class Conversation : IWatsonService {
		#region Public Types
		/// <summary>
		/// The callback for Message().
		/// </summary>
		/// <param name="success"></param>
		public delegate void OnMessageCallback(bool success);
		/// <summary>
		/// The callback delegate for the Converse() function.
		/// </summary>
		/// <param name="resp">The response object to a call to Converse().</param>
		public delegate void OnMessage(DataModels.MessageResponse resp);
		#endregion

		#region Public Properties
		#endregion

		#region Private Data
		private const string SERVICE_ID = "ConversationV1";
		private static fsSerializer sm_Serializer = new fsSerializer();
		#endregion

		#region Message
		/// <summary>
		/// Message the specified workspaceId, input and callback.
		/// </summary>
		/// <param name="workspaceId">Workspace identifier.</param>
		/// <param name="input">Input.</param>
		/// <param name="callback">Callback.</param>
		public bool Message(string workspaceId, string input, OnMessage callback)
		{
			if(string.IsNullOrEmpty(workspaceId))
				throw new ArgumentNullException("workspaceId");
			if(string.IsNullOrEmpty(input))
				throw new ArgumentNullException("input");
			if(callback == null)
				throw new ArgumentNullException("callback");

			RESTConnector connector = RESTConnector.GetConnector(SERVICE_ID, "v2/rest/workspaces");
			if(connector == null)
				return false;

			string reqJson = "{\"input\": {\"text\": \"{0}\"}}";

			MessageReq req = new MessageReq();
			req.Callback = callback;
			req.Headers["Content-Type"] = "application/json";
			req.Function = "/" + workspaceId + "/message";
			req.OnResponse = MessageResp;
			req.Forms = new Dictionary<string, RESTConnector.Form>();
			req.Forms["input"] = new RESTConnector.Form(input);

			return connector.Send(req);
		}

		private class MessageReq : RESTConnector.Request
		{
			public OnMessage Callback { get; set; }
		}

		private void MessageResp(RESTConnector.Request req, RESTConnector.Response resp)
		{
			DataModels.MessageResponse response = new DataModels.MessageResponse();
			if (resp.Success)
			{
				try
				{
					fsData data = null;
					fsResult r = fsJsonParser.Parse(Encoding.UTF8.GetString(resp.Data), out data);
					if (!r.Succeeded)
						throw new WatsonException(r.FormattedMessages);

					object obj = response;
					r = sm_Serializer.TryDeserialize(data, obj.GetType(), ref obj);
					if (!r.Succeeded)
						throw new WatsonException(r.FormattedMessages);
				}
				catch (Exception e)
				{
					Log.Error("Conversation", "MessageResp Exception: {0}", e.ToString());
					resp.Success = false;
				}
			}

			if (((MessageReq)req).Callback != null)
				((MessageReq)req).Callback(resp.Success ? response : null);
		}
		#endregion

		#region IWatsonService implementation

		public string GetServiceID()
		{
			return SERVICE_ID;
		}

		public void GetServiceStatus(ServiceStatus callback)
		{
			if (callback != null && callback.Target != null)
			{
				callback(SERVICE_ID, false);
			}
		}

		#endregion


	}
}
