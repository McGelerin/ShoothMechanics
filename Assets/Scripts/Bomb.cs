using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float speed, radius, explosionForce =50f;
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
                Collider[] colliders = Physics.OverlapSphere(other.transform.parent.position, radius);
                foreach (var nearBy in colliders)
                {
                    if (!nearBy.CompareTag("ShootTarget")) continue;
                    for (int j = 0; j < nearBy.transform.parent.childCount; j++)
                    {
                        nearBy.transform.parent.GetChild(j).tag = "Untagged";
                        nearBy.transform.parent.GetChild(j).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                        nearBy.transform.parent.GetChild(j).GetComponent<Rigidbody>().AddExplosionForce(explosionForce, nearBy.transform.position, radius);
                        nearBy.transform.parent.GetChild(j).GetComponent<MeshRenderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
                    }
                GameManager.Instance.TargetController.DecreaseTargetCount();
                }
            isMovement = false;
            transform.position = spawnPosition; 
        }
    }
}
