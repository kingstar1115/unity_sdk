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
* @author Dogukan Erenel (derenel@us.ibm.com)
*/

using UnityEngine;
using System.Collections;

namespace IBM.Watson.Utilities
{
    public static class Constants
    {

        /// <summary>
        /// All Keycodes used via keyboard listed here.
        /// </summary>
        public static class KeyCodes
        {
			//Debug Mode Key Pressed to use the keys given below
			public const KeyModifiers MODIFIER_KEY = KeyModifiers.SHIFT;

            //Cube actions keycodes
            public const KeyCode CUBE_TO_FOLD = KeyCode.F;
            public const KeyCode CUBE_TO_UNFOLD = KeyCode.U;
            public const KeyCode CUBE_TO_FOCUS = KeyCode.Z;
            public const KeyCode CUBE_TO_UNFOCUS = KeyCode.X;
			public const KeyCode CUBE_TO_ROTATE_OR_PAUSE = KeyCode.R;

        }

        /// <summary>
        /// All constant event names used in SDK and Applications listed here. 
        /// </summary>
        public enum Event
        {
            NONE = -1,

            #region Debug
            /// <summary>
            /// Send a debug command.
            /// </summary>
            ON_DEBUG_COMMAND = 0,
            /// <summary>
            /// TOggle the debug console on or off.
            /// </summary>
            ON_DEBUG_TOGGLE,
            /// <summary>
            /// BEgin editing a command in the debug console.
            /// </summary>
            ON_DEBUG_BEGIN_COMMAND,
            /// <summary>
            /// Event to send debug message
            /// </summary>
            ON_DEBUG_MESSAGE,
            /// <summary>
            /// Event to change quality settings
            /// </summary>
            ON_CHANGE_QUALITY,
            /// <summary>
            /// Event after Quality Settings change
            /// </summary>
            ON_CHANGE_QUALITY_FINISH,
            #endregion

            #region Avatar 
            /// <summary>
            /// Event to change Avatar mood
            /// </summary>
            ON_CHANGE_AVATAR_MOOD = 100,
            /// <summary>
            /// Event after Avatar mood change
            /// </summary>
            ON_CHANGE_AVATAR_MOOD_FINISH,
            /// <summary>
            /// Event to change Avatar state
            /// </summary>
            ON_CHANGE_AVATAR_STATE,
            /// <summary>
            /// Event after Avatar state change
            /// </summary>
            ON_CHANGE_AVATAR_STATE_FINISH,
            #endregion

            #region Question
            /// <summary>
            /// Event on Question Cube state change
            /// </summary>
            ON_CHANGE_STATE_QUESTIONCUBE_ANIMATION = 200,
			/// <summary>
			/// Event on Question tap inside question cube
			/// </summary>
			ON_QUESTIONCUBE_TAP_INSIDE,
			/// <summary>
			/// Event on Question tap outside question cube
			/// </summary>
			ON_QUESTIONCUBE_TAP_OUTSIDE,
			/// <summary>
			/// Event on Questino drag 
			/// </summary>
			ON_QUESTIONCUBE_DRAG_ONE_FINGER,
            #endregion

            #region Animation / Camera
            /// <summary>
            /// Event to Stop the all animations
            /// </summary>
            ON_ANIMATION_STOP = 300,
            /// <summary>
            /// Event to pause the all animations playing
            /// </summary>
            ON_ANIMATION_PAUSE,
            /// <summary>
            /// Event to resume the paused animations
            /// </summary>
            ON_ANIMATION_RESUME,
            /// <summary>
            /// Event to speed-up the animations
            /// </summary>
            ON_ANIMATION_SPEED_UP,
            /// <summary>
            /// Event to speed-down the animations
            /// </summary>
            ON_ANIMATION_SPEED_DOWN,
            /// <summary>
            /// Event to set the default speed on animations
            /// </summary>
            ON_ANIMATION_SPEED_DEFAULT,
            /// <summary>
            /// Event to drag camera with two finger to zoom and pan
            /// </summary>
            ON_CAMERA_DRAG_TWO_FINGER,
            #endregion

            #region NLC

            /// <summary>
            /// 
            /// </summary>
            ON_CLASSIFY_FAILURE = 400,
            /// <summary>
            /// 
            /// </summary>
            ON_CLASSIFY_QUESTION,
            /// <summary>
            /// 
            /// </summary>
            ON_CLASSIFY_DIALOG,
            /// <summary>
            /// 
            /// </summary>
            ON_COMMAND_WAKEUP,
            /// <summary>
            /// 
            /// </summary>
            ON_COMMAND_SLEEP,
            /// <summary>
            /// 
            /// </summary>
            ON_COMMAND_DEBUGON,
            /// <summary>
            /// 
            /// </summary>
            ON_COMMAND_DEBUGOFF,
            /// <summary>
            /// 
            /// </summary>
            ON_COMMAND_FOLD,
            /// <summary>
            /// 
            /// </summary>
            ON_COMMAND_UNFOLD,
            /// <summary>
            /// 
            /// </summary>
            ON_COMMAND_ANSWERS,
            /// <summary>
            /// 
            /// </summary>
            ON_COMMAND_EVIDENCE,
            /// <summary>
            /// 
            /// </summary>
            ON_COMMAND_CHAT,
            /// <summary>
            /// 
            /// </summary>
            ON_COMMAND_PARSE,
            /// <summary>
            /// 
            /// </summary>
            ON_COMMAND_LOCATION,

            #endregion
        }

        /// <summary>
        /// All constant path variables liste here. Exp. Configuration file
        /// </summary>
        public static class Path
        {
            public const string CONFIG_FILE = "/Config.json";
        }

        /// <summary>
        /// All resources (files names under resource directory) used in the SDK listed here. Exp. Watson Logo
        /// </summary>
        public static class Resources
        {
            public const string WATSON_ICON = "WatsonSpriteIcon_32x32";
            public const string WATSON_LOGO = "WatsonSpriteLogo_506x506";

        }

        /// <summary>
        /// All string variables or string formats used in the SDK listed here. Exp. Quality Debug Format = Quality {0}
        /// </summary>
        public static class String
        {
            public const string DEBUG_DISPLAY_QUALITY = "Quality: {0}";
            public const string DEBUG_DISPLAY_AVATAR_MOOD = "Behavior:{0} Mood: {1}";
        }


    }
}


