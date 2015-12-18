﻿using UnityEngine;
using System.Collections;

namespace TouchScript.Examples.Portal 
{
	public class Rotator : MonoBehaviour 
	{
		public float RotationSpeed = 1f;

		void Update () 
		{
			transform.rotation *= Quaternion.Euler(0, 0, Time.deltaTime * RotationSpeed);
		}
	}
}