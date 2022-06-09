using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameStatus GameStatusCache;
    public TargetController TargetController;

    public static GameManager Instance { get; private set; }
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
    }

}
public enum GameStatus
{
    START,
    END
}