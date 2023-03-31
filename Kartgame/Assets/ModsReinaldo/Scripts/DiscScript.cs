using KartGame.KartSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class DiscScript : MonoBehaviour
    {
        private Rigidbody rdbd;
        private Collider col;

        ArcadeKart arcadeKart;

    Vector3 lastVelocity;
        void Start()
        {
            rdbd = GetComponent<Rigidbody>();
            col = GetComponent<Collider>();
            Invoke("Destroy", 4f);
            rdbd.velocity = Vector3.forward * 30f;
            arcadeKart = FindObjectOfType<ArcadeKart>();
        }
        private void FixedUpdate()
        {
        lastVelocity = rdbd.velocity;
        }
        // Update is called once per frame

        private void Destroy()
        {
            Destroy(gameObject);
            arcadeKart.itemUsed = false;
        }
    private void OnCollisionEnter(Collision collision)
    {
        var speed = lastVelocity.magnitude;
        var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

        rdbd.velocity = direction * Mathf.Max(speed, 30f);
    }

}

