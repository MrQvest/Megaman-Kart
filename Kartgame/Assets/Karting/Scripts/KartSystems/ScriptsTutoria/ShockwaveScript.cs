using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModScripts
{
    public class ShockwaveScript : MonoBehaviour, IUseItem
    {
        public float force = 10f;
        public float radius = 10f;
        public float lifespan = 1f;
        public AnimationCurve animationCurve;
        public Material material;

        private float startTime;

        void Start()
        {
            startTime = Time.time;
            GetComponent<ParticleSystem>().Play();
        }

        void Update()
        {
            float age = Time.time - startTime;
            float progress = age / lifespan;
            float scale = animationCurve.Evaluate(progress);

            transform.localScale = new Vector3(scale, scale, scale);

            if (age >= lifespan)
            {
                Destroy(gameObject);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                Vector3 direction = (other.transform.position - transform.position).normalized;
                float distance = Vector3.Distance(other.transform.position, transform.position);

                float effect = Mathf.Clamp01(1f - (distance / radius));

                rb.AddForce(direction * force * effect, ForceMode.Impulse);
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        void OnRenderObject()
        {
            if (material != null)
            {
                material.SetPass(0);
                Graphics.DrawProceduralNow(MeshTopology.Triangles, 3, 1);
            }
        }

        public void UseItem(Transform position, Quaternion rotation)
        {
            Instantiate(gameObject, position.position + (new Vector3(0f, 1.25f, 0f)), rotation);
        }
    }
}