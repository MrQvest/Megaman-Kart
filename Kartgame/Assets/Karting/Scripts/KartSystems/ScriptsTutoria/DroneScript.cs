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
        private ArcadeKart hitTarget;

            private void Start()
        {
            // Encontra o jogador e define como alvo do casco azul
            target = findPlayer().transform;
        }
            private Transform findPlayer()
            {
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                GameObject farthest = null;
                float maxDist = -Mathf.Infinity;
                Vector3 position = transform.position;
                foreach (GameObject go in players)
                {
                    Vector3 diff = go.transform.position - position;
                    float curDistance = diff.sqrMagnitude;
                    if (curDistance > maxDist)
                    {
                        farthest = go;
                        maxDist = curDistance;
                    }
                }
                return farthest.transform;
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
            void OnTriggerEnter(Collider other)
            {
                  float distance = Vector3.Distance(transform.position, target.position);
                if (other.gameObject.tag == "Player" && distance <= explosionRadius)
                {
                    Destroy(gameObject);
                    hitTarget = other.gameObject.GetComponent<ArcadeKart>();

                    // Causa dano ao jogador aqui
                    hitTarget.baseStats.Acceleration = 0f;
                    hitTarget.baseStats.TopSpeed = 0f;
                    // Inicia a coroutine para restaurar a velocidade padr„o depois de 3 segundos
                }
            }
            private void OnDestroy()
            {
                hitTarget.StartCoroutine(RestoreSpeed(3f));
            }
            public void UseItem(Transform position, Quaternion rotation)
        {
            Instantiate(gameObject, position.position - (new Vector3(0f, 0f, 0f)), rotation);
        }
            private IEnumerator RestoreSpeed(float delay)
            {
                // Espera por um tempo antes de restaurar a velocidade padr„o
                yield return new WaitForSeconds(delay);

                // Restaura a velocidade padr„o do jogador
               hitTarget.baseStats.Acceleration = 10f;
               hitTarget.baseStats.TopSpeed = 30f;
            }
        }
}
    }
   

