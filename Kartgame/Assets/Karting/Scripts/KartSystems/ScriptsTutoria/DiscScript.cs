
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModScripts
{
    public class DiscScript : MonoBehaviour, IUseItem
    {

        private Rigidbody rdbd;
        private Collider col;

       // ArcadeKart arcadeKart;

        Vector3 lastVelocity;
        void Start()
        {
            rdbd = GetComponent<Rigidbody>();
            col = GetComponent<Collider>();
            Invoke("Destroy", 4f);
            rdbd.velocity = transform.forward * 30f;
        }
        private void FixedUpdate()
        {
            lastVelocity = rdbd.velocity;
        }
        // Update is called once per frame

        private void Destroy()
        {
            Destroy(gameObject);
           // arcadeKart.itemUsed = false;
        }
        private void OnCollisionEnter(Collision collision)
        {
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

            rdbd.velocity = direction * Mathf.Max(speed, 30f);
        }

        public void UseItem(Transform position, Quaternion rotation)
        {
            Instantiate(gameObject, position.position - (new Vector3(0f, 2f, 0f)), rotation);

        }

    }
}

