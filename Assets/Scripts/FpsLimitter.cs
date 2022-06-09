using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FpsLimitter : MonoBehaviour
{
    [Header("FPS Settings")]
    [SerializeField] private FPSMode _fpsMode;

    private void Start()
    {
        #if UNITY_EDITOR
        Application.targetFrameRate = (int)_fpsMode;
        #endif
    }
}
public enum FPSMode
{
    LowPerformance = 10,
    Default = 60,
    MaxPerformance = 120
}
