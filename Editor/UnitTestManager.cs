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
 * @author Richard Lyle (rolyle@us.ibm.com)
*/

using UnityEngine;
using UnityEditor;
using IBM.Watson.Logging;
using IBM.Watson.Utilities;
using IBM.Watson.UnitTests;
using System.Collections.Generic;
using System.Collections;
using System;

namespace IBM.Watson.Editor
{
    /// <summary>
    /// This class implements a UI using OnGUI and allows the user to run a UnitTest by clicking on a button. Additionally,
    /// it handles running any number of unit tests.
    /// </summary>
    public class UnitTestManager : MonoBehaviour
    {
        public delegate void OnTestsComplete();

        /// <summary>
        /// Maximum time in seconds a test can run before we consider it timed out.
        /// </summary>
        const float TEST_TIMEOUT = 300.0f;

        /// <summary>
        /// Returns the instance of the UnitTestManager.
        /// </summary>
        public static UnitTestManager Instance { get { return Singleton<UnitTestManager>.Instance; } }

        /// <summary>
        /// Number of tests that have failed.
        /// </summary>
        public int TestsFailed { get; private set; }
        /// <summary>
        /// Number of tests that have completed.
        /// </summary>
        public int TestsComplete { get; private set; }
        /// <summary>
        /// If true, then the editor will exit with an error code once the last test is completed.
        /// </summary>
        public bool QuitOnTestsComplete { get; set; }
        /// <summary>
        /// This callback is invoked when all queued tests have been completed.
        /// </summary>
        public OnTestsComplete OnTestCompleteCallback { get; set; }

        /// <summary>
        /// Queue test by Type to run.
        /// </summary>
        /// <param name="test">The type of the UnitTest to run.</param>
        /// <param name="run">If true, then the test co-routine will be started after queueing.</param>
        public void QueueTest(Type test, bool run = false)
        {
            m_QueuedTests.Enqueue(test);
            if (run)
                RunTests();
        }

        public void QueueTests(Type[] tests, bool run = false)
        {
            foreach (var t in tests)
                m_QueuedTests.Enqueue(t);
            if (run)
                RunTests();
        }

        /// <summary>
        /// Start running all queued tests.
        /// </summary>
        public void RunTests()
        {
            TestsFailed = TestsComplete = 0;
            // using Runnable, since we hook the editor update and handles those co-routines even if the editor isn't in play mode.
            Runnable.Run(RunTestsCR());
        }

        #region Private Data
        private Queue<Type> m_QueuedTests = new Queue<Type>();
        private Type[] m_TestsAvailable = null;
        private UnitTest m_ActiveTest = null;
        #endregion

        #region Private Functions
        private IEnumerator RunTestsCR()
        {
            while (m_QueuedTests.Count > 0)
            {
                Type testType = m_QueuedTests.Dequeue();

                m_ActiveTest = Activator.CreateInstance(testType) as UnitTest;
                if (m_ActiveTest != null)
                {
                    Log.Status("UnitTestManager", "STARTING UnitTest {0} ...", testType.Name);

                    // wait for the test to complete..
                    float fStartTime = Time.time;
                    IEnumerator e = m_ActiveTest.RunTest();
                    while (e.MoveNext())
                    {
                        yield return null;
                        if (Time.time > (fStartTime + TEST_TIMEOUT))
                        {
                            Log.Error("UnitTestManager", "UnitTest {0} has timed out.", testType.Name);
                            m_ActiveTest.TestFailed = true;
                            break;
                        }
                    }

                    if (m_ActiveTest.TestFailed)
                    {
                        Log.Error("UnitTestManager", "... UnitTest {0} FAILED.", testType.Name);
                        TestsFailed += 1;
                    }
                    else
                    {
                        Log.Status("UnitTestManager", "... UnitTest {0} COMPLETED.", testType.Name);
                        TestsComplete += 1;
                    }
                }
                else
                {
                    Log.Error("UnitTestManager", "Failed to instantiate test {0}.", testType.Name);
                    TestsFailed += 1;
                }

            }

            if (OnTestCompleteCallback != null)
                OnTestCompleteCallback();

            if (QuitOnTestsComplete)
            {
                Log.Status("UnitTestManager", "Exiting, Tests Completed: {0}, Tests Failed: {1}", TestsComplete, TestsFailed);
                EditorApplication.Exit(TestsFailed > 0 ? 1 : 0);
            }
        }

        private void OnGUI()
        {
            if (m_TestsAvailable == null)
                m_TestsAvailable = Utility.FindAllDerivedTypes(typeof(UnitTest));

            if (m_TestsAvailable != null)
            {
                foreach (var t in m_TestsAvailable)
                {
                    string sButtonLabel = "Run " + t.Name;
                    if (GUILayout.Button(sButtonLabel))
                    {
                        QueueTest(t, true);
                    }
                }
            }
        }
        #endregion
    }
}


/// <summary>
/// This static class is for menu items and invoking a function from the command line, since it doesn't like namespaces.
/// </summary>
public static class RunUnitTest
{
    /// <summary>
    /// Public functions invoked from the command line to run all UnitTest objects.
    /// (e.g. "C:\Program Files\Unity\Editor\Unity.exe" -quit -batchmode -executemethod UnitTestManager.RunAll -projectPath <pathname>)
    /// </summary>
    /// 
    static public void All()
    {
        Logger.InstallDefaultReactors();
        EditorApplication.update += UpdateRunnable;

        IBM.Watson.Editor.UnitTestManager instance = IBM.Watson.Editor.UnitTestManager.Instance;
        instance.QuitOnTestsComplete = true;
        instance.OnTestCompleteCallback = OnTestsComplete;
        instance.QueueTests(Utility.FindAllDerivedTypes(typeof(UnitTest)), true);
    }

    [MenuItem("Watson/Run All UnitTests")]
    static public void AllNoQuit()
    {
        Logger.InstallDefaultReactors();
        EditorApplication.update += UpdateRunnable;

        IBM.Watson.Editor.UnitTestManager instance = IBM.Watson.Editor.UnitTestManager.Instance;
        instance.OnTestCompleteCallback = OnTestsComplete;
        instance.QueueTests(Utility.FindAllDerivedTypes(typeof(UnitTest)), true);
    }

    static void UpdateRunnable()
    {
        Runnable.Instance.UpdateRoutines();
    }

    static void OnTestsComplete()
    {
        EditorApplication.update -= UpdateRunnable;
    }

}

