using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    [Header("Target Scriptable Object")]
    [SerializeField] TargetScriptable targetScriptable;
    public GameObject BrokenTargetSpawner;

    private GameObject _targetPrefab;
    private GameObject _brokentargetPrefab;
    int _column, _row, _coefficient;
    float _offset;
    private string _targetTag = "ShootTarget";
    [HideInInspector]public int TargetCount,CurrentBrokenTarget;
    public List<GameObject> BrokenTargetList = new List<GameObject>();
    public List<Vector3> BrokenTargetListCache = new List<Vector3>();

    private void Awake()
    {
        CurrentBrokenTarget = 0;
        TargetScriptable();
        TargetCount = _column * _row;
    }

    void Start()
    {
        GameManager.Instance.TargetController = this;
        StartCoroutine(CreateTargets());
        BrokenTargetCreate(BrokenTargetSpawner.transform,5);
    }

    void TargetScriptable()
    {
        _targetPrefab = targetScriptable.TargetPrefab;
        _brokentargetPrefab = targetScriptable.BrokenTargetPrefab;
        _column = targetScriptable.Column;
        _row = targetScriptable.Row;
        _offset = targetScriptable.Offset;
        _coefficient = targetScriptable.Coefficient;
    }

    IEnumerator CreateTargets()
    {
        GameObject _target;
        float _zOffset = _column * _row * _offset / _coefficient;

        for (int i = 0; i < _row; i++)
        {
            for (int j = 0; j < _column; j++)
            {
                _target = Instantiate(_targetPrefab, new Vector3(Camera.main.transform.position.x -(_offset *(_column -1) /2f) +(j *_offset), 
                        Camera.main.transform.position.y -(_offset *(_row - 1)) /2f +(i *_offset), 
                        Camera.main.transform.position.z + _zOffset), 
                        Quaternion.identity);

                Color color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
                _target.transform.GetComponent<MeshRenderer>().material.color = color;
                _target.tag = _targetTag;
                #region MyRegion
                //for (int a = 0; a < _target.transform.childCount; a++)
                //{
                //    _target.transform.GetChild(a).GetComponent<MeshRenderer>().material.color = color;
                //    _target.transform.GetChild(a).tag = _targetTag;
                //} 
                #endregion
                yield return new WaitForSeconds(0.02f);
            }
        }
    }

    void BrokenTargetCreate(Transform brokenTargetSpawnPoint ,int brokenTargeetCount)
    {
        for (int i = 0; i < brokenTargeetCount; i++)
        {
            GameObject _brokenTarget = Instantiate(_brokentargetPrefab, brokenTargetSpawnPoint);
            for (int a = 0; a < _brokenTarget.transform.childCount; a++)
            {
                _brokenTarget.transform.GetChild(a).GetComponent<MeshRenderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            }
            BrokenTargetList.Add(_brokenTarget);
        }
        BrokenTargetListCache = FirstBrokenTargetCache(BrokenTargetList[0]);
    }

    List<Vector3> FirstBrokenTargetCache(GameObject brokenTarget)
    {
        List<Vector3> _brokenTargtTransform = new List<Vector3>();
        Debug.Log(brokenTarget.transform.childCount);
        for (int i = 0; i < brokenTarget.transform.childCount; i++)
        {
            Debug.Log(i);
            _brokenTargtTransform.Add(brokenTarget.transform.GetChild(i).transform.position);
        }
        return _brokenTargtTransform;
    }

    public void DecreaseTargetCount()
    {
        TargetCount -= 1;
        if (TargetCount == 0)
        {
            GameManager.GameStatusCache = GameStatus.END;
        }
    }
}
