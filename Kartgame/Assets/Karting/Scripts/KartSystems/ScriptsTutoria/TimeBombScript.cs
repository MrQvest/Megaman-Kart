using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBombScript : MonoBehaviour, IUseItem
{
    Rigidbody rdbd;
    [SerializeField]
    private GameObject timeZone;

    [Range(0f,20f)]
    [SerializeField]
    private float throwForce;
    void Start()
    {
        rdbd= GetComponent<Rigidbody>();
        rdbd.AddForce(transform.up * throwForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(timeZone,transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void UseItem(Transform position, Quaternion rotation)
    {
        Instantiate(gameObject, position.position + new Vector3 (0f,3f,.5f), rotation);
    }
}
