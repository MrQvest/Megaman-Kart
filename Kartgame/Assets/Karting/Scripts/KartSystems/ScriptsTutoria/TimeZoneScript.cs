using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.KartSystems;

public class TimeZoneScript : MonoBehaviour
{
    private bool slowed;
    ArcadeKart arcadeKart;
    

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !slowed)
        {
            arcadeKart = other.gameObject.GetComponentInParent<ArcadeKart>();
            print("GameObject"+other.gameObject);
            print("Script" + arcadeKart);
            if (arcadeKart != null)
            {                
                arcadeKart.baseStats.TopSpeed /= 2;
                slowed = true;
                Invoke("Delete", 2);
            }
        }
    }
    void Delete()
    {
        arcadeKart.baseStats.TopSpeed *= 2;
        Destroy(gameObject);
    }
}
