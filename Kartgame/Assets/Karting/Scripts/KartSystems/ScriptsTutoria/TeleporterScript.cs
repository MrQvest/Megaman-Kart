using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModScripts
{
    public class TeleporterScript : MonoBehaviour, IUseItem
    {
        public float maxRange = 5f;

        [SerializeField]
        private GameObject playerObject;
        private Collider[] colliders;
        private GameObject target;
        private float distancia;

        public void UseItem(Transform position, Quaternion rotation)
        {
            playerObject = position.gameObject;
            Instantiate(gameObject, position.position, rotation);
            CheckCollider();
        }

        void CheckCollider()
        {
            int layerMask = (1 << 15) | (1 << 16);
            colliders = Physics.OverlapSphere(transform.position, maxRange, layerMask);

            // Calcular distância entre colliders[]
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.layer != playerObject.layer)
                {
                    // Calcula a distância entre o objeto que contém o script e o objeto do collider
                    distancia = Vector3.Distance(playerObject.transform.position, collider.transform.position);
                    if (distancia > maxRange)
                    {
                        break;
                    }
                    else
                    {
                        target = collider.gameObject;                        
                        Teleport(target, transform.position);
                    }
                }
            }
        }                  

        void Teleport(GameObject target, Vector3 tempPosition)
        {
            Debug.Log(target.name);
            float elevation = 4f;
            // Obtém a velocidade do kart atual e do kart alvo
            Vector3 currentVelocity = playerObject.GetComponent<Rigidbody>().velocity;
            Vector3 targetVelocity = target.GetComponentInParent<Rigidbody>().velocity;

            // Troca a posição do kart atual com o kart alvo
           // Vector3 tempPosition = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y + elevation, playerObject.transform.position.z);           
            playerObject.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + elevation, target.transform.position.z);
            Debug.Break();
            target.transform.position = tempPosition + new Vector3(0, elevation, 0);
            
            // Mantém a velocidade dos karts 
            playerObject.GetComponent<Rigidbody>().velocity = currentVelocity;
            target.GetComponentInParent<Rigidbody>().velocity = targetVelocity;
        }
    }
}
