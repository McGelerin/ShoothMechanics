using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed;
    public bool IsMovement = false;
    Vector3 _spawnPosition;

    void Start()
    {
        _spawnPosition = transform.position;
    }
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (IsMovement)
        {
            transform.Translate(transform.forward * Time.deltaTime * Speed, Space.World);
        }
    }

    //    private void OnCollisionEnter(Collision other)
    //    {
    //        if (other.gameObject.CompareTag("ShootTarget"))
    //        {
    //            Debug.Log("Çarptý");
    //            GameObject _brokenTarget = GameManager.Instance.TargetController.BrokenTargetList[GameManager.Instance.TargetController.CurrentBrokenTarget];
    //            _brokenTarget.transform.position = other.transform.position;
    //            Destroy(other.gameObject);
    //            for (int i = 0; i < _brokenTarget.transform.parent.childCount; i++)
    //            {
    ////                other.transform.parent.GetChild(i).tag = "Untagged";
    //                other.transform.parent.GetChild(i).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    //                other.transform.parent.GetChild(i).GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(500, 1000f));
    ////                other.transform.parent.GetChild(i).GetComponent<MeshRenderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f); ;
    //            }
    //            isMovement = false;
    //            transform.position = spawnPosition;
    //            GameManager.Instance.TargetController.DecreaseTargetCount();
    //        }
    //    }
    //private void OnTriggerEnter(Collider other)

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Çarptý");
        GameObject _brokenTarget = GameManager.Instance.TargetController.BrokenTargetList[GameManager.Instance.TargetController.CurrentBrokenTarget % GameManager.Instance.TargetController.BrokenTargetList.Count];
        _brokenTarget.transform.position = other.transform.position;
        Destroy(other.gameObject);
        for (int i = 0; i < _brokenTarget.transform.childCount; i++)
        {
            //                other.transform.parent.GetChild(i).tag = "Untagged";
            _brokenTarget.transform.GetChild(i).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            _brokenTarget.transform.GetChild(i).GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(500, 1000f));
            //                other.transform.parent.GetChild(i).GetComponent<MeshRenderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f); ;
        }
        GameManager.Instance.TargetController.CurrentBrokenTarget++;
        StartCoroutine(BrokenTargetSetActive(_brokenTarget));
        IsMovement = false;
        transform.position = _spawnPosition;
        GameManager.Instance.TargetController.DecreaseTargetCount();
    }

    IEnumerator BrokenTargetSetActive(GameObject brokenTarget)
    {
        Debug.Log("cartotine");
        yield return new WaitForSeconds(2f);
        //brokenTarget.SetActive(false)
        //brokenTarget.SetActive(false);
        brokenTarget.transform.position = GameManager.Instance.TargetController.BrokenTargetSpawner.transform.position;
        for (int i = 0; i < brokenTarget.transform.childCount; i++)
        {
            Debug.Log("carotine for");
            brokenTarget.transform.GetChild(i).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            brokenTarget.transform.GetChild(i).transform.rotation = Quaternion.identity;
            brokenTarget.transform.GetChild(i).transform.position = GameManager.Instance.TargetController.BrokenTargetListCache[i];
        }
    }
}
