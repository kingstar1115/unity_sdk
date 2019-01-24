/**
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

using System.Collections.Generic;
using FullSerializer;
using IBM.Watson.Connection;

namespace IBM.Watson.ToneAnalyzer.v3.Model
{
    /// <summary>
    /// ToneChatInput.
    /// </summary>
    public class ToneChatInput
    {
        /// <summary>
        /// An array of `Utterance` objects that provides the input content that the service is to analyze.
        /// </summary>
        [fsProperty("utterances")]
        public List<Utterance> Utterances { get; set; }
    }


}
