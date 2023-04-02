using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        // Verifica se algum jogador está próximo o suficiente para teleporte
        Collider[] colliders = Physics.OverlapSphere(transform.position, maxRange);
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Player") && col.gameObject != gameObject)
            {
                // Mostra o efeito de teleporte
                GameObject effect = Instantiate(teleportEffect, transform.position, Quaternion.identity);
                Destroy(effect, teleportTime);

                // Inicia o teleporte
                StartCoroutine(Teleport(col.transform));
                break;
            }
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
