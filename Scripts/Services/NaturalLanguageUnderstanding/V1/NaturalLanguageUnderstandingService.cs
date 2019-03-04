/**
* Copyright 2018, 2019 IBM Corp. All Rights Reserved.
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

using System.Collections.Generic;
using System.Text;
using IBM.Cloud.SDK;
using IBM.Cloud.SDK.Connection;
using IBM.Cloud.SDK.Utilities;
using IBM.Watson.NaturalLanguageUnderstanding.V1.Model;
using Newtonsoft.Json;
using System;
using UnityEngine.Networking;

namespace IBM.Watson.NaturalLanguageUnderstanding.V1
{
    public class NaturalLanguageUnderstandingService : BaseService
    {
        private const string serviceId = "natural-language-understanding";
        private const string defaultUrl = "https://gateway.watsonplatform.net/natural-language-understanding/api";

        #region Credentials
        /// <summary>
        /// Gets and sets the credentials of the service. Replace the default endpoint if endpoint is defined.
        /// </summary>
        public Credentials Credentials
        {
            get { return credentials; }
            set
            {
                credentials = value;
                if (!string.IsNullOrEmpty(credentials.Url))
                {
                    Url = credentials.Url;
                }
            }
        }
        #endregion

        #region Url
        /// <summary>
        /// Gets and sets the endpoint URL for the service.
        /// </summary>
        public string Url
        {
            get { return url; }
            set { url = value; }
        }
        #endregion

        #region VersionDate
        private string versionDate;
        /// <summary>
        /// Gets and sets the versionDate of the service.
        /// </summary>
        public string VersionDate
        {
            get { return versionDate; }
            set { versionDate = value; }
        }
        #endregion

        #region DisableSslVerification
        private bool disableSslVerification = false;
        /// <summary>
        /// Gets and sets the option to disable ssl verification
        /// </summary>
        public bool DisableSslVerification
        {
            get { return disableSslVerification; }
            set { disableSslVerification = value; }
        }
        #endregion

        /// <summary>
        /// NaturalLanguageUnderstandingService constructor.
        /// </summary>
        /// <param name="versionDate">The service version date in `yyyy-mm-dd` format.</param>
        public NaturalLanguageUnderstandingService(string versionDate) : base(versionDate, serviceId)
        {
            VersionDate = versionDate;
        }

        /// <summary>
        /// NaturalLanguageUnderstandingService constructor.
        /// </summary>
        /// <param name="versionDate">The service version date in `yyyy-mm-dd` format.</param>
        /// <param name="credentials">The service credentials.</param>
        public NaturalLanguageUnderstandingService(string versionDate, Credentials credentials) : base(versionDate, credentials, serviceId)
        {
            if (string.IsNullOrEmpty(versionDate))
            {
                throw new ArgumentNullException("A versionDate (format `yyyy-mm-dd`) is required to create an instance of NaturalLanguageUnderstandingService");
            }
            else
            {
                VersionDate = versionDate;
            }

            if (credentials.HasCredentials() || credentials.HasIamTokenData())
            {
                Credentials = credentials;

                if (string.IsNullOrEmpty(credentials.Url))
                {
                    credentials.Url = defaultUrl;
                }
            }
            else
            {
                throw new IBMException("Please provide a username and password or authorization token to use the NaturalLanguageUnderstanding service. For more information, see https://github.com/watson-developer-cloud/unity-sdk/#configuring-your-service-credentials");
            }
        }

        /// <summary>
        /// Analyze text.
        ///
        /// Analyzes text, HTML, or a public webpage for the following features:
        /// - Categories
        /// - Concepts
        /// - Emotion
        /// - Entities
        /// - Keywords
        /// - Metadata
        /// - Relations
        /// - Semantic roles
        /// - Sentiment.
        /// </summary>
        /// <param name="callback">The callback function that is invoked when the operation completes.</param>
        /// <param name="parameters">An object containing request parameters. The `features` object and one of the
        /// `text`, `html`, or `url` attributes are required.</param>
        /// <returns><see cref="AnalysisResults" />AnalysisResults</returns>
        /// <param name="customData">A Dictionary<string, object> of data that will be passed to the callback. The raw
        /// json output from the REST call will be passed in this object as the value of the 'json'
        /// key.</string></param>
        public bool Analyze(Callback<AnalysisResults> callback, Parameters parameters, Dictionary<string, object> customData = null)
        {
            if (callback == null)
                throw new ArgumentNullException("A callback is required for Analyze");
            if (parameters == null)
                throw new ArgumentNullException("parameters is required for Analyze");

            RequestObject<AnalysisResults> req = new RequestObject<AnalysisResults>
            {
                Callback = callback,
                HttpMethod = UnityWebRequest.kHttpVerbPOST,
                DisableSslVerification = DisableSslVerification,
                CustomData = customData == null ? new Dictionary<string, object>() : customData
            };

            if (req.CustomData.ContainsKey(Constants.String.CUSTOM_REQUEST_HEADERS))
            {
                foreach (KeyValuePair<string, string> kvp in req.CustomData[Constants.String.CUSTOM_REQUEST_HEADERS] as Dictionary<string, string>)
                {
                    req.Headers.Add(kvp.Key, kvp.Value);
                }
            }

            req.Headers["X-IBMCloud-SDK-Analytics"] = "service_name=natural-language-understanding;service_version=V1;operation_id=Analyze";
            req.Parameters["version"] = VersionDate;
            req.Headers["Content-Type"] = "application/json";
            req.Headers["Accept"] = "application/json";
            if (parameters != null)
            {
                req.Send = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(parameters));
            }

            req.OnResponse = OnAnalyzeResponse;

            RESTConnector connector = RESTConnector.GetConnector(Credentials, "/v1/analyze");
            if (connector == null)
            {
                return false;
            }

            return connector.Send(req);
        }

        private void OnAnalyzeResponse(RESTConnector.Request req, RESTConnector.Response resp)
        {
            DetailedResponse<AnalysisResults> response = new DetailedResponse<AnalysisResults>();
            Dictionary<string, object> customData = ((RequestObject<AnalysisResults>)req).CustomData;
            foreach (KeyValuePair<string, string> kvp in resp.Headers)
            {
                response.Headers.Add(kvp.Key, kvp.Value);
            }
            response.StatusCode = resp.HttpResponseCode;

            try
            {
                string json = Encoding.UTF8.GetString(resp.Data);
                response.Result = JsonConvert.DeserializeObject<AnalysisResults>(json);
                customData.Add("json", json);
            }
            catch (Exception e)
            {
                Log.Error("NaturalLanguageUnderstandingService.OnAnalyzeResponse()", "Exception: {0}", e.ToString());
                resp.Success = false;
            }

            if (((RequestObject<AnalysisResults>)req).Callback != null)
                ((RequestObject<AnalysisResults>)req).Callback(response, resp.Error, customData);
        }
        /// <summary>
        /// Delete model.
        ///
        /// Deletes a custom model.
        /// </summary>
        /// <param name="callback">The callback function that is invoked when the operation completes.</param>
        /// <param name="modelId">Model ID of the model to delete.</param>
        /// <returns><see cref="DeleteModelResults" />DeleteModelResults</returns>
        /// <param name="customData">A Dictionary<string, object> of data that will be passed to the callback. The raw
        /// json output from the REST call will be passed in this object as the value of the 'json'
        /// key.</string></param>
        public bool DeleteModel(Callback<DeleteModelResults> callback, string modelId, Dictionary<string, object> customData = null)
        {
            if (callback == null)
                throw new ArgumentNullException("A callback is required for DeleteModel");
            if (string.IsNullOrEmpty(modelId))
                throw new ArgumentNullException("modelId is required for DeleteModel");

            RequestObject<DeleteModelResults> req = new RequestObject<DeleteModelResults>
            {
                Callback = callback,
                HttpMethod = UnityWebRequest.kHttpVerbDELETE,
                DisableSslVerification = DisableSslVerification,
                CustomData = customData == null ? new Dictionary<string, object>() : customData
            };

            if (req.CustomData.ContainsKey(Constants.String.CUSTOM_REQUEST_HEADERS))
            {
                foreach (KeyValuePair<string, string> kvp in req.CustomData[Constants.String.CUSTOM_REQUEST_HEADERS] as Dictionary<string, string>)
                {
                    req.Headers.Add(kvp.Key, kvp.Value);
                }
            }

            req.Headers["X-IBMCloud-SDK-Analytics"] = "service_name=natural-language-understanding;service_version=V1;operation_id=DeleteModel";
            req.Parameters["version"] = VersionDate;

            req.OnResponse = OnDeleteModelResponse;

            RESTConnector connector = RESTConnector.GetConnector(Credentials, string.Format("/v1/models/{0}", modelId));
            if (connector == null)
            {
                return false;
            }

            return connector.Send(req);
        }

        private void OnDeleteModelResponse(RESTConnector.Request req, RESTConnector.Response resp)
        {
            DetailedResponse<DeleteModelResults> response = new DetailedResponse<DeleteModelResults>();
            Dictionary<string, object> customData = ((RequestObject<DeleteModelResults>)req).CustomData;
            foreach (KeyValuePair<string, string> kvp in resp.Headers)
            {
                response.Headers.Add(kvp.Key, kvp.Value);
            }
            response.StatusCode = resp.HttpResponseCode;

            try
            {
                string json = Encoding.UTF8.GetString(resp.Data);
                response.Result = JsonConvert.DeserializeObject<DeleteModelResults>(json);
                customData.Add("json", json);
            }
            catch (Exception e)
            {
                Log.Error("NaturalLanguageUnderstandingService.OnDeleteModelResponse()", "Exception: {0}", e.ToString());
                resp.Success = false;
            }

            if (((RequestObject<DeleteModelResults>)req).Callback != null)
                ((RequestObject<DeleteModelResults>)req).Callback(response, resp.Error, customData);
        }
        /// <summary>
        /// List models.
        ///
        /// Lists Watson Knowledge Studio [custom
        /// models](https://cloud.ibm.com/docs/services/natural-language-understanding/customizing.html) that are
        /// deployed to your Natural Language Understanding service.
        /// </summary>
        /// <param name="callback">The callback function that is invoked when the operation completes.</param>
        /// <returns><see cref="ListModelsResults" />ListModelsResults</returns>
        /// <param name="customData">A Dictionary<string, object> of data that will be passed to the callback. The raw
        /// json output from the REST call will be passed in this object as the value of the 'json'
        /// key.</string></param>
        public bool ListModels(Callback<ListModelsResults> callback, Dictionary<string, object> customData = null)
        {
            if (callback == null)
                throw new ArgumentNullException("A callback is required for ListModels");

            RequestObject<ListModelsResults> req = new RequestObject<ListModelsResults>
            {
                Callback = callback,
                HttpMethod = UnityWebRequest.kHttpVerbGET,
                DisableSslVerification = DisableSslVerification,
                CustomData = customData == null ? new Dictionary<string, object>() : customData
            };

            if (req.CustomData.ContainsKey(Constants.String.CUSTOM_REQUEST_HEADERS))
            {
                foreach (KeyValuePair<string, string> kvp in req.CustomData[Constants.String.CUSTOM_REQUEST_HEADERS] as Dictionary<string, string>)
                {
                    req.Headers.Add(kvp.Key, kvp.Value);
                }
            }

            req.Headers["X-IBMCloud-SDK-Analytics"] = "service_name=natural-language-understanding;service_version=V1;operation_id=ListModels";
            req.Parameters["version"] = VersionDate;

            req.OnResponse = OnListModelsResponse;

            RESTConnector connector = RESTConnector.GetConnector(Credentials, "/v1/models");
            if (connector == null)
            {
                return false;
            }

            return connector.Send(req);
        }

        private void OnListModelsResponse(RESTConnector.Request req, RESTConnector.Response resp)
        {
            DetailedResponse<ListModelsResults> response = new DetailedResponse<ListModelsResults>();
            Dictionary<string, object> customData = ((RequestObject<ListModelsResults>)req).CustomData;
            foreach (KeyValuePair<string, string> kvp in resp.Headers)
            {
                response.Headers.Add(kvp.Key, kvp.Value);
            }
            response.StatusCode = resp.HttpResponseCode;

            try
            {
                string json = Encoding.UTF8.GetString(resp.Data);
                response.Result = JsonConvert.DeserializeObject<ListModelsResults>(json);
                customData.Add("json", json);
            }
            catch (Exception e)
            {
                Log.Error("NaturalLanguageUnderstandingService.OnListModelsResponse()", "Exception: {0}", e.ToString());
                resp.Success = false;
            }

            if (((RequestObject<ListModelsResults>)req).Callback != null)
                ((RequestObject<ListModelsResults>)req).Callback(response, resp.Error, customData);
        }
    }
}