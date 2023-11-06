using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoApplySettings : MonoBehaviour
{
    void Start()
    {
        settingsManager.instance.ApplySettings();
        Debug.Log("Settings have been applied");
    }

    void Update()
    {
        
    }
}
