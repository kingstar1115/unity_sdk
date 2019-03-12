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
using IBM.Watson.PersonalityInsights.V3.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine.Networking;

namespace IBM.Watson.PersonalityInsights.V3
{
    public class PersonalityInsightsService : BaseService
    {
        private const string serviceId = "personality_insights";
        private const string defaultUrl = "https://gateway.watsonplatform.net/personality-insights/api";

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
        /// PersonalityInsightsService constructor.
        /// </summary>
        /// <param name="versionDate">The service version date in `yyyy-mm-dd` format.</param>
        public PersonalityInsightsService(string versionDate) : base(versionDate, serviceId)
        {
            VersionDate = versionDate;
        }

        /// <summary>
        /// PersonalityInsightsService constructor.
        /// </summary>
        /// <param name="versionDate">The service version date in `yyyy-mm-dd` format.</param>
        /// <param name="credentials">The service credentials.</param>
        public PersonalityInsightsService(string versionDate, Credentials credentials) : base(versionDate, credentials, serviceId)
        {
            if (string.IsNullOrEmpty(versionDate))
            {
                throw new ArgumentNullException("A versionDate (format `yyyy-mm-dd`) is required to create an instance of PersonalityInsightsService");
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
                throw new IBMException("Please provide a username and password or authorization token to use the PersonalityInsights service. For more information, see https://github.com/watson-developer-cloud/unity-sdk/#configuring-your-service-credentials");
            }
        }

        /// <summary>
        /// Get profile.
        ///
        /// Generates a personality profile for the author of the input text. The service accepts a maximum of 20 MB of
        /// input content, but it requires much less text to produce an accurate profile. The service can analyze text
        /// in Arabic, English, Japanese, Korean, or Spanish. It can return its results in a variety of languages.
        ///
        /// **See also:**
        /// * [Requesting a profile](https://cloud.ibm.com/docs/services/personality-insights/input.html)
        /// * [Providing sufficient
        /// input](https://cloud.ibm.com/docs/services/personality-insights/input.html#sufficient)
        ///
        /// ### Content types
        ///
        ///  You can provide input content as plain text (`text/plain`), HTML (`text/html`), or JSON
        /// (`application/json`) by specifying the **Content-Type** parameter. The default is `text/plain`.
        /// * Per the JSON specification, the default character encoding for JSON content is effectively always UTF-8.
        /// * Per the HTTP specification, the default encoding for plain text and HTML is ISO-8859-1 (effectively, the
        /// ASCII character set).
        ///
        /// When specifying a content type of plain text or HTML, include the `charset` parameter to indicate the
        /// character encoding of the input text; for example, `Content-Type: text/plain;charset=utf-8`.
        ///
        /// **See also:** [Specifying request and response
        /// formats](https://cloud.ibm.com/docs/services/personality-insights/input.html#formats)
        ///
        /// ### Accept types
        ///
        ///  You must request a response as JSON (`application/json`) or comma-separated values (`text/csv`) by
        /// specifying the **Accept** parameter. CSV output includes a fixed number of columns. Set the **csv_headers**
        /// parameter to `true` to request optional column headers for CSV output.
        ///
        /// **See also:**
        /// * [Understanding a JSON profile](https://cloud.ibm.com/docs/services/personality-insights/output.html)
        /// * [Understanding a CSV profile](https://cloud.ibm.com/docs/services/personality-insights/output-csv.html).
        /// </summary>
        /// <param name="callback">The callback function that is invoked when the operation completes.</param>
        /// <param name="content">A maximum of 20 MB of content to analyze, though the service requires much less text;
        /// for more information, see [Providing sufficient
        /// input](https://cloud.ibm.com/docs/services/personality-insights/input.html#sufficient). For JSON input,
        /// provide an object of type `Content`.</param>
        /// <param name="contentLanguage">The language of the input text for the request: Arabic, English, Japanese,
        /// Korean, or Spanish. Regional variants are treated as their parent language; for example, `en-US` is
        /// interpreted as `en`.
        ///
        /// The effect of the **Content-Language** parameter depends on the **Content-Type** parameter. When
        /// **Content-Type** is `text/plain` or `text/html`, **Content-Language** is the only way to specify the
        /// language. When **Content-Type** is `application/json`, **Content-Language** overrides a language specified
        /// with the `language` parameter of a `ContentItem` object, and content items that specify a different language
        /// are ignored; omit this parameter to base the language on the specification of the content items. You can
        /// specify any combination of languages for **Content-Language** and **Accept-Language**. (optional, default to
        /// en)</param>
        /// <param name="acceptLanguage">The desired language of the response. For two-character arguments, regional
        /// variants are treated as their parent language; for example, `en-US` is interpreted as `en`. You can specify
        /// any combination of languages for the input and response content. (optional, default to en)</param>
        /// <param name="rawScores">Indicates whether a raw score in addition to a normalized percentile is returned for
        /// each characteristic; raw scores are not compared with a sample population. By default, only normalized
        /// percentiles are returned. (optional, default to false)</param>
        /// <param name="csvHeaders">Indicates whether column labels are returned with a CSV response. By default, no
        /// column labels are returned. Applies only when the response type is CSV (`text/csv`). (optional, default to
        /// false)</param>
        /// <param name="consumptionPreferences">Indicates whether consumption preferences are returned with the
        /// results. By default, no consumption preferences are returned. (optional, default to false)</param>
        /// <param name="contentType">The type of the input. For more information, see **Content types** in the method
        /// description.
        ///
        /// Default: `text/plain`. (optional)</param>
        /// <param name="customData">A Dictionary<string, object> of data that will be passed to the callback. The raw
        /// json output from the REST call will be passed in this object as the value of the 'json'
        /// key.</string></param>
        /// <returns><see cref="Profile" />Profile</returns>
        public bool Profile(Callback<Profile> callback, Content content, Dictionary<string, object> customData = null, string contentLanguage = null, string acceptLanguage = null, bool? rawScores = null, bool? csvHeaders = null, bool? consumptionPreferences = null, string contentType = null)
        {
            if (callback == null)
                throw new ArgumentNullException("`callback` is required for `Profile`");
            if (content == null)
                throw new ArgumentNullException("`content` is required for `Profile`");

            RequestObject<Profile> req = new RequestObject<Profile>
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

            foreach(KeyValuePair<string, string> kvp in Common.GetDefaultheaders("personality_insights", "V3", "Profile"))
            {
                req.Headers.Add(kvp.Key, kvp.Value);
            }

            req.Parameters["version"] = VersionDate;
            if (!string.IsNullOrEmpty(contentLanguage))
            {
                req.Headers["Content-Language"] = contentLanguage;
            }
            if (!string.IsNullOrEmpty(acceptLanguage))
            {
                req.Headers["Accept-Language"] = acceptLanguage;
            }
            if (!string.IsNullOrEmpty(contentType))
            {
                req.Headers["Content-Type"] = contentType;
            }
            if (rawScores != null)
            {
                req.Parameters["raw_scores"] = (bool)rawScores ? "true" : "false";
            }
            if (csvHeaders != null)
            {
                req.Parameters["csv_headers"] = (bool)csvHeaders ? "true" : "false";
            }
            if (consumptionPreferences != null)
            {
                req.Parameters["consumption_preferences"] = (bool)consumptionPreferences ? "true" : "false";
            }
            req.Headers["Content-Type"] = "application/json";
            req.Headers["Accept"] = "application/json";

            JObject bodyObject = new JObject();
            req.Send = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(bodyObject));

            req.OnResponse = OnProfileResponse;

            RESTConnector connector = RESTConnector.GetConnector(Credentials, "/v3/profile");
            if (connector == null)
            {
                return false;
            }

            return connector.Send(req);
        }

        private void OnProfileResponse(RESTConnector.Request req, RESTConnector.Response resp)
        {
            DetailedResponse<Profile> response = new DetailedResponse<Profile>();
            Dictionary<string, object> customData = ((RequestObject<Profile>)req).CustomData;
            foreach (KeyValuePair<string, string> kvp in resp.Headers)
            {
                response.Headers.Add(kvp.Key, kvp.Value);
            }
            response.StatusCode = resp.HttpResponseCode;

            try
            {
                string json = Encoding.UTF8.GetString(resp.Data);
                response.Result = JsonConvert.DeserializeObject<Profile>(json);
                customData.Add("json", json);
            }
            catch (Exception e)
            {
                Log.Error("PersonalityInsightsService.OnProfileResponse()", "Exception: {0}", e.ToString());
                resp.Success = false;
            }

            if (((RequestObject<Profile>)req).Callback != null)
                ((RequestObject<Profile>)req).Callback(response, resp.Error, customData);
        }
        /// <summary>
        /// Get profile as csv.
        ///
        /// Generates a personality profile for the author of the input text. The service accepts a maximum of 20 MB of
        /// input content, but it requires much less text to produce an accurate profile. The service can analyze text
        /// in Arabic, English, Japanese, Korean, or Spanish. It can return its results in a variety of languages.
        ///
        /// **See also:**
        /// * [Requesting a profile](https://cloud.ibm.com/docs/services/personality-insights/input.html)
        /// * [Providing sufficient
        /// input](https://cloud.ibm.com/docs/services/personality-insights/input.html#sufficient)
        ///
        /// ### Content types
        ///
        ///  You can provide input content as plain text (`text/plain`), HTML (`text/html`), or JSON
        /// (`application/json`) by specifying the **Content-Type** parameter. The default is `text/plain`.
        /// * Per the JSON specification, the default character encoding for JSON content is effectively always UTF-8.
        /// * Per the HTTP specification, the default encoding for plain text and HTML is ISO-8859-1 (effectively, the
        /// ASCII character set).
        ///
        /// When specifying a content type of plain text or HTML, include the `charset` parameter to indicate the
        /// character encoding of the input text; for example, `Content-Type: text/plain;charset=utf-8`.
        ///
        /// **See also:** [Specifying request and response
        /// formats](https://cloud.ibm.com/docs/services/personality-insights/input.html#formats)
        ///
        /// ### Accept types
        ///
        ///  You must request a response as JSON (`application/json`) or comma-separated values (`text/csv`) by
        /// specifying the **Accept** parameter. CSV output includes a fixed number of columns. Set the **csv_headers**
        /// parameter to `true` to request optional column headers for CSV output.
        ///
        /// **See also:**
        /// * [Understanding a JSON profile](https://cloud.ibm.com/docs/services/personality-insights/output.html)
        /// * [Understanding a CSV profile](https://cloud.ibm.com/docs/services/personality-insights/output-csv.html).
        /// </summary>
        /// <param name="callback">The callback function that is invoked when the operation completes.</param>
        /// <param name="content">A maximum of 20 MB of content to analyze, though the service requires much less text;
        /// for more information, see [Providing sufficient
        /// input](https://cloud.ibm.com/docs/services/personality-insights/input.html#sufficient). For JSON input,
        /// provide an object of type `Content`.</param>
        /// <param name="contentLanguage">The language of the input text for the request: Arabic, English, Japanese,
        /// Korean, or Spanish. Regional variants are treated as their parent language; for example, `en-US` is
        /// interpreted as `en`.
        ///
        /// The effect of the **Content-Language** parameter depends on the **Content-Type** parameter. When
        /// **Content-Type** is `text/plain` or `text/html`, **Content-Language** is the only way to specify the
        /// language. When **Content-Type** is `application/json`, **Content-Language** overrides a language specified
        /// with the `language` parameter of a `ContentItem` object, and content items that specify a different language
        /// are ignored; omit this parameter to base the language on the specification of the content items. You can
        /// specify any combination of languages for **Content-Language** and **Accept-Language**. (optional, default to
        /// en)</param>
        /// <param name="acceptLanguage">The desired language of the response. For two-character arguments, regional
        /// variants are treated as their parent language; for example, `en-US` is interpreted as `en`. You can specify
        /// any combination of languages for the input and response content. (optional, default to en)</param>
        /// <param name="rawScores">Indicates whether a raw score in addition to a normalized percentile is returned for
        /// each characteristic; raw scores are not compared with a sample population. By default, only normalized
        /// percentiles are returned. (optional, default to false)</param>
        /// <param name="csvHeaders">Indicates whether column labels are returned with a CSV response. By default, no
        /// column labels are returned. Applies only when the response type is CSV (`text/csv`). (optional, default to
        /// false)</param>
        /// <param name="consumptionPreferences">Indicates whether consumption preferences are returned with the
        /// results. By default, no consumption preferences are returned. (optional, default to false)</param>
        /// <param name="contentType">The type of the input. For more information, see **Content types** in the method
        /// description.
        ///
        /// Default: `text/plain`. (optional)</param>
        /// <param name="customData">A Dictionary<string, object> of data that will be passed to the callback. The raw
        /// json output from the REST call will be passed in this object as the value of the 'json'
        /// key.</string></param>
        /// <returns><see cref="System.IO.FileStream" />System.IO.FileStream</returns>
        public bool ProfileAsCsv(Callback<System.IO.FileStream> callback, Content content, Dictionary<string, object> customData = null, string contentLanguage = null, string acceptLanguage = null, bool? rawScores = null, bool? csvHeaders = null, bool? consumptionPreferences = null, string contentType = null)
        {
            if (callback == null)
                throw new ArgumentNullException("`callback` is required for `ProfileAsCsv`");
            if (content == null)
                throw new ArgumentNullException("`content` is required for `ProfileAsCsv`");

            RequestObject<System.IO.FileStream> req = new RequestObject<System.IO.FileStream>
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

            foreach(KeyValuePair<string, string> kvp in Common.GetDefaultheaders("personality_insights", "V3", "ProfileAsCsv"))
            {
                req.Headers.Add(kvp.Key, kvp.Value);
            }

            req.Parameters["version"] = VersionDate;
            if (!string.IsNullOrEmpty(contentLanguage))
            {
                req.Headers["Content-Language"] = contentLanguage;
            }
            if (!string.IsNullOrEmpty(acceptLanguage))
            {
                req.Headers["Accept-Language"] = acceptLanguage;
            }
            if (!string.IsNullOrEmpty(contentType))
            {
                req.Headers["Content-Type"] = contentType;
            }
            if (rawScores != null)
            {
                req.Parameters["raw_scores"] = (bool)rawScores ? "true" : "false";
            }
            if (csvHeaders != null)
            {
                req.Parameters["csv_headers"] = (bool)csvHeaders ? "true" : "false";
            }
            if (consumptionPreferences != null)
            {
                req.Parameters["consumption_preferences"] = (bool)consumptionPreferences ? "true" : "false";
            }
            req.Headers["Content-Type"] = "application/json";
            req.Headers["Accept"] = "text/csv";

            JObject bodyObject = new JObject();
            req.Send = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(bodyObject));

            req.OnResponse = OnProfileAsCsvResponse;

            RESTConnector connector = RESTConnector.GetConnector(Credentials, "/v3/profile");
            if (connector == null)
            {
                return false;
            }

            return connector.Send(req);
        }

        private void OnProfileAsCsvResponse(RESTConnector.Request req, RESTConnector.Response resp)
        {
            DetailedResponse<System.IO.FileStream> response = new DetailedResponse<System.IO.FileStream>();
            Dictionary<string, object> customData = ((RequestObject<System.IO.FileStream>)req).CustomData;
            foreach (KeyValuePair<string, string> kvp in resp.Headers)
            {
                response.Headers.Add(kvp.Key, kvp.Value);
            }
            response.StatusCode = resp.HttpResponseCode;

            try
            {
                string json = Encoding.UTF8.GetString(resp.Data);
                response.Result = JsonConvert.DeserializeObject<System.IO.FileStream>(json);
                customData.Add("json", json);
            }
            catch (Exception e)
            {
                Log.Error("PersonalityInsightsService.OnProfileAsCsvResponse()", "Exception: {0}", e.ToString());
                resp.Success = false;
            }

            if (((RequestObject<System.IO.FileStream>)req).Callback != null)
                ((RequestObject<System.IO.FileStream>)req).Callback(response, resp.Error, customData);
        }
    }
}