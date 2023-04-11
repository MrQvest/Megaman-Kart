using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModScripts
{
    public class TeleporterScript : MonoBehaviour, IUseItem
    {
        public float maxRange = 5f;
        public float teleportTime = 2f;
        public GameObject teleportEffect;

        private Transform teleporterTarget;
        private bool isTeleporting = false;
        private GameObject playerObject;
        private GameObject currentPlayer;
        private GameObject[] playerList;

        // Lista de Rigidbody chamada rdbdList
        List<Rigidbody> rdbdList = new List<Rigidbody>();
        Rigidbody[] rdbdsArray;

        int index = 0;

        private void Awake()
        {
            currentPlayer = GameObject.FindGameObjectWithTag("Player");
            playerObject = GameObject.FindGameObjectWithTag("Player");
            playerList = GameObject.FindGameObjectsWithTag("Player");
        }

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
        }

        private void Update()
        {
            if (isTeleporting)
            {
                return;
            }

            // Verifica se há um jogador próximo o suficiente para teleporte
            Collider[] colliders = Physics.OverlapSphere(transform.position, maxRange);
            int numValidColliders = 0;
            foreach (Collider col in colliders)
            {
                // Se os colliders tiverem a Tag Player
                if (col.CompareTag("Player"))
                {
                    // Os adiciona à numValidColliders
                    numValidColliders++;
                }
            }
            if (numValidColliders >= 1)
            {
                // Teleporta para o próximo jogador
                List<Collider> validColliders = new List<Collider>();
                foreach (Collider col in colliders)
                {
                    if (col.CompareTag("Player"))   // Corrigir para que o script reconheça o jogador atual, e não o adicione à lista validColliders
                    {                               //
                        validColliders.Add(col);    //
                    }                               //
                }

                // Teleporta para o próximo jogador, sem aleatoriedade
                Collider target = validColliders[Random.Range(0, validColliders.Count)];
                StartCoroutine(Teleport(target.transform));
            }
        }


        private IEnumerator Teleport(Transform target)
        {
            isTeleporting = true;

            // Aguarda o tempo de carregamento
            yield return new WaitForSeconds(teleportTime);

            // Obtém a velocidade do kart atual
            Vector3 currentVelocity = playerObject.GetComponent<Rigidbody>().velocity;

            // Troca a posição do kart atual com o kart alvo
            Vector3 tempPosition = playerObject.transform.position;      // Corrigir
            playerObject.transform.position = target.position;           // Corrigir
            target.position = tempPosition;                 // Corrigir

            // Mantém a velocidade do kart atual
            playerObject.GetComponent<Rigidbody>().velocity = currentVelocity;

            isTeleporting = false;
        }

        public void UseItem(Transform position, Quaternion rotation)
        {
            Instantiate(gameObject, position.position - (new Vector3(0f, 2f, 0f)), rotation);

        }
    }
}