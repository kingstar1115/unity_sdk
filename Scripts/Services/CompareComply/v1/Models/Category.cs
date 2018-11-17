/**
* Copyright 2018 IBM Corp. All Rights Reserved.
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

using FullSerializer;
using System.Collections.Generic;

namespace  IBM.Watson.DeveloperCloud.Services.CompareComply.v1
{
    /// <summary>
    /// Information defining an element's subject matter.
    /// </summary>
    [fsObject]
    public class Category
    {
        /// <summary>
        /// The category of the associated element.
        /// </summary>
        [fsProperty("label")]
        public string Label { get; set; }
        /// <summary>
        /// One or more hashed values that you can send to IBM to provide feedback or receive support.
        /// </summary>
        [fsProperty("provenance_ids")]
        public List<string> ProvenanceIds { get; set; }
    }

}
