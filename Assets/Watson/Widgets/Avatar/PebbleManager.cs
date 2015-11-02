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
using IBM.Watson.Logging;

namespace IBM.Watson.Widgets.Avatar
{

	[System.Serializable]
	public class PebbleRow
	{
		public GameObject[] pebbleList;
	}

	public class PebbleManager : MonoBehaviour {

		public PebbleRow[] pebbleRowList = null;

		// Use this for initialization
		void Start () {
			if (pebbleRowList == null) {
				Log.Error("PebbleManager", "pebbleRowList is null! This shouldn't be!");
				return;
			}
		}

		public Vector2 smoothnessLimitBetweenRows; //Smothness MIN, MAX

		public bool setDataOnFrame = false;
		public float latestValueReceived = 0.0f;

		// Update is called once per frame
		void Update () {
			if (setDataOnFrame) { 	//skip value lowering
				setDataOnFrame = false;
			} else {
				latestValueReceived = Mathf.Lerp(latestValueReceived, 0.0f, 0.1f);
				SetAudioData(latestValueReceived, setDataOnFrame: false);
			}
		}

		public void SetAudioData(float centerHitNormalized, bool setDataOnFrame = true){
			this.setDataOnFrame = setDataOnFrame;
			if (centerHitNormalized == float.NaN) {
				Log.Error("PebbleManager", "Value for SetAudioData is NAN");
				centerHitNormalized = 0.0f;
			}
			latestValueReceived = centerHitNormalized;

			for (int i = pebbleRowList.Length - 1; i >= 0; i--) {
				float smoothnessBetweenRows = Mathf.Lerp(smoothnessLimitBetweenRows.y, smoothnessLimitBetweenRows.x, (float)i / pebbleRowList.Length);

				if (pebbleRowList [i].pebbleList != null) {
					for (int j = 0; j < pebbleRowList[i].pebbleList.Length; j++) {

						if(pebbleRowList[i].pebbleList[j] != null){

							if( i > 0){
							pebbleRowList[i].pebbleList[j].transform.localPosition = Vector3.Lerp(pebbleRowList[i].pebbleList[j].transform.localPosition, 
								                                                                  new Vector3(pebbleRowList[i].pebbleList[j].transform.localPosition.x, pebbleRowList[i-1].pebbleList[j].transform.localPosition.y, pebbleRowList[i].pebbleList[j].transform.localPosition.z),
								                                                                      smoothnessBetweenRows);
							}
							else{
								//Debug.Log("centerHitNormalized: " + centerHitNormalized);
							
							pebbleRowList[i].pebbleList[j].transform.localPosition = Vector3.Lerp(pebbleRowList[i].pebbleList[j].transform.localPosition, 
							                                                                      new Vector3(pebbleRowList[i].pebbleList[j].transform.localPosition.x, centerHitNormalized, pebbleRowList[i].pebbleList[j].transform.localPosition.z),
								                                                                      1.0f);
							}
						}
						else{
							Log.Error("PebbleManager", "pebbleRowList[{0}].pebbleList[{1}].pebbleList is null", i, j);
						}
					}
				}
				else{
					Log.Error("PebbleManager", "pebbleRowList[{0}].pebbleList is null", i);
				}
			}
		}
	}

}
