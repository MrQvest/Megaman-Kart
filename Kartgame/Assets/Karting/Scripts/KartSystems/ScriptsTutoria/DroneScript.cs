using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModScripts
{
    public class DroneScript : MonoBehaviour, IUseItem
    {
        public float moveSpeed = 10f;
        public float rotateSpeed = 100f;
        public float trackingRange = 20f;
        public float explosionRadius = 5f;
        public GameObject explosionEffect;

        private Transform target;

        private void Start()
        {
            // Encontra o jogador e define como alvo do casco azul
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Update()
        {
            // Verifica se o alvo está dentro do raio de perseguição
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance <= trackingRange)
            {
                // Rotaciona o casco azul em direção ao alvo
                Vector3 targetDirection = target.position - transform.position;
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotateSpeed * Time.deltaTime, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDirection);

                // Move o casco azul em direção ao alvo
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

                // Se o casco azul atingir o jogador, explode e causa dano
                if (distance <= explosionRadius)
                {
                    Instantiate(explosionEffect, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                    // Causa dano ao jogador aqui
                }
            }
        }
        public void UseItem(Transform position, Quaternion rotation)
        {
            Instantiate(gameObject, position.position - (new Vector3(0f, 2f, 0f)), rotation);
        }
    }
}
   

