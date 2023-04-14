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

        private bool isTeleporting = false;
        private GameObject playerObject;
        //private Transform position;

        // Lista de Rigidbody chamada rdbdList
        List<Rigidbody> rdbdList = new List<Rigidbody>();

        private void Start()
        {

            // Procura por todos os objetos contendo Rigidbody
            foreach (Rigidbody rdbds in FindObjectsOfType<Rigidbody>())
            {
                // Verifica se eles têm a Tag Player e os adiciona à lista rdbdList
                if (rdbds.CompareTag("Player"))
                {
                    rdbdList.Add(rdbds);
                }
            }
            //print(rdbdList.Count);
        }

        private void Update()
        {
            /*if (isTeleporting)
            {
                return;
            }*/

            
        }


        private IEnumerator Teleport(Transform target)
        {
            isTeleporting = true;

            // Aguarda o tempo de carregamento
            yield return new WaitForSeconds(teleportTime);

            // Obtém a velocidade do kart atual
            Vector3 currentVelocity = playerObject.GetComponent<Rigidbody>().velocity;

            // Troca a posição do kart atual com o kart alvo
            Vector3 tempPosition = playerObject.transform.position;
            playerObject.transform.position = target.position;
            target.position = tempPosition;

            // Mantém a velocidade do kart atual
            playerObject.GetComponent<Rigidbody>().velocity = currentVelocity;

            isTeleporting = false;
        }

        public void UseItem(Transform position, Quaternion rotation)
        {
            playerObject = position.gameObject;
            Instantiate(gameObject, position.position - (new Vector3(0f, 2f, 0f)), rotation);
            //print(position.gameObject.name); //Essa linha da print no nome do objeto que usou o item
            print(playerObject.name);
            CheckTP();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(transform.position, maxRange);
        }

        void CheckTP()
        {
            // Verifica se há um jogador próximo o suficiente para teleporte
            Collider[] colliders = Physics.OverlapSphere(transform.position, maxRange);
            int numValidColliders = 0;
            foreach (Collider col in colliders)
            {
                print(playerObject);
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
            if (numValidColliders >= 1)
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


                Collider target = validColliders[0];
                StartCoroutine(Teleport(target.transform));
            }
        }
    }

  
}