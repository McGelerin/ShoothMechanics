using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    [Header("Bullet-Bomb Scriptable Objects")]
    [SerializeField]BulletScriptable bulletScriptable;
    [SerializeField]BombScriptable bombScriptable;

    [Header("Fire Rate")]
    [Range(0.05f, 2f)]
    [SerializeField] float fireRefreshRate = 0.25f;

    [Header("Spawn Points")]
    [SerializeField] Transform poolSpawnPosition;

    //Private Variables
    GameObject _bulletPrefab, _bombPrefab;
    float _bulletSpeed, _bombSpeed, _radius;
    List<GameObject> _bullets = new List<GameObject>();
    List<GameObject> _bombs = new List<GameObject>();
    int _currentBullet = 0, _currentBomb = 0, _poolSize = 10;
    bool _isShoot = true;
    RaycastHit _hit;

    private void Awake()
    {
        BulletScriptable();
        BombScriptable();
    }

    void Start()
    {
        BulletPool();
        BombPool();
    }

    void Update()
    {
        MouseLeftButton();
        MouseRightButton();
    }

    void BulletScriptable()
    {
        _bulletPrefab = bulletScriptable.BulletPrefab;
        _bulletSpeed = bulletScriptable.Speed;
    }

    void BombScriptable()
    {
        _bombPrefab = bombScriptable.BombPrefab;
        _bombSpeed = bombScriptable.Speed;
        _radius = bombScriptable.Radius;
    }

    void BulletPool()
    {
        GameObject poolObject = new GameObject("BulletPool");
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject cloneBullet = Instantiate(_bulletPrefab, poolSpawnPosition.position, Quaternion.identity);
            cloneBullet.GetComponent<Bullet>().Speed = _bulletSpeed;
            cloneBullet.transform.parent = poolObject.transform;
            _bullets.Add(cloneBullet);
        }
    }

    void BombPool()
    {
        GameObject poolObject = new GameObject("BombPool");
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject cloneBomb = Instantiate(_bombPrefab, poolSpawnPosition.position, Quaternion.identity);
            cloneBomb.GetComponent<Bomb>().speed = _bombSpeed;
            cloneBomb.GetComponent<Bomb>().radius = _radius;
            cloneBomb.transform.parent = poolObject.transform;
            _bombs.Add(cloneBomb);
        }
    }

    void MouseLeftButton()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.GameStatusCache == GameStatus.START)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _hit, 100f))
            {
                if (_hit.transform.CompareTag("ShootTarget") && _isShoot)
                {
                    BulletShot();
                }
            }
        }
    }

    void MouseRightButton()
    {
        if (Input.GetMouseButtonDown(1) && GameManager.GameStatusCache == GameStatus.START)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _hit, 100f))
            {
                if (_hit.transform.CompareTag("ShootTarget") && _isShoot)
                {
                    BombShot();
                }
            }
        }
    }

    void BulletShot()
    {
        _isShoot = false;

        _bullets[_currentBullet].transform.position = poolSpawnPosition.position;

        Vector3 Direction = _hit.point - poolSpawnPosition.position;
        _bullets[_currentBullet].transform.LookAt(_hit.point);
        _bullets[_currentBullet].GetComponent<Bullet>().IsMovement = true;
        _currentBullet++;
        if (_currentBullet >= _poolSize)
        {
            _currentBullet = 0;
        }
        StartCoroutine("isShotChangeState");
    }

    void BombShot()
    {
        _isShoot = false;

        _bombs[_currentBomb].transform.position = poolSpawnPosition.position;

        Vector3 Direction = _hit.point - poolSpawnPosition.position;
        _bombs[_currentBomb].transform.LookAt(_hit.point);
        _bombs[_currentBomb].GetComponent<Bomb>().isMovement = true;
        _currentBomb++;
        if (_currentBomb >= _poolSize)
        {
            _currentBomb = 0;
        }
        StartCoroutine("isShotChangeState");
    }

    IEnumerator isShotChangeState()
    {
        yield return new WaitForSeconds(fireRefreshRate);
        _isShoot = true;
    }
}
