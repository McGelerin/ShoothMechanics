using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    [Header("Bullet Types")]
    [SerializeField] GameObject Bullet;
    [SerializeField] GameObject Bomb;

    [Header("Spawn Points")]
    [SerializeField] Transform poolSpawnPosition;

    [Header("Bullet Settings")]
    [Range(10f, 50f)]
    [SerializeField] float _BulletSpeed = 20f;


    [Header("Bomb Settings")]
    [Range(10f, 50f)]
    [SerializeField] float _BombSpeed = 20f;
    [Range(0.5f,2f)]
    [SerializeField] private float _radius;

    [Header("Variables")]
    [Range(0.05f, 2f)]
    [SerializeField] float _FireRefreshRate = 0.25f;
    List<GameObject> Bullets = new List<GameObject>();
    List<GameObject> Bombs = new List<GameObject>();
    int currentBullet = 0;
    int currentBomb = 0;
    int poolSize = 10;
    bool isShoot = true;
    RaycastHit hit;

    void Start()
    {
        GameObject poolObject = new GameObject("Pool");
        for (int i = 0; i < poolSize; i++)
        {
            GameObject cloneBullet = Instantiate(Bullet, poolSpawnPosition.position, Quaternion.identity);
            GameObject cloneBomb = Instantiate(Bomb, poolSpawnPosition.position, Quaternion.identity);
            
            Bullets.Add(cloneBullet);
            Bombs.Add(cloneBomb);

            cloneBullet.GetComponent<Bullet>().speed = _BulletSpeed;
            cloneBomb.GetComponent<Bomb>().speed = _BombSpeed;
            cloneBomb.GetComponent<Bomb>().radius = _radius;

            cloneBullet.transform.parent = poolObject.transform;
            cloneBomb.transform.parent = poolObject.transform;
        }
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && gameManager.GameStatusCache == GameStatus.START)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100f))
            {
                if (hit.transform.CompareTag("ShootTarget") && isShoot)
                {
                    BulletShot();
                }
            }
        }

        if (Input.GetMouseButtonDown(1) && gameManager.GameStatusCache == GameStatus.START)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100f))
            {
                if (hit.transform.CompareTag("ShootTarget") && isShoot)
                {
                    BombShot();
                }
            }
        }
    }

    void BulletShot()
    {
        isShoot = false;

        Bullets[currentBullet].transform.position = poolSpawnPosition.position;

        Vector3 Direction = hit.point - poolSpawnPosition.position;
        Bullets[currentBullet].transform.LookAt(hit.point);
        Bullets[currentBullet].GetComponent<Bullet>().isMovement = true;
        currentBullet++;
        if (currentBullet >= poolSize)
        {
            currentBullet = 0;
        }
        StartCoroutine("isShotChangeState");
    }

    void BombShot()
    {
        isShoot = false;

        Bombs[currentBomb].transform.position = poolSpawnPosition.position;

        Vector3 Direction = hit.point - poolSpawnPosition.position;
        Bombs[currentBomb].transform.LookAt(hit.point);
        Bombs[currentBomb].GetComponent<Bomb>().isMovement = true;
        currentBomb++;
        if (currentBomb >= poolSize)
        {
            currentBomb = 0;
        }
        StartCoroutine("isShotChangeState");
    }

    IEnumerator isShotChangeState()
    {
        yield return new WaitForSeconds(_FireRefreshRate);
        isShoot = true;
    }
}
