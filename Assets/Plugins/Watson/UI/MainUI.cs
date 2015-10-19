﻿using UnityEngine;
using System.Collections;
using IBM.Watson.Utilities;

public class MainUI : MonoBehaviour {

    public IEnumerator Start()
    {
        while(! Config.Instance.ConfigLoaded )
            yield return null;
    }

    public void OnUnitTests()
    {
        Application.LoadLevel( "UnitTests" );
    }

    public void OnTestSTT()
    {
        Application.LoadLevel( "TestSTT" );
    }

    public void OnTestTTS()
    {
        Application.LoadLevel( "TestTTS" );
    }

    public void OnTestNLC()
    {
        Application.LoadLevel( "TestNLC" );
    }

    public void OnXRAY()
    {
        Application.LoadLevel( "XRay" );
    }

    public void OnCubeTest()
    {
        Application.LoadLevel( "CubeTest" );
    }

    public void OnMain()
    {
        Application.LoadLevel( "Main" );
    }
}
