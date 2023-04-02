using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneScript : MonoBehaviour, IUseItem
{
    // Start is called before the first frame update
    public float Speed = 5f; // Velocidade de perseguição
    private Transform Player; // Transform do jogador mais próximo
    public LineRenderer lineRenderer1; // Referência ao Line Renderer
    public LineRenderer lineRenderer2;
    public LineRenderer lineRenderer3;
    void Start()
    {
        lineRenderer1 = GetComponent<LineRenderer>();
        lineRenderer2 = GetComponent<LineRenderer>();
        lineRenderer3 = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Encontra o jogador mais próximo
        Player = FindNextPlayer();

        if (Player != null)
        {
            // Direciona o objeto para o jogador
            transform.LookAt(Player);

            // Move o objeto em direção ao jogador
            transform.position += transform.forward * Speed * Time.deltaTime;

            // Configura a posição das pontas do Line Renderer
            lineRenderer1.SetPosition(0, transform.position);
            lineRenderer1.SetPosition(1, Player.position);
            lineRenderer2.SetPosition(0, transform.position);
            lineRenderer2.SetPosition(1, Player.position);
            lineRenderer3.SetPosition(0, transform.position);
            lineRenderer3.SetPosition(1, Player.position);
        }
    }

    Transform FindNextPlayer()
    {
        GameObject[] Players= GameObject.FindGameObjectsWithTag("Player"); // Encontra todos os jogadores
        Transform NextPlayer= null;
        float Closedistance= Mathf.Infinity;

        foreach (GameObject player in Players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance < Closedistance)
            {
                Closedistance = distance;
                NextPlayer = player.transform;
            }
        }

        return NextPlayer;
    }

    public void UseItem(Transform position, Quaternion rotation)
    {
        Instantiate(gameObject, position.position - (new Vector3(0f, 2f, 0f)), rotation);
    }
}
   

