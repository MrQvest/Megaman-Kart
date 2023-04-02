using KartGame.KartSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ModScripts
{
    public class ConeScript : MonoBehaviour, IUseItem
    {
        private Collider col;
        private Rigidbody rdbd;
        [Range(0,50f)]
        [SerializeField] private float throwForce;

        void Start()
        {
            col = GetComponent<Collider>();
            rdbd = GetComponent<Rigidbody>();

            print("Interface"+gameObject.GetComponent<IUseItem>());

            rdbd.AddForce(transform.forward * throwForce, ForceMode.Impulse);
            

        }

        public void UseItem(Transform position, Quaternion rotation)
        {
            Instantiate(gameObject, position.position + (new Vector3(0f, 2f, 0f)), rotation);
        }

        private void OnCollisionEnter(Collision collision)
        {
            rdbd.isKinematic = true;
            Invoke("Destroy", 15f);
            /*if (collision.gameObject.CompareTag("Player"))
            {
                Invoke("Destroy", 2f);
            }*/
        }
    }
}
