/**
* (C) Copyright IBM Corp. 2021.
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

using Newtonsoft.Json;

namespace IBM.Watson.Assistant.V1.Model
{
    /// <summary>
    /// DialogNodeVisitedDetails.
    /// </summary>
    public class DialogNodeVisitedDetails
    {
        /// <summary>
        /// The unique ID of a dialog node that was triggered during processing of the input message.
        /// </summary>
        [JsonProperty("dialog_node", NullValueHandling = NullValueHandling.Ignore)]
        public string DialogNode { get; set; }
        /// <summary>
        /// The title of the dialog node.
        /// </summary>
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }
        /// <summary>
        /// The conditions that trigger the dialog node.
        /// </summary>
        [JsonProperty("conditions", NullValueHandling = NullValueHandling.Ignore)]
        public string Conditions { get; set; }
    }
}
