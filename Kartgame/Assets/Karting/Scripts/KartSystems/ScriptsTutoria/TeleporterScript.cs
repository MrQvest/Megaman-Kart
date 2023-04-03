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
        private GameObject player0Object;
        
        List<Rigidbody> rdbdList = new List<Rigidbody>();
        Rigidbody[] rdbdsArray;

        int index = 0;

        private void Awake()
        {
            player0Object = GameObject.FindGameObjectWithTag("Player0");
            Collider player0Collider = player0Object.GetComponent<Collider>();
        }

        private void Start()
        {
            foreach (Rigidbody rdbds in FindObjectsOfType<Rigidbody>())
            { 

                if (rdbds.CompareTag("Player"))
                {
                    rdbdList.Add(rdbds);
                    
                }
            }
            

            /*FindObjectOfType<Rigidbody>().CompareTag("Player");
            teleporterTarget = GameObject.FindGameObjectWithTag("Player").transform;*/
        }

        private void Update()
        {
            if (isTeleporting)
            {
                return;
            }

            // Verifica se há pelo menos dois jogadores próximos o suficiente para teleporte
            Collider[] colliders = Physics.OverlapSphere(transform.position, maxRange);
            int numValidColliders = 0;
            foreach (Collider col in colliders)
            {
                if (col.CompareTag("Player") && col.gameObject != player0Object.GetComponent<Collider>())
                {
                    numValidColliders++;
                }
            }
            if (numValidColliders >= 1)
            {
                // Teleporta para um jogador aleatório
                List<Collider> validColliders = new List<Collider>();
                foreach (Collider col in colliders)
                {
                    if (col.CompareTag("Player") && col.gameObject != player0Object.GetComponent<Collider>())
                    {
                        validColliders.Add(col);
                    }
                }
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
            Vector3 currentVelocity = player0Object.GetComponent<Rigidbody>().velocity;

            // Troca a posição do kart atual com o kart alvo
            Vector3 tempPosition = player0Object.transform.position;      // Corrigir
            player0Object.transform.position = target.position;           // Corrigir
            target.position = tempPosition;                 // Corrigir

            // Mantém a velocidade do kart atual
            player0Object.GetComponent<Rigidbody>().velocity = currentVelocity;

            isTeleporting = false;
        }

        public void UseItem(Transform position, Quaternion rotation)
        {
            Instantiate(gameObject, position.position - (new Vector3(0f, 2f, 0f)), rotation);

        }
    }
}