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

        List<Rigidbody> rdbdList = new List<Rigidbody>();
        Rigidbody[] rdbdsArray;

        int index = 0;

        private void Start()
        {
            // Percorre todos os objetos na cena que possuem um componente Rigidbody
            foreach (Rigidbody rdbds in FindObjectsOfType<Rigidbody>())
            { 
                // Verifica se o objeto possui a tag "Player"
                if (rdbds.CompareTag("Player"))
                {
                    // Se tiver, adiciona o Rigidbody do objeto à lista rdbdList
                    rdbdList.Add(rdbds);   
                }
            }

            /*FindObjectOfType<Rigidbody>().CompareTag("Player");
            teleporterTarget = GameObject.FindGameObjectWithTag("Player").transform;*/
        }

        private void Update()
        {
            // Se já estiver teleportando, não faz nada
            if (isTeleporting)
            {
                return;
            }

            // Verifica quantos jogadores estão próximos o suficiente para o teleporte
            Collider[] colliders = Physics.OverlapSphere(transform.position, maxRange); // Cria um array de colliders dos objetos que estão dentro do raio definido
            int numValidColliders = 0;
            foreach (Collider col in colliders)
            {
                if (col.CompareTag("Player") && col.gameObject != gameObject) // Se o collider tiver a tag "Player" e não for o objeto atual
                {
                    numValidColliders++; // Incrementa o número de colliders válidos para o teleporte
                }
            }

            if (numValidColliders >= 1) // se há pelo menos um jogador dentro do raio de detecção
            {
                // Teleporta para um jogador aleatório

                // Cria uma lista vazia para armazenar colliders válidos
                List<Collider> validColliders = new List<Collider>();

                // Percorre cada collider dentro da matriz colliders
                foreach (Collider col in colliders)
                {
                    // Verifica se o collider tem a tag "Player" e não é o objeto atual
                    if (col.CompareTag("Player") && col.gameObject != gameObject)
                    {
                        // Adiciona o collider à lista de colliders válidos
                        validColliders.Add(col);
                    }
                }

    // Seleciona aleatoriamente um collider da lista de colliders válidos
    Collider target = validColliders[Random.Range(0, validColliders.Count)];

    // Inicia a corrotina Teleport para teleportar o objeto atual para a posição do collider selecionado
    StartCoroutine(Teleport(target.transform));
}

        }


        private IEnumerator Teleport(Transform target)
        {
            isTeleporting = true;

            // Aguarda o tempo de carregamento
            yield return new WaitForSeconds(teleportTime);

            // Obtém a velocidade do kart atual
            Vector3 currentVelocity = kartRigidbody.velocity;

            // Troca a posição do kart atual com o kart alvo
            Vector3 tempPosition = transform.position;      // Corrigir
            transform.position = target.position;           // Corrigir
            target.position = tempPosition;                 // Corrigir

            // Mantém a velocidade do kart atual
            kartRigidbody.velocity = currentVelocity;

            isTeleporting = false;
        }

        public void UseItem(Transform position, Quaternion rotation)
        {
            Instantiate(gameObject, position.position - (new Vector3(0f, 2f, 0f)), rotation);

        }
    }
}