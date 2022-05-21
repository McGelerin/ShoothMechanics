using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public bool isMovement = false;
    Vector3 spawnPosition;

    void Start()
    {
        spawnPosition = transform.position;
    }
    void Update()
    {
        if (isMovement)
        {
            transform.Translate(transform.forward * Time.deltaTime * speed, Space.World);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("ShootTarget"))
        {

            for (int i = 0; i < other.transform.parent.childCount; i++)
            {
                other.transform.parent.GetChild(i).tag = "Untagged";
                other.transform.parent.GetChild(i).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                other.transform.parent.GetChild(i).GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(500, 1000f));
                other.transform.parent.GetChild(i).GetComponent<MeshRenderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f); ;
            }
            isMovement = false;
            transform.position = spawnPosition;
            gameManager.Instance.TargetController.DecreaseTargetCount();
        }
    }
}
