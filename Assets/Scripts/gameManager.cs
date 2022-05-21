using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{

    public static GameStatus GameStatusCache;
    public TargetController TargetController;
    [Header("FPS Settings")]
    [SerializeField] private FPSMode _fpsMode;

    public static gameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            TargetController = GetComponent<TargetController>();
        }
    }

    private void Start()
    {
        GameStatusCache = GameStatus.START;
        #if UNITY_EDITOR
                Application.targetFrameRate = (int)_fpsMode;
        #endif  
    }

}
public enum GameStatus
{
    START,
    END
}

public enum FPSMode
{
    LowPerformance = 10,
    Default = 60
}