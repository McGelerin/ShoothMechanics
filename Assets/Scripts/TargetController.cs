using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    [Header("Target Prefab")]
    [SerializeField] private GameObject _targetPrefab;

    [Header("Matris Settings")]
    [SerializeField] int _Column = 4;
    [SerializeField] int _Row = 4;
    [Range(.5f,3)]
    [SerializeField] float _offset = 1;


    private string _targetTag = "ShootTarget";
    public int targetCount;

    private void Awake()
    {
        targetCount = _Column * _Row;
    }



    void Start()
    {
        StartCoroutine(CreateCubes());
    }

    IEnumerator CreateCubes()
    {
        GameObject target;
        float zOffset = _Column * _Row * _offset / 2;

        for (int i = 0; i < _Row; i++)
        {
            for (int j = 0; j < _Column; j++)
            {
                target = Instantiate(_targetPrefab, new Vector3(Camera.main.transform.position.x -(_offset *(_Column -1) /2f) +(j *_offset), 
                        Camera.main.transform.position.y -(_offset *(_Row - 1)) /2f +(i *_offset), 
                        Camera.main.transform.position.z + zOffset), 
                        Quaternion.identity);

                Color color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
                for (int a = 0; a < target.transform.childCount; a++)
                {
                    target.transform.GetChild(a).GetComponent<MeshRenderer>().material.color = color;
                    target.transform.GetChild(a).tag = _targetTag;
                }
                yield return new WaitForSeconds(0.02f);
            }
        }
    }

    public void DecreaseTargetCount()
    {
        targetCount -= 1;
        if (targetCount == 0)
        {
            gameManager.GameStatusCache = GameStatus.END;
        }
    }
}
