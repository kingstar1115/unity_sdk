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
using IBM.Watson.DeveloperCloud.Utilities;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace  IBM.Watson.DeveloperCloud.Services.CompareComply.v1
{
    /// <summary>
    /// Identification of a specific type.
    /// </summary>
    [fsObject(Converter = typeof(TypeLabelConverter))]
    public class TypeLabel
    {
        /// <summary>
        /// A pair of `nature` and `party` objects. The `nature` object identifies the effect of the element on the
        /// identified `party`, and the `party` object identifies the affected party.
        /// </summary>
        [fsProperty("label")]
        public Label Label { get; set; }
        /// <summary>
        /// One or more hash values that you can send to IBM to provide feedback or receive support.
        /// </summary>
        [fsProperty("provenance_ids")]
        public List<string> ProvenanceIds { get; set; }
        /// <summary>
        /// A string identifying the type of modification the feedback entry in the `updated_labels` array. Possible
        /// values are `added`, `unchanged`, and `removed`.
        /// </summary>
        public enum ModificationEnum
        {
            /// <summary>
            /// Enum added for added
            /// </summary>
            [EnumMember(Value = "added")]
            added,
            /// <summary>
            /// Enum notChanged for unchanged
            /// </summary>
            [EnumMember(Value = "unchanged")]
            unchanged,
            /// <summary>
            /// Enum removed for removed
            /// </summary>
            [EnumMember(Value = "removed")]
            removed
        }

        /// <summary>
        /// A string identifying the type of modification the feedback entry in the `updated_labels` array. Possible
        /// values are `added`, `unchanged`, and `removed`.
        /// </summary>
        [fsProperty("modification")]
        public ModificationEnum? Modification { get; set; }
    }

    #region TypeLabelConverter Converter
    public class TypeLabelConverter : fsConverter
    {
        private fsSerializer _serializer = new fsSerializer();

        public override bool CanProcess(Type type)
        {
            return type == typeof(TypeLabel);
        }

        public override fsResult TryDeserialize(fsData data, ref object instance, Type storageType)
        {
            if (data.Type != fsDataType.Object)
            {
                return fsResult.Fail("Expected object fsData type but got " + data.Type);
            }

            var myType = (TypeLabel)instance;
            Dictionary<string, fsData> dataDict = data.AsDictionary;

            if (dataDict.ContainsKey("label"))
            {
                if (!dataDict["label"].IsNull)
                {
                    Label typeLabel = new Label();
                    object obj = typeLabel;
                    var r = _serializer.TryDeserialize(dataDict["label"], obj.GetType(), ref obj);
                    if (!r.Succeeded)
                        throw new WatsonException(r.FormattedMessages);
                    myType.Label = typeLabel;
                }
            }

            if (dataDict.ContainsKey("provenance_ids"))
            {
                if (!dataDict["provenance_ids"].IsNull && dataDict["provenance_ids"].IsList)
                {
                    List<fsData> dataList = dataDict["provenance_ids"].AsList;
                    List<string> dataStringList = new List<string>();
                    foreach (fsData fsDataString in dataList)
                    {
                        dataStringList.Add(fsDataString.AsString);
                    }
                    myType.ProvenanceIds = dataStringList;
                }
            }

            if (dataDict.ContainsKey("modification"))
            {
                if (!dataDict["modification"].IsNull)
                {
                    if (dataDict["modification"].AsString == TypeLabel.ModificationEnum.added.ToString())
                        myType.Modification = TypeLabel.ModificationEnum.added;
                    if (dataDict["modification"].AsString == TypeLabel.ModificationEnum.removed.ToString())
                        myType.Modification = TypeLabel.ModificationEnum.removed;
                    if (dataDict["modification"].AsString == TypeLabel.ModificationEnum.unchanged.ToString())
                        myType.Modification = TypeLabel.ModificationEnum.unchanged;
                }
            }
            
            return fsResult.Success;
        }

        public override object CreateInstance(fsData data, Type storageType)
        {
            return new TypeLabel();
        }

        public override fsResult TrySerialize(object instance, out fsData serialized, Type storageType)
        {
            TypeLabel typeLabel = (TypeLabel)instance;
            serialized = null;

            Dictionary<string, fsData> serialization = new Dictionary<string, fsData>();

            fsData tempData = null;

            if (typeLabel.Label != null)
            {
                _serializer.TrySerialize(typeLabel.Label, out tempData);
                serialization.Add("label", tempData);
            }

            if (typeLabel.ProvenanceIds != null && typeLabel.ProvenanceIds.Count > 0)
            {
                _serializer.TrySerialize(typeLabel.ProvenanceIds, out tempData);
                serialization.Add("provenance_ids", tempData);
            }

            serialized = new fsData(serialization);

            return fsResult.Success;
        }
    }
    #endregion
}
