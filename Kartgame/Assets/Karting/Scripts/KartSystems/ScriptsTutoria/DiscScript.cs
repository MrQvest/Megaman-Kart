
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.KartSystems;

namespace ModScripts
{
    public class DiscScript : MonoBehaviour, IUseItem
    {
        private Rigidbody rdbd;
        private Collider col;

        ArcadeKart arcadeKart;

        Vector3 lastVelocity;

        void Start()
        {
            rdbd = GetComponent<Rigidbody>();
            col = GetComponent<Collider>();
            Invoke("Destroy", 6f);
            rdbd.velocity = transform.forward * 30f;
        }

        private void FixedUpdate()
        {
            lastVelocity = rdbd.velocity;
        }

        private void Destroy()
        {
            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

            rdbd.velocity = direction * Mathf.Max(speed, 30f);

            Collider colliderA = collision.collider;

            if (colliderA.CompareTag("Player"))
            {
                arcadeKart = colliderA.gameObject.GetComponentInParent<ArcadeKart>();
                //print("GameObject"+other.gameObject);
                //print("Script" + arcadeKart);
                if (arcadeKart != null)
                {
                    arcadeKart.baseStats.TopSpeed /= 0;
                    Invoke("Delete",0.02f);
                }
            }
        }
        void Delete()
        {
            arcadeKart.baseStats.TopSpeed= 35;
        }
        public void UseItem(Transform position, Quaternion rotation)
        {
            Instantiate(gameObject, position.position - (new Vector3(0f, 2f, -4f)), rotation);
        }
    }
}

