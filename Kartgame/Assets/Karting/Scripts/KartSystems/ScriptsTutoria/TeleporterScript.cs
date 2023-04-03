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

        private Rigidbody kartRigidbody;
        private bool isTeleporting = false;

        private void Start()
        {
            kartRigidbody = GetComponent<Rigidbody>();
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
        if (col.CompareTag("Player") && col.gameObject != gameObject)
        {
            numValidColliders++;
        }
    }
    if (numValidColliders >= 2)
    {
        // Teleporta para um jogador aleatório
        List<Collider> validColliders = new List<Collider>();
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Player") && col.gameObject != gameObject)
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
            Vector3 currentVelocity = kartRigidbody.velocity;

            // Troca a posição do kart atual com o kart alvo
            Vector3 tempPosition = transform.position;
            transform.position = target.position;
            target.position = tempPosition;

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