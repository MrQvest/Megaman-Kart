using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KartGame.KartSystems{
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
        private float currentSpeed;

            private void Start()
        {
            // Encontra o jogador e define como alvo do casco azul
            target = GameObject.FindGameObjectWithTag("Player").transform;
            currentSpeed = target.GetComponent<ArcadeKart>().baseStats.Acceleration;
        }

        private void Update()
        {
            // Verifica se o alvo estÅEdentro do raio de perseguiÁ„o
            float distance = Vector3.Distance(transform.position, target.position);
                if (distance <= trackingRange)
                {
                    // Rotaciona o casco azul em direÁ„o ao alvo
                    Vector3 targetDirection = target.position - transform.position;
                    Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotateSpeed * Time.deltaTime, 0.0f);
                    transform.rotation = Quaternion.LookRotation(newDirection);

                    // Move o casco azul em direÁ„o ao alvo
                    transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

                    // Se o casco azul atingir o jogador, explode e causa dano
                }                
        }
            void OnCollisionEnter(Collision other)
            {
                  float distance = Vector3.Distance(transform.position, target.position);
                if (other.gameObject.tag == "Player" && distance <= explosionRadius)
                {
                    Instantiate(explosionEffect, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                    // Causa dano ao jogador aqui
                    currentSpeed = 0f;
                    // Inicia a coroutine para restaurar a velocidade padr„o depois de 3 segundos
                    StartCoroutine(RestoreSpeed(3f));
                }
            }
            public void UseItem(Transform position, Quaternion rotation)
        {
            Instantiate(gameObject, position.position - (new Vector3(0f, 2f, 0f)), rotation);
        }
            private IEnumerator RestoreSpeed(float delay)
            {
                // Espera por um tempo antes de restaurar a velocidade padr„o
                yield return new WaitForSeconds(delay);

                // Restaura a velocidade padr„o do jogador
                currentSpeed = 5f;
            }
        }
}
    }
   

