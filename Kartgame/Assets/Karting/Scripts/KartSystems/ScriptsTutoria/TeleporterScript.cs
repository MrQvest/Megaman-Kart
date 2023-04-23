using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModScripts
{
    public class TeleporterScript : MonoBehaviour, IUseItem
    {
        public float maxRange = 5f;
        public float teleportTime = 2f;

        [SerializeField]
        private LayerMask playerALayer;
        [SerializeField]
        private LayerMask playerBLayer;
        private GameObject playerObject;
        private int numValidColliders = 0;
        private Collider[] colliders;

        public void UseItem(Transform position, Quaternion rotation)
        {
            playerObject = position.gameObject;
            Instantiate(gameObject, position.position, rotation);
            //print(playerObject.name);
            CheckTP();
        }

        void CheckTP()
        {
            CheckCollider();

            if (numValidColliders >= 1)
            {
                Teleport();
            }
            else 
            {
                return;
            }
        }



        void CheckCollider()
        {
            // Verifica se há um jogador próximo o suficiente para teleporte
            colliders = Physics.OverlapSphere(transform.position, maxRange);
            numValidColliders = 0;
            foreach (Collider col in colliders)
            {
                //print(playerObject);
                //print(col.gameObject);
                // Se os colliders estiverem nas layers PlayerA ou PlayerB e não forem o jogador atual
                if (col.gameObject.layer != playerObject.layer)
                {
                    if (col.gameObject.layer == 15 || col.gameObject.layer == 16)
                    {
                        print(col.gameObject.layer);
                        // Os adiciona à numValidColliders
                        numValidColliders++;
                    }
                }
            }
        }

        void Teleport()
        {
            // Teleporta para o próximo jogador
            List<Collider> validColliders = new List<Collider>();
            foreach (Collider col in colliders)
            {
                if ((col.gameObject.layer == LayerMask.NameToLayer("PlayerA") || col.gameObject.layer == LayerMask.NameToLayer("PlayerB")) && col.gameObject != playerObject)
                {
                    validColliders.Add(col);
                }
            }
            Collider target = validColliders[1];
            //Debug.Break();
            //print("Active? " + gameObject.activeSelf);
            print(target);
            Teleport1(target);
        }

        private void Teleport1(Collider target)
        {
            float elevation = 10f;
            // Obtém a velocidade do kart atual e do kart alvo
            Vector3 currentVelocity = playerObject.GetComponent<Rigidbody>().velocity;
            Vector3 targetVelocity = target.GetComponent<Rigidbody>().velocity;
            // Troca a posição do kart atual com o kart alvo
            Vector3 tempPosition = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y + elevation, playerObject.transform.position.z);

            playerObject.transform.position = new Vector3(target.gameObject.transform.position.x, target.gameObject.transform.position.y + elevation, target.gameObject.transform.position.z);
            target.gameObject.transform.position = tempPosition;

            // Mantém a velocidade dos karts 
            playerObject.GetComponent<Rigidbody>().velocity = currentVelocity;
            target.GetComponent<Rigidbody>().velocity = targetVelocity;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(transform.position, maxRange);
        }
    }  
}