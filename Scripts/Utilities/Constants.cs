﻿

using System;
/**
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
namespace IBM.Watson.DeveloperCloud.Utilities
{
    /// <summary>
    /// This class wraps all constants.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// All constant path variables liste here. Exp. Configuration file
        /// </summary>
        public static class Path
        {
            /// <summary>
            /// Configuration file name.
            /// </summary>
            public const string CONFIG_FILE = "/Config.json";
            /// <summary>
            /// Cache folder to customize a parent folder for cache directory
            /// </summary>
            public static string CACHE_FOLDER = "";   //It needs to start with /
                                                      /// <summary>
                                                      /// Log folder to customize a parent folder for logs
                                                      /// </summary>
            public static string LOG_FOLDER = "";    //It needs to start with /
        }

        /// <summary>
        /// All resources (files names under resource directory) used in the SDK listed here. Exp. Watson Logo
        /// </summary>
        public static class Resources
        {
            /// <summary>
            /// Watson icon.
            /// </summary>
            public const string WATSON_ICON = "watsonSpriteIcon-32x32";
            /// <summary>
            /// Watson logo.
            /// </summary>
            public const string WATSON_LOGO = "watsonSpriteLogo-506x506";
        }

        /// <summary>
        /// All string variables or string formats used in the SDK listed here. Exp. Quality Debug Format = Quality {0}
        /// </summary>
        public static class String
        {
            /// <exclude />
            public const string VERSION = "watson-apis-unity-sdk/1.0.0";
            /// <exclude />
            public const string DEBUG_DISPLAY_QUALITY = "Quality: {0}";
        }

        /// <summary>
        /// Variables for configuration.
        /// </summary>
        public static class Config
        {
            /// <summary>
            /// Maximum number of REST connections allowed at once.
            /// </summary>
            public static int MaxRestConnections = 5;
            /// <summary>
            /// Default time in seconds after which a call should timeout.
            /// </summary>
            public static float Timeout = 120.0f;
        }

        /// <summary>
        /// Variables for authentication tokens
        /// </summary>
        public static class Token
        {
            /// <summary>
            /// The time in minutes after which the authentication token is expired.
            /// </summary>
            public static float TOKEN_TIME_TO_LIVE = 60f;
        }
    }
}
